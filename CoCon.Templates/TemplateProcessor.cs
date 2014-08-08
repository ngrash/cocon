using System;
using System.Reflection;

namespace CoCon.Templates
{
    public class TemplateProcessor
    {
        public string ProcessTemplate(Type templateType)
        {
            object instance = Activator.CreateInstance(templateType);

            MethodInfo processMethod = templateType.GetMethod("Process");
            var result = (string)processMethod.Invoke(instance, null);

            return result;
        }
    }
}