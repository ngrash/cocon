using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoCon.Templates.Tests
{
    [TestClass]
    public class TemplateCompilerTests
    {
        [TestMethod]
        public void CompilesAndReturnsRuntimeGeneratedTemplateClass()
        {
            const string TemplateCode = @"class RuntimeGeneratedTemplateClass { }";

            var compiler = new TemplateCompiler();
            Type result = compiler.CompileTemplate(TemplateCode);

            Assert.IsNotNull(result, "Result type is null");
        }
    }
}
