using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class User
    {
        [JsonIgnore]
        [Key]
        public int id { get; set; }

        [StringLength(20)]
        [Index(IsUnique =true)]
        public string username { get; set; }
        [JsonIgnore]
        [StringLength(254)]
        [Index(IsUnique = true)]
        public string mail { get; set; }
        [JsonIgnore]
        [StringLength(8)]
        [Index(IsUnique = true)]
        public string phone { get; set; }
        [JsonIgnore]
        [Required]
        [MinLength(8,ErrorMessage ="password must be at least 8 characters"),MaxLength(20,ErrorMessage ="password must be less than 20 charaters")]
        public string password { get; set; }
        [JsonIgnore]
        public string activated { get; set; }
        [JsonIgnore]
        public virtual ICollection<Event> Event { get; set; }

        public User()
        {

        }

        public User(string username,  string mail, string phone, string password)
        {
            this.username = username;
           
            this.mail = mail;
            this.phone = phone;
            this.password = password;
            
        }
    }
}
