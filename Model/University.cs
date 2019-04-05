using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class University
    {
        [Key]
        public int idUniv { get; set; }
        public string UnivName { get; set; }

        ICollection<organization> organizations { get; set; }

        public University(string univName)
        {
            UnivName = univName;
        }

        public University()
        {
        }
    }
}
