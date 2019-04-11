
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public class UserEvent
    {
        [Key]
        public int idUsev { get; set; }
        public User User { get; set; }
        public Event Event { get;set; }

        public bool participation { get; set; }

        [Range(0, 5,ErrorMessage = "Value must be between 0 and 5.")]
        public int star { get; set; }
    }
}
