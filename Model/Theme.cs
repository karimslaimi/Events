using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Theme
    {
        [Key]
        public int idTheme { get; set; }
        public string designation { get; set; }

        public Theme(string designation)
        {
            this.designation = designation;
        }

        public Theme()
        {
        }
    }
}
