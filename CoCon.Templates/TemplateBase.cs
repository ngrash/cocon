using System.Text;

namespace CoCon.Templates
{
    /// <summary>
    /// Abstract base class of all runtime generated template classes.
    /// </summary>
    public abstract class TemplateBase
    {
        private readonly StringBuilder _contentBuilder = new StringBuilder();

        /// <summary>
        /// Processes the template.
        /// </summary>
        /// <returns>The result of the processed template.</returns>
        public string Process()
        {
            ProcessInternal();

            return _contentBuilder.ToString();
        }

        /// <summary>
        /// This method is overridden by runtime generated template classes and contains the template specific code.
        /// </summary>
        protected abstract void ProcessInternal();

        /// <summary>
        /// Writes the specified content to the template output.
        /// </summary>
        /// <param name="content">The content to be written to the template output.</param>
        protected void Write(string content)
        {
            _contentBuilder.Append(content);
        }
    }
}
