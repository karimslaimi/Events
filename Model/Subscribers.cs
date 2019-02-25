using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Subscribers
    {
        [Key]
        public int idsubs { get; set; }
        public string mailsubs { get; set; }

        public Subscribers(string mailsubs)
        {
            this.mailsubs = mailsubs;
        }

        public Subscribers()
        {
        }
    }
}
