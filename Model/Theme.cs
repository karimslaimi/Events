
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public class Theme
    {
        [Key]
        public int idTheme { get; set; }
        public string designation { get; set; }

        public Theme(string designation)
        {
            this.designation = designation;
        }

        public Theme()
        {
        }
    }
}
