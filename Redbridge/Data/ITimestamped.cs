using System;

namespace Redbridge.Data
{
public interface ITimestamped : IDeletable
{
	DateTime Created { get; set; }

	DateTime Updated { get; set; } }
}
