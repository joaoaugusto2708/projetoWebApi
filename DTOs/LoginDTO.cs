using System.ComponentModel.DataAnnotations;

namespace projetoWebApi.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="Username Required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string? Password { get; set; }
    }
}
