using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Event
    {
        [Key]
        public int idEvent{get;set;}
        public string EventTitle { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public string Description { get; set; }
        public ICollection<EventPicture> Pics { get; set; }


        public organization hostedby { get; set; }
        public User creator { get; set; }
        public Theme theme { get; set; }
        public Admin approvedBy { get; set; }
    }
}
