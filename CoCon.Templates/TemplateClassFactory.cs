using System;
using System.Collections.Generic;
using System.Text;

using CoCon.Templates.Parser;

namespace CoCon.Templates
{
    public class TemplateClassFactory
    {
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
