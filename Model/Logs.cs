

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Logs
    {
        [Key]
        public int logid { get; set; }

        [ForeignKey("admin")]
        public int adminid { get; set; }
        public virtual Admin admin { get; set; }
        
        [ForeignKey("events")]
        public int eventid { get; set; }
        public virtual Event events { get; set; }

        public DateTime date { get; set; }

    }
}
