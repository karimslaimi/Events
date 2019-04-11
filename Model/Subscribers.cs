
using System.ComponentModel.DataAnnotations;


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
