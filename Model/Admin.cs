using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Admin
    {
        [Key]
        public int idAdmin { get; set; }
        public string nameAdmin { get; set; }
        public string mailAdmin {get;set;}
        public string passwordAdmin { get; set; }
        public bool isSuperAdmin { get; set; }

    }
}
