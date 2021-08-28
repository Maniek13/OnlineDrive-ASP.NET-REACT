using System.ComponentModel.DataAnnotations;

namespace TreeExplorer.Models
{
    public class UsserData
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UsserId { get; set; }
        [Required]
        public string IpV4 { get; set; }
        [Required]
        public string Browser { get; set; }
        public Usser Usser { get; set; }
    }
}
