
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Model
{
    public class organization
    {
        [Key]
        public int idorg { get; set; }//organization id
        public string orgname { get; set; }//organization name

        [ForeignKey("university")]
        public int idUniv { get; set; }
        public virtual University university { get; set; }

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
