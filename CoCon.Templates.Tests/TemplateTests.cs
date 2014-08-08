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
            var template = new Template("Hello <% if(false) { %>World<% } else { %>Universe<% } %>");
            string result = template.Process();

            Assert.AreEqual("Hello Universe", result);
        }
    }
}
