namespace EmailSenderWeb.Models;

public sealed class LifetimeDemoModel
{
    public Guid TransientFirst { get; init; }
    public Guid TransientSecond { get; init; }
    public Guid ScopedFirst { get; init; }
    public Guid ScopedSecond { get; init; }
    public Guid SingletonFirst { get; init; }
    public Guid SingletonSecond { get; init; }
}
