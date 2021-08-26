using System.ComponentModel.DataAnnotations;

namespace TreeExplorer.Models
{
    public class Element
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int IdW { get; set; }
        [Required]
        public int UsserId { get; set; }
        public string Path { get; set; }
    }
}
