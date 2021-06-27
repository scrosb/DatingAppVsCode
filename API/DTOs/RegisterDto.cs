using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        //an "N" is not important here. 
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}