using System;
using System.CodeDom.Compiler;
using System.Reflection;

using Microsoft.CSharp;

namespace CoCon.Templates
{
    public class TemplateCompiler
    {
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
