using System;
namespace Redbridge.Dynamic.Linq
{
	public class DynamicProperty
	{
		string name;
		Type type;

		public DynamicProperty(string name, Type type)
		{
            this.name = name ?? throw new ArgumentNullException(nameof(name));
			this.type = type ?? throw new ArgumentNullException(nameof(type));
		}

		public string Name
		{
			get { return name; }
		}

		public Type Type
		{
			get { return type; }
		}
	}
}
