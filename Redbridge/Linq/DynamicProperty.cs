using System;

namespace Redbridge.Linq
{
	public class DynamicProperty
	{
        readonly string _name;
        readonly Type _type;

		public DynamicProperty(string name, Type type)
		{
            this._name = name ?? throw new ArgumentNullException(nameof(name));
			this._type = type ?? throw new ArgumentNullException(nameof(type));
		}

		public string Name => _name;

        public Type Type => _type;
    }
}
