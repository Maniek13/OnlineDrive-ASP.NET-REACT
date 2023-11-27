using TreeExplorer.Interfaces;

namespace TreeExplorer.Models
{
    public class Responde : IResponde
    {
        public string Message { get; set; }
        public bool Error { get; set; }
    }
}
