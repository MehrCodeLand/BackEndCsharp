using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Article
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Descriptions")]
        public string Descriptions { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "AothurName")]
        public string AothurName { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Body")]
        public string Body { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Picture")]
        public string Picture { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "PictureTitle")]
        public string PictureTitle { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Alt Pic")]
        public string AltPicture { get; set; }
        public DateTime Created { get; set; }


    }
}
