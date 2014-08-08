namespace CoCon.Templates.Parser
{
    /// <summary>
    /// Represents a segment of a template.
    /// </summary>
    public class TemplateSegment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateSegment"/> class.
        /// </summary>
        /// <param name="type">The type of the segment.</param>
        /// <param name="content">The content of the segment.</param>
        public TemplateSegment(TemplateSegmentType type, string content)
        {
            Type = type;
            Content = content;
        }

        /// <summary>
        /// Gets the type of the segment.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TemplateSegmentType Type { get; private set; }

        /// <summary>
        /// Gets the content of the segment.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; private set; }
    }
}
