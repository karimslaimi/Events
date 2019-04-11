
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Model
{
    public class EventPicture
    {
        [Key]
        public int idPicture { get; set; }
        public string picName { get; set; }

        [ForeignKey("Event")]
        public int eventid { get; set; }
        public virtual Event Event { get; set; }
    }
}
