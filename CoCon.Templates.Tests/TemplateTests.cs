using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoCon.Templates.Tests
{
    [TestClass]
    public class TemplateTests
    {
        [TestMethod]
        public void CanProcessBasicTemplate()
        {
            var template = new Template("Hello <% Write(\"World\"); %>");
            string result = template.Process();

            Assert.AreEqual("Hello World", result);
        }

        [TestMethod]
        public void CanProcessBasicConditionalTemplate()
        {
            const string TemplateString = @"Hello <% if(false) { %>World<% } else { %>Universe<% } %>";
            var template = new Template(TemplateString);
            string result = template.Process();

            const string ExpectedResult = @"Hello Universe";

            Assert.AreEqual(ExpectedResult, result);
        }
    }
}
