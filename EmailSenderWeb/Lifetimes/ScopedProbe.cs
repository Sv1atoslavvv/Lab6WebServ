namespace EmailSenderWeb.Lifetimes;

public sealed class ScopedProbe
{
    public Guid Id { get; } = Guid.NewGuid();
}
