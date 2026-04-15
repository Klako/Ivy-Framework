using System.IO;
using System.Linq;
using System.Xml.Linq;
using Xunit;

namespace Ivy.Tendril.Test;

public class ExternalWidgetValidationTests
{
    private const string WidgetsDirectory = "../../../../../../widgets";
    private const string WorkflowPath = "../../../../../../.github/workflows/publish-external-widgets.yml";

    [Fact]
    public void DetectsExternalWidgetPackages_WhenScanningWidgetsDirectory()
    {
        // Arrange
        var widgetsPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), WidgetsDirectory));

        // Act
        var widgetDirs = Directory.GetDirectories(widgetsPath, "Ivy.Widgets.*");
        var csprojFiles = widgetDirs
            .Select(dir => Path.Combine(dir, Path.GetFileName(dir) + ".csproj"))
            .Where(File.Exists)
            .ToList();

        // Assert
        Assert.NotEmpty(widgetDirs);
        Assert.NotEmpty(csprojFiles);
        Assert.True(csprojFiles.Count >= 6, $"Expected at least 6 external widget projects, found {csprojFiles.Count}");
    }

    [Fact]
    public void ValidatesNuGetMetadata_ForExistingExternalWidgets()
    {
        // Arrange
        var widgetsPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), WidgetsDirectory));
        var widgetDirs = Directory.GetDirectories(widgetsPath, "Ivy.Widgets.*");

        var requiredElements = new[] { "PackageId", "Description", "Authors", "PackageLicenseExpression" };

        // Act & Assert
        foreach (var widgetDir in widgetDirs)
        {
            var widgetName = Path.GetFileName(widgetDir);
            var csprojPath = Path.Combine(widgetDir, widgetName + ".csproj");

            Assert.True(File.Exists(csprojPath), $"Expected .csproj file for {widgetName}");

            var doc = XDocument.Load(csprojPath);
            var ns = doc.Root?.Name.Namespace ?? XNamespace.None;

            foreach (var element in requiredElements)
            {
                var found = doc.Descendants(ns + element).Any();
                Assert.True(found, $"Widget {widgetName} is missing NuGet metadata element: {element}");
            }

            // Verify Ivy.ExternalWidget.targets import
            var import = doc.Descendants(ns + "Import")
                .Any(x => x.Attribute("Project")?.Value.Contains("Ivy.ExternalWidget.targets") == true);
            Assert.True(import, $"Widget {widgetName} does not import Ivy.ExternalWidget.targets");
        }
    }

    [Fact]
    public void ValidatesWorkflowInclusion_ForExternalWidgets()
    {
        // Arrange
        var workflowPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), WorkflowPath));

        // Act
        Assert.True(File.Exists(workflowPath), "publish-external-widgets.yml workflow file does not exist");

        var workflowContent = File.ReadAllText(workflowPath);

        // Assert
        Assert.Contains("name: Publish External Widgets", workflowContent);
        Assert.Contains("widgets/v*", workflowContent);
        Assert.Contains("Ivy.Widgets.", workflowContent);
        Assert.Contains("dotnet pack", workflowContent);
        Assert.Contains("NuGet", workflowContent);
    }
}
