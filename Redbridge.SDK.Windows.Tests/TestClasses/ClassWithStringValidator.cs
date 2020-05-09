
using Redbridge.Validation.Markup;

namespace Redbridge.Windows.Tests.TestClasses
{
    public class ClassWithStringValidator
    {
        [StringValidator(IsRequired=true)]
        public string Name
        {
            get;
            set;
        }
    }
}
