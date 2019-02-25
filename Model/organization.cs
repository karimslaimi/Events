using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class organization
    {
        [Key]
        public int idorg { get; set; }//organization id
        public string orgname { get; set; }//organization name
        public University university { get; set; }

        public organization(string orgname,University university)
        {
            this.orgname = orgname;
            this.university = university;
        }

        public organization()
        {
        }
    }
}
