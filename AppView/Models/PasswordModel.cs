using System.ComponentModel.DataAnnotations;

namespace AppView.Models
{
    public class PasswordModel
    {
        [Required]
        public string Email { get; set; }
    }
}
