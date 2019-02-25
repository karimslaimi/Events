using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }//prenom
        public string lastname { get; set; }//nom
        public string mail { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public DateTime birthdate { get; set; }
        public ICollection<Event> Event { get; set; }

        public User()
        {

        }

        public User(string username, string firstname, string lastname, string mail, string phone, string password, DateTime birthdate)
        {
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.mail = mail;
            this.phone = phone;
            this.password = password;
            this.birthdate = birthdate;
        }
    }
}
