using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.Accounts
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Kirjuta oma uus praool uuesti:")]
        [Compare("Password", ErrorMessage = "Paroolid ei kattu, plaun proovi uuesti")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
