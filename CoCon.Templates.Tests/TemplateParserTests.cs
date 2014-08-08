using System;
using System.Collections.ObjectModel;

using CoCon.Templates.Parser;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoCon.Templates.Tests
{
    [TestClass]
    public class TemplateParserTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsArgumentNullExceptionWhenArgumentNull()
        {
            var parser = new TemplateParser();
            parser.Parse(null);
        }

        [TestMethod]
        public void ParsesSingleCodeSegment()
        {
            var parser = new TemplateParser();
            ReadOnlyCollection<TemplateSegment> segments = parser.Parse("<% foo %>");

            Assert.AreEqual(1, segments.Count);
            Assert.AreEqual(" foo ", segments[0].Content);
            Assert.AreEqual(TemplateSegmentType.CodeBlock, segments[0].Type);
        }

        [TestMethod]
        public void ParsesMultiLineCodeSegment()
        {
            const string Template = 
@"<% foo
bar %>";

            var parser = new TemplateParser();
            ReadOnlyCollection<TemplateSegment> segments = parser.Parse(Template);

            const string ExpectedSegmentContent = 
@" foo
bar ";

            Assert.AreEqual(1, segments.Count);
            Assert.AreEqual(ExpectedSegmentContent, segments[0].Content);
            Assert.AreEqual(TemplateSegmentType.CodeBlock, segments[0].Type);
        }

        [TestMethod]
        public void ParsesSinglePlainTextSegment()
        {
            var parser = new TemplateParser();
            ReadOnlyCollection<TemplateSegment> segments = parser.Parse("foo");

            Assert.AreEqual(1, segments.Count);
            Assert.AreEqual("foo", segments[0].Content);
            Assert.AreEqual(TemplateSegmentType.PlainText, segments[0].Type);
        }

        [TestMethod]
        public void ParsesMultiLineTextSegment()
        {
            const string Template =
@"foo
bar";

            var parser = new TemplateParser();
            ReadOnlyCollection<TemplateSegment> segments = parser.Parse(Template);

            const string ExpectedSegmentContent =
@"foo
bar";

            Assert.AreEqual(1, segments.Count);
            Assert.AreEqual(ExpectedSegmentContent, segments[0].Content);
            Assert.AreEqual(TemplateSegmentType.PlainText, segments[0].Type);
        }
    }
}
