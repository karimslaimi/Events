using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Admin
    {
        [Key]
        public int idAdmin { get; set; }


        [Required(ErrorMessage ="this field is required")]
        public string nameAdmin { get; set; }


        [Required(ErrorMessage = "this field is required")]
        [DataType(DataType.EmailAddress)]
        public string mailAdmin { get;set;  }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "this field is required")]
        public string passwordAdmin { get; set; }

        public bool isSuperAdmin { get; set; }

    }
}
