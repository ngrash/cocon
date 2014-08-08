using System;
using System.Collections.ObjectModel;

using CoCon.Templates.Parser;

namespace CoCon.Templates
{
    public class Template
    {
        private readonly string _templateString;

        public Template(string templateString)
        {
            _templateString = templateString;
        }

        public string Process()
        {
            var parser = new TemplateParser();
            ReadOnlyCollection<TemplateSegment> segments = parser.Parse(_templateString);

            var classFactory = new TemplateClassFactory();
            string templateClass = classFactory.CreateTemplateClass(segments);

            var compiler = new TemplateCompiler();
            Type templateType = compiler.CompileTemplate(templateClass);

            var processor = new TemplateProcessor();
            string result = processor.ProcessTemplate(templateType);

            return result;
        }
    }
}
