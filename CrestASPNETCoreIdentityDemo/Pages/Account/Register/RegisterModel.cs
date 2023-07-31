using System.ComponentModel.DataAnnotations;

namespace CrestASPNETCoreIdentityDemo.Pages.Account.Register
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
