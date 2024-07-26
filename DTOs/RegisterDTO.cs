using System.ComponentModel.DataAnnotations;

namespace projetoWebApi.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Username Required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string? Password { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "E-mail required")]
        public string? Email { get; set; }
    }
}
