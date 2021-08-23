using TreeExplorer.Interfaces;

namespace TreeExplorer.Models
{
    public class ContentLengthRestrictionOptions : IContentLengthRestrictionOptions
    {
        public int ContentLengthLimit { get; set; }
    }
}
