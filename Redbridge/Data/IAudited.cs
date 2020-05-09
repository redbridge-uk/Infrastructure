namespace Redbridge.Data
{
	public interface IAudited<TUserKey> : ITimestamped
		where TUserKey : struct
	{
		TUserKey CreatedBy { get; set; }
		TUserKey UpdatedBy { get; set; }
		TUserKey? DeletedBy { get; set; }
	}
}
