using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TreeExplorer.Models
{
    public class Usser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<Element> Elements { get; set; }
        public ICollection<UsserData> UsserDatas { get; set; }
    }
}
