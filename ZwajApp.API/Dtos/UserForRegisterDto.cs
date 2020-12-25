using System.ComponentModel.DataAnnotations;

namespace ZwajApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string userName { get; set; }
        
        [StringLength(8,MinimumLength=6,ErrorMessage="pass should be between 8 to 6 length")]
        public string Password { get; set; }

    }
}