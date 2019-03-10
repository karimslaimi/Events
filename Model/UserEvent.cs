using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserEvent
    {
        [Key]
        public int idUsev { get; set; }
        public User User { get; set; }
        public Event Event { get;set; }

        public bool participation { get; set; }
        public string comment { get; set; }
    }
}
