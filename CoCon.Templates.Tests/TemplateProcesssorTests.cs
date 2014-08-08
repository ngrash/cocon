using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoCon.Templates.Tests
{
    [TestClass]
    public class TemplateProcesssorTests
    {
        [TestMethod]
        public void ReturnsStringResultOfProcessMethod()
        {
            var processor = new TemplateProcessor();
            string result = processor.ProcessTemplate(typeof(TestClass));

            Assert.AreEqual("success", result);
        }

        private class TestClass
        {
            public string Process()
            {
                return "success";
            }
        }
    }
}
