using TreeExplorer.Interfaces;

namespace TreeExplorer.Objects
{
    public class ContentLengthRestrictionOptions : IContentLengthRestrictionOptions
    {
        public int ContentLengthLimit { get; set; }
    }
}
