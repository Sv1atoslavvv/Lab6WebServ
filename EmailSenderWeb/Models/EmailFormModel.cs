using System.ComponentModel.DataAnnotations;

namespace EmailSenderWeb.Models;

public sealed class EmailFormModel
{
    [Required(ErrorMessage = "RecipientRequired")]
    [EmailAddress(ErrorMessage = "RecipientInvalid")]
    [Display(Name = "RecipientLabel")]
    public string Recipient { get; set; } = string.Empty;

    [Required(ErrorMessage = "MessageRequired")]
    [StringLength(5000, MinimumLength = 2, ErrorMessage = "MessageLength")]
    [Display(Name = "MessageLabel")]
    public string Message { get; set; } = string.Empty;
}
