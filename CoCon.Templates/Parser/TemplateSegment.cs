namespace CoCon.Templates.Parser
{
    public class TemplateSegment
    {
        public TemplateSegment(TemplateSegmentType type, string content)
        {
            Type = type;
            Content = content;
        }

        public TemplateSegmentType Type { get; private set; }

        public string Content { get; private set; }
    }
}
