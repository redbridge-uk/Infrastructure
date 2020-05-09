using System;

namespace Redbridge.Data
{
public interface IDeletable
{
	DateTime? Deleted { get; set; } }
}
