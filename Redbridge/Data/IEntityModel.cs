using System.Threading.Tasks;

namespace Redbridge.Data
{
	public interface IEntityModel
	{
		Task<int> SaveChangesAsync();
	}
}
