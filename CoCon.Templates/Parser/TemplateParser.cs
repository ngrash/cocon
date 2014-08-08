using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace CoCon.Templates.Parser
{
    /// <summary>
    /// The <c>TemplateParser</c> extracts <see cref="TemplateSegment"/>s from a template string.
    /// </summary>
    public class TemplateParser
    {
        /// <summary>
        /// Parses the specified template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns>A <c>ReadOnlyCollection</c> containing the segments of the template.</returns>
        /// <exception cref="System.ArgumentNullException">The <paramref name="template"/> parameter is null.</exception>
        public ReadOnlyCollection<TemplateSegment> Parse(string template)
        {
            if (template == null)
            {
                throw new ArgumentNullException("template");
            }

            var segments = new List<TemplateSegment>();

            using (var reader = new StringReader(template))
            {
                var segmentType = TemplateSegmentType.PlainText;
                var segmentContent = new StringBuilder();

                int ch;
                while ((ch = reader.Read()) != -1)
                {
                    bool isOpening = !IsInsideBlock(segmentType) && ch == '<' && reader.Peek() == '%';
                    bool isClosing = IsInsideBlock(segmentType) && ch == '%' && reader.Peek() == '>';

                    if (isOpening || isClosing)
                    {
                        // Because we peeked at the next char and know it is part of the identifier we can discard it
                        reader.Read();

                        if (segmentContent.Length != 0)
                        {
                            segments.Add(new TemplateSegment(segmentType, segmentContent.ToString()));
                            segmentContent.Clear();
                        }

                        segmentType = isOpening ? TemplateSegmentType.CodeBlock : TemplateSegmentType.PlainText;
                    }
                    else
                    {
                        segmentContent.Append((char)ch);
                    }
                }

                // If there is never an opening or closing identifier, the content of the segment is considered plain text
                if (segmentContent.Length != 0)
                {
                    segments.Add(new TemplateSegment(TemplateSegmentType.PlainText, segmentContent.ToString()));
                }
            }

            return new ReadOnlyCollection<TemplateSegment>(segments);
        }

        private bool IsInsideBlock(TemplateSegmentType type)
        {
            return type != TemplateSegmentType.PlainText;
        }
    }
}
