using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PersonsApi.Model
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [JsonProperty("Person")]
        [Required]
        public string PersonName { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
