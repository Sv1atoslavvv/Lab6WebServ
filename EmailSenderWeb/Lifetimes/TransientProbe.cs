namespace EmailSenderWeb.Lifetimes;

public sealed class TransientProbe
{
    public Guid Id { get; } = Guid.NewGuid();
}
