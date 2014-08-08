using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoCon.Templates.Tests
{
    [TestClass]
    public class TemplateBaseTests
    {
        [TestMethod]
        public void ProcessCallsProcessInternal()
        {
            var template = new TestTemplate();
            template.Process();

            Assert.IsTrue(template.ProcessInternalCalled);
        }

        [TestMethod]
        public void ProcessReturnsEmptyString()
        {
            var template = new TestTemplate();
            string result = template.Process();

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void WriteWritesToContent()
        {
            var template = new TestTemplate();
            template.Write("foo");
            string result = template.Process();

            Assert.AreEqual("foo", result);
        }

        private class TestTemplate : TemplateBase
        {
            public bool ProcessInternalCalled { get; private set; }

            public new void Write(string content)
            {
                base.Write(content);
            }

            protected override void ProcessInternal()
            {
                ProcessInternalCalled = true;
            }
        }
    }
}
