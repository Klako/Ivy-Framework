using Ivy;
using Ivy.Plugin.HelloWorld;
using static Ivy.Layout;
using static Ivy.Text;

namespace HelloWorldHost;

[App(icon: Icons.Hand, title: "Hello World")]
public class HelloWorldApp : ViewBase
{
    public override object? Build()
    {
        var greeters = this.UseService<IReadOnlyList<IGreeter>>();
        var nameState = UseState("World");

        var greeting = greeters.Count > 0
            ? greeters[0].Greet(string.IsNullOrWhiteSpace(nameState.Value) ? "World" : nameState.Value)
            : "No greeter plugin loaded.";

        return Vertical().Gap(6).Padding(4)
            | H1(greeting)
            | new Field(
                nameState.ToTextInput().Placeholder("Enter a name")
            ).Label("Who to greet?")
            | Muted($"Using {greeters.Count} greeter plugin(s)");
    }
}
