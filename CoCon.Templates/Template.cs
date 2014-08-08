using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;

using Microsoft.CSharp;

namespace CoCon.Templates
{
    public class Template
    {
        private readonly string _templateString;
        private readonly StringBuilder _resultBuilder = new StringBuilder();

        public Template(string templateString)
        {
            _templateString = templateString;
        }

        protected Template()
        {
            
        }

        public string Process()
        {
            var codeBuffer = new StringBuilder();
            var outputBuffer = new StringBuilder();

            bool insideCodeBlock = false;

            using (var reader = new StringReader(_templateString))
            {
                int ch;
                while ((ch = reader.Read()) != -1)
                {
                    if (!insideCodeBlock && ch == '<' && reader.Peek() == '%')
                    {
                        reader.Read();
                        insideCodeBlock = true;

                        // Transform output buffer into code
                        string output = outputBuffer.ToString();
                        string outputCode = string.Format("Write(\"{0}\");", output);
                        codeBuffer.AppendLine(outputCode);
                    }
                    else if (insideCodeBlock && ch == '%' && reader.Peek() == '>')
                    {
                        reader.Read();
                        insideCodeBlock = false;
                    }
                    else
                    {
                        if (insideCodeBlock)
                        {
                            codeBuffer.Append((char)ch);
                        }
                        else
                        {
                            outputBuffer.Append((char)ch);
                        }
                    }
                }
            }

            var snippetBuffer = new StringBuilder();
            snippetBuffer.AppendLine("public class CoConTemplate : global::CoCon.Templates.Template {");
            snippetBuffer.AppendLine("\tpublic CoConTemplate() { }");
            snippetBuffer.AppendLine("\tpublic void ProcessTemplate() {");
            snippetBuffer.AppendLine(codeBuffer.ToString());
            snippetBuffer.AppendLine("\t}");
            snippetBuffer.AppendLine("}");
            string snippet = snippetBuffer.ToString();

            var codeProvider = new CSharpCodeProvider();
            var options = new CompilerParameters();
            options.ReferencedAssemblies.Add("CoCon.Templates.dll");
            CompilerResults result = codeProvider.CompileAssemblyFromSource(options, snippet);

            Type type = result.CompiledAssembly.GetType("CoConTemplate");
            object instance = Activator.CreateInstance(type);

            MethodInfo processMethod = type.GetMethod("ProcessTemplate");
            processMethod.Invoke(instance, null);

            MethodInfo resultMethod = type.GetMethod("GetResult");
            var processResult = (string)resultMethod.Invoke(instance, null);

            return processResult;
        }

        public string GetResult()
        {
            return _resultBuilder.ToString();
        }

        protected void Write(string text)
        {
            _resultBuilder.Append(text);
        }
    }
}
