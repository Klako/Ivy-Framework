using System.ComponentModel;
using System.Text.RegularExpressions;
using Spectre.Console.Cli;

namespace Ivy.IvyML.Console;

public sealed partial class IconsCommand : Command<IconsCommand.Settings>
{
    private static readonly string[] AllIcons = Enum.GetNames<Icons>()
        .Where(n => n != nameof(Icons.None))
        .ToArray();

    public sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "[search]")]
        [Description("Search term to filter icons (case-insensitive, matches anywhere in the name).")]
        public string? Search { get; init; }
    }

    protected override int Execute(CommandContext context, Settings settings, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(settings.Search))
        {
            System.Console.WriteLine($"{AllIcons.Length} icons available. Provide a search term to filter.");
            System.Console.WriteLine();
            System.Console.WriteLine("Usage: ivyml icons <search>");
            System.Console.WriteLine();
            System.Console.WriteLine("Examples:");
            System.Console.WriteLine("  ivyml icons chart     -> ChartArea, ChartBar, ChartLine, ...");
            System.Console.WriteLine("  ivyml icons arrow     -> ArrowDown, ArrowLeft, ArrowRight, ...");
            System.Console.WriteLine("  ivyml icons user      -> User, UserCheck, UserPlus, ...");
            return 0;
        }

        var terms = settings.Search.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var matches = AllIcons
            .Select(name => (name, score: Score(name, terms)))
            .Where(x => x.score > 0)
            .OrderByDescending(x => x.score)
            .ThenBy(x => x.name)
            .Select(x => x.name)
            .ToArray();

        if (matches.Length == 0)
        {
            System.Console.Error.WriteLine($"No icons matching \"{settings.Search}\".");
            return 1;
        }

        System.Console.WriteLine($"{matches.Length} icon{(matches.Length == 1 ? "" : "s")} matching \"{settings.Search}\":");
        System.Console.WriteLine();
        foreach (var name in matches)
            System.Console.WriteLine(name);

        return 0;
    }

    private static int Score(string iconName, string[] terms)
    {
        var segments = SplitPascalCase(iconName);
        var score = 0;

        foreach (var term in terms)
        {
            var termMatched = false;

            foreach (var seg in segments)
            {
                if (seg.Equals(term, StringComparison.OrdinalIgnoreCase))
                {
                    score += 10;
                    termMatched = true;
                }
                else if (seg.StartsWith(term, StringComparison.OrdinalIgnoreCase))
                {
                    score += 5;
                    termMatched = true;
                }
            }

            if (!termMatched)
            {
                if (iconName.Contains(term, StringComparison.OrdinalIgnoreCase))
                    score += 2;
                else
                    return 0;
            }
        }

        return score;
    }

    private static string[] SplitPascalCase(string name)
    {
        return PascalCasePattern().Split(name)
            .Where(s => s.Length > 0)
            .ToArray();
    }

    [GeneratedRegex("(?<=[a-z])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])|(?<=[A-Za-z])(?=[0-9])")]
    private static partial Regex PascalCasePattern();
}
