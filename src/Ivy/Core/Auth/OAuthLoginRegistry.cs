using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace Ivy.Core.Auth;

public interface IOAuthLoginRegistry
{
    string RegisterPending(string connectionId, string optionId, string? provider = null);

    PendingOAuthLogin? GetAndRemove(string loginId);
}

public record PendingOAuthLogin(string ConnectionId, string OptionId, DateTime CreatedAt, string? Provider = null);

public class OAuthLoginRegistry : IOAuthLoginRegistry
{
    private readonly ConcurrentDictionary<string, PendingOAuthLogin> _pending = new();
    private static readonly TimeSpan Expiration = TimeSpan.FromMinutes(10);
    private DateTime _lastCleanup = DateTime.UtcNow;
    private static readonly TimeSpan CleanupInterval = TimeSpan.FromMinutes(1);

    public string RegisterPending(string connectionId, string optionId, string? provider = null)
    {
        CleanupExpiredIfNeeded();

        var loginId = GenerateSecureKey();
        _pending[loginId] = new PendingOAuthLogin(connectionId, optionId, DateTime.UtcNow, provider);
        return loginId;
    }

    public PendingOAuthLogin? GetAndRemove(string loginId)
    {
        if (string.IsNullOrEmpty(loginId))
            return null;

        if (_pending.TryRemove(loginId, out var login))
        {
            if (DateTime.UtcNow - login.CreatedAt < Expiration)
                return login;
        }

        return null;
    }

    private static string GenerateSecureKey()
    {
        Span<byte> bytes = stackalloc byte[32];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes, Base64FormattingOptions.None)
            .Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }

    private void CleanupExpiredIfNeeded()
    {
        if (DateTime.UtcNow - _lastCleanup < CleanupInterval)
            return;

        _lastCleanup = DateTime.UtcNow;

        var expiredKeys = _pending
            .Where(kvp => DateTime.UtcNow - kvp.Value.CreatedAt > Expiration)
            .Select(kvp => kvp.Key)
            .ToList();

        foreach (var key in expiredKeys)
        {
            _pending.TryRemove(key, out _);
        }
    }
}
