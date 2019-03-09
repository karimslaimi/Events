using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User_Event
    {
        [Key]
        public int idUser_Event { get; set; }
        public User User;
        public Event Event { get;set; }
        public bool participate;
        public string comment;
    }
}
