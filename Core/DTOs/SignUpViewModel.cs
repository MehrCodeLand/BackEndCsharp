using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Not Found")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "RePassword")]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}
