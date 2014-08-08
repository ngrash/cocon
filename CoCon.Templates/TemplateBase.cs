using System.Text;

namespace CoCon.Templates
{
    public abstract class TemplateBase
    {
        private readonly StringBuilder _contentBuilder = new StringBuilder();

        public string Process()
        {
            ProcessInternal();

            return _contentBuilder.ToString();
        }

        protected abstract void ProcessInternal();

        protected void Write(string content)
        {
            _contentBuilder.Append(content);
        }
    }
}
