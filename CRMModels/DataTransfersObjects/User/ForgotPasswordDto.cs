using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CRMModels.DataTransfersObjects
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
