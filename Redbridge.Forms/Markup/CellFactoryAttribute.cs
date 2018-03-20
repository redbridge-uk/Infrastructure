using System;
namespace Redbridge.Forms.Markup
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CellFactoryAttribute : Attribute
    {
        public CellFactoryAttribute (Type factoryType, string name)
        {
            FactoryType = factoryType;
            Name = name;
        }

        public string Name { get; }

        public Type FactoryType { get; }
    }
}
