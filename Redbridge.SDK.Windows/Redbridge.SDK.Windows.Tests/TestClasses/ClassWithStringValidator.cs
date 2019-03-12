
using Redbridge.Validation.Markup;

namespace Easilog.Tests.TestClasses
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
