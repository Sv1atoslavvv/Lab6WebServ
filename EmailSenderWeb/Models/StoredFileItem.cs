namespace EmailSenderWeb.Models;

public sealed class StoredFileItem
{
    public required string StoredName { get; init; }

    public required string DisplayName { get; init; }

    public required long Length { get; init; }

    public required DateTime UploadedAt { get; init; }

    public required bool CanPreview { get; init; }
}
