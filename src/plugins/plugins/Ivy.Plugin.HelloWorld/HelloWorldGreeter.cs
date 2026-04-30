namespace Ivy.Plugin.HelloWorld;

public class HelloWorldGreeter : IGreeter
{
    public string Greet(string name) => $"Hello from Ivy.Plugin.HelloWorld, {name}!";
}
