using System;
using System.Collections.Generic;
using System.Text;

namespace CoCon.Templates
{
    /// <summary>
    /// Generates a compile-ready template class.
    /// </summary>
    public class TemplateClassFactory
    {
        /// <summary>
        /// Creates the template class.
        /// </summary>
        /// <param name="segments">The segments of which the template consists.</param>
        /// <returns>A string containing a compile-ready template class.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">A <see cref="TemplateSegment"/> with an unknown <see cref="TemplateSegmentType"/> was encountered.</exception>
        public string CreateTemplateClass(IReadOnlyList<TemplateSegment> segments)
        {
            var templateBuilder = new StringBuilder();

            templateBuilder.AppendLine("public class RuntimeGeneratedTemplateClass : global::CoCon.Templates.TemplateBase {");
            templateBuilder.AppendLine("\tprotected override void ProcessInternal() {");

            foreach (TemplateSegment segment in segments)
            {
                switch (segment.Type)
                {
                    case TemplateSegmentType.PlainText:
                        string writeCall = string.Format("Write(\"{0}\");", segment.Content);
                        templateBuilder.Append(writeCall);
                        break;
                    case TemplateSegmentType.CodeBlock:
                        templateBuilder.Append(segment.Content);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            templateBuilder.AppendLine("\t}");
            templateBuilder.AppendLine("}");

            return templateBuilder.ToString();
        }
    }
}
