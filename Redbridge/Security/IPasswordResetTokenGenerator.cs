using System;
namespace Redbridge.Security
{
	public interface IPasswordResetTokenGenerator
	{
		string GenerateToken(); 
	}
}
