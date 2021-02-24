using System;

namespace Redbridge.ApiManagement
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ApiAttribute :Attribute
    {
        public ApiAttribute()
        {
        }

        public ApiAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
