
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public class Theme
    {
        [JsonIgnore]
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
