using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
