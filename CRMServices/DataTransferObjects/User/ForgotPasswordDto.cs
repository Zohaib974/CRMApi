using System.ComponentModel.DataAnnotations;

namespace CRMServices.DataTransferObjects
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
