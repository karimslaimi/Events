using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Event
    {
        [Key]
        public int idEvent{get;set;}
        public string EventTitle { get; set; }//titre de l'evenement
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        public DateTime CreationDate { get; set; } //date de creation
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        public DateTime EventDate { get; set; } //date de l'evenement
        public string EventLocation { get; set; } //lieu de l'evenement
        public string Description { get; set; } //description de l'evenement
        public ICollection<EventPicture> Pics { get; set; } //les photos de l'annonce

        [ForeignKey("hostedby")]
        public int? hostedbyid { get; set; }
        public virtual organization hostedby { get; set; } //l'organisateur de l'evenement

        [ForeignKey("creator")]
        public int? creatorid { get; set; }
        public User creator { get; set; } //le createur de l'evenement 


        [ForeignKey("theme")]
        public int? themeid { get; set; }
        public virtual Theme theme { get; set; } //le theme de l'evenement

        [ForeignKey("approvedby")]
        public int? adminid; 
        public virtual Admin approvedBy { get; set; } //l'admin qui a approuvé l'annonce de l'evenement tant que ce champ est null 
                                             //l'evenement n'apparaitra pas sur le site
    }
}
