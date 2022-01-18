using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class MadVideo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }   
    }
}
