using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

// ReSharper disable once CheckNamespace
namespace Ivy;

[Route("ivy/local-file")]
public class LocalFileController(Server server) : Controller
{
    private static readonly FileExtensionContentTypeProvider ContentTypeProvider = new();

    [HttpGet]
    public IActionResult GetFile([FromQuery] string? path)
    {
        if (!server.Args.DangerouslyAllowLocalFiles)
            return NotFound();

        if (string.IsNullOrWhiteSpace(path))
            return BadRequest("Path is required");

        var fullPath = Path.GetFullPath(path);
        if (!System.IO.File.Exists(fullPath))
            return NotFound();

        if (!ContentTypeProvider.TryGetContentType(fullPath, out var contentType))
            contentType = "application/octet-stream";

        return PhysicalFile(fullPath, contentType);
    }
}
