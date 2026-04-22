namespace Ivy.Test.Views.DataTables;

public class DataTableRowActionsTests
{
    private record TestRow(int Id, string Name, string Status);

    [Fact]
    public void RowActions_StaticOverload_StoresActions()
    {
        var data = new[] { new TestRow(1, "A", "Active") }.AsQueryable();
        var actions = new[] { new MenuItem("Edit"), new MenuItem("Delete") };
        var builder = data.ToDataTable()
            .RowActions(actions);

        var field = builder.GetType()
            .GetField("_menuItemRowActions", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(field);
        var stored = field.GetValue(builder) as MenuItem[];
        Assert.NotNull(stored);
        Assert.Equal(2, stored.Length);
    }

    [Fact]
    public void RowActions_FactoryOverload_StoresFactory()
    {
        var data = new[] { new TestRow(1, "A", "Active") }.AsQueryable();
        var builder = data.ToDataTable()
            .RowActions(row => row.Status == "Active"
                ? [new MenuItem("Stop")]
                : [new MenuItem("Start")]);

        var field = builder.GetType()
            .GetField("_rowActionsFactory", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(field);
        Assert.NotNull(field.GetValue(builder));
    }

    [Fact]
    public void RowActions_FactoryOverload_ReturnsBuilder_ForChaining()
    {
        var data = new[] { new TestRow(1, "A", "Active") }.AsQueryable();
        var builder = data.ToDataTable();
        var result = builder.RowActions(row => [new MenuItem("Edit")]);
        Assert.Same(builder, result);
    }

    [Fact]
    public void RowActions_StaticOverload_ReturnsBuilder_ForChaining()
    {
        var data = new[] { new TestRow(1, "A", "Active") }.AsQueryable();
        var builder = data.ToDataTable();
        var result = builder.RowActions(new MenuItem("Edit"));
        Assert.Same(builder, result);
    }

    [Fact]
    public void RowActions_FactoryInvokedPerRow_ReturnsDifferentActions()
    {
        var data = new[]
        {
            new TestRow(1, "A", "Active"),
            new TestRow(2, "B", "Stopped"),
            new TestRow(3, "C", "Active"),
        };

        Func<TestRow, MenuItem[]> factory = row => row.Status == "Active"
            ? [new MenuItem("Stop") { Tag = "stop" }]
            : [new MenuItem("Start") { Tag = "start" }];

        var actionsForRow1 = factory(data[0]);
        var actionsForRow2 = factory(data[1]);
        var actionsForRow3 = factory(data[2]);

        Assert.Single(actionsForRow1);
        Assert.Equal("stop", actionsForRow1[0].Tag);

        Assert.Single(actionsForRow2);
        Assert.Equal("start", actionsForRow2[0].Tag);

        Assert.Single(actionsForRow3);
        Assert.Equal("stop", actionsForRow3[0].Tag);
    }
}
