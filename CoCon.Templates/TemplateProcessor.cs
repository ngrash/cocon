using System;
using System.Reflection;

namespace CoCon.Templates
{
    /// <summary>
    /// Processes a runtime generated template type.
    /// </summary>
    public class TemplateProcessor
    {
        /// <summary>
        /// Processes the template.
        /// </summary>
        /// <param name="templateType">Type of the template.</param>
        /// <returns>Result of the processing, i.e. the transformed template.</returns>
        public string ProcessTemplate(Type templateType)
        {
            object instance = Activator.CreateInstance(templateType);

            MethodInfo processMethod = templateType.GetMethod("Process");
            var result = (string)processMethod.Invoke(instance, null);

            return result;
        }
    }
}