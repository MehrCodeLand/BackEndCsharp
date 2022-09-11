using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SignInViewModel
    {
        [Display(Name = "Enter ID")]
        public string UsernameOrEmail { get; set; }
        [Display(Name = "Password")]   
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

    }
}
