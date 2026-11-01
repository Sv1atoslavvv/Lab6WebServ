namespace EmailSenderWeb.Lifetimes;

public sealed class SingletonProbe
{
    public Guid Id { get; } = Guid.NewGuid();
}
