using System;
namespace Redbridge.SDK
{
public interface ITimestamped : IDeletable
{
	DateTime Created { get; set; }

	DateTime Updated { get; set; } }
}
