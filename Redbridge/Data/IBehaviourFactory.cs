namespace Redbridge.Data
{
	public interface IBehaviourFactory<in TIn, out TOut>
	{
		TOut CreateBehaviour(TIn input);
	}
}
