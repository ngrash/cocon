using System.Collections.Generic;

using CoCon.Templates.Parser;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoCon.Templates.Tests
{
    [TestClass]
    public class TemplateClassFactoryTests
    {
        public const string TestPlainText = "fizzbuzz";
        public const string TestCodeBlock = "foo();";

        [TestMethod]
        public void CreatesEmptyTemplate()
        {
            var factory = new TemplateClassFactory();
            string templateClass = factory.CreateTemplateClass(new List<TemplateSegment>());

            Assert.IsNotNull(templateClass);
            Assert.AreNotEqual(string.Empty, templateClass);

            // Some sanity checks
            Assert.IsFalse(templateClass.Contains(TestPlainText), "Empty template class contains test plain text");
            Assert.IsFalse(templateClass.Contains(TestCodeBlock), "Empty template class contains test code block");
        }

        [TestMethod]
        public void InsertsPlainText()
        {
            var factory = new TemplateClassFactory();
            var segments = new List<TemplateSegment>
                {
                    new TemplateSegment(TemplateSegmentType.PlainText, TestPlainText)
                };

            string templateClass = factory.CreateTemplateClass(segments);

            Assert.IsTrue(templateClass.Contains(TestPlainText));
        }

        [TestMethod]
        public void InsertsCodeBlock()
        {
            var factory = new TemplateClassFactory();
            var segments = new List<TemplateSegment>
                {
                    new TemplateSegment(TemplateSegmentType.CodeBlock, TestCodeBlock)
                };

            string templateClass = factory.CreateTemplateClass(segments);

            Assert.IsTrue(templateClass.Contains(TestCodeBlock));
        }

        [TestMethod]
        public void InsertsInRightOrder()
        {
            var factory = new TemplateClassFactory();
            var segments = new List<TemplateSegment>
                {
                    new TemplateSegment(TemplateSegmentType.PlainText, TestPlainText),
                    new TemplateSegment(TemplateSegmentType.CodeBlock, TestCodeBlock)
                };

            string templateClass = factory.CreateTemplateClass(segments);

            int indexOfTestPlainText = templateClass.IndexOf(TestPlainText, System.StringComparison.Ordinal);
            int indexOfTestCodeBlock = templateClass.IndexOf(TestCodeBlock, System.StringComparison.Ordinal);

            Assert.IsTrue(indexOfTestPlainText < indexOfTestCodeBlock, "Plain text comes before code block");
        }
    }
}
