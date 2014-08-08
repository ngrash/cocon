using System;
using System.CodeDom.Compiler;
using System.Reflection;

using Microsoft.CSharp;

namespace CoCon.Templates
{
    /// <summary>
    /// Compiles template classes.
    /// </summary>
    public class TemplateCompiler
    {
        /// <summary>
        /// Compiles the specified template class.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns>The type of the compiled template.</returns>
        public Type CompileTemplate(string template)
        {
            var codeProvider = new CSharpCodeProvider();
            var options = new CompilerParameters();
            options.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
            CompilerResults result = codeProvider.CompileAssemblyFromSource(options, template);

            Type templateType = result.CompiledAssembly.GetType("RuntimeGeneratedTemplateClass");

            return templateType;
        }
    }
}
