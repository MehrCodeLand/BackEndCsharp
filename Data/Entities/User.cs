using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string ActiveCode { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "User Avatar")]
        public DateTime Created { get; set; }
        public bool IsActive { get; set; }
        //public string UserAvatar { get; set; }

    }
}
