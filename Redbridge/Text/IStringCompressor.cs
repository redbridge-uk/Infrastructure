namespace Redbridge.Text
{
	public interface IStringCompressor
	{
		string Compress(string input);

		string Decompress(string input);
	}
}
