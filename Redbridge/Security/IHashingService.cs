using System;
namespace Redbridge.Security
{
    public interface IHashingService
    {
        string CreateHash (string input);
    }
}
