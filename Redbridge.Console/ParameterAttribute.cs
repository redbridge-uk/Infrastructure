namespace Redbridge.Console
{
    public abstract class ParameterAttribute : PropertyArgumentAttribute
    {
        protected ParameterAttribute(string name) : base(name) { }
        
        public override string ParameterDisplay => $"{base.ParameterDisplay}=({base.ParameterName})";
    }
}
