using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Participate
    {
        [Key]
        public int idParticipation { get; set; }
        public User User;
        public Event Event { get;set; }
    }
}
