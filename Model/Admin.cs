using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


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

        public ICollection<Event> Event { get; set; }

        public ICollection<Logs> logs { get; set; }


    }
}
