
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class UserEvent
    {
        [Key]
        public int idUsev { get; set; }

        [ForeignKey("User")]
        public int userid { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Event")]
        public int eventid { get; set; }
        public virtual Event Event { get;set; }

        public bool participation { get; set; }

        public bool like { get; set; }
    }
}
