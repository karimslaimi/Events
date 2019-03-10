using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Event picEvent { get; set; }
    }
}
