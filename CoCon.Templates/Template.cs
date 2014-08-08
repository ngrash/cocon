using System;
using System.Collections.ObjectModel;

namespace CoCon.Templates
{
    /// <summary>
    /// Represents a template.
    /// </summary>
    public class Template
    {
        private readonly string _templateString;

        /// <summary>
        /// Initializes a new instance of the <see cref="Template"/> class.
        /// </summary>
        /// <param name="templateString">The template in string representation.</param>
        public Template(string templateString)
        {
            _templateString = templateString;
        }

        /// <summary>
        /// Processes the template.
        /// </summary>
        /// <returns>The result of the template.</returns>
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
