
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    public class University
    {
        [JsonIgnore]
        [Key]
        public int idUniv { get; set; }
        public string UnivName { get; set; }

        [JsonIgnore]
        public virtual ICollection<organization> organizations { get; set; }

        public University(string univName)
        {
            UnivName = univName;
        }

        public University()
        {
        }
    }
}
