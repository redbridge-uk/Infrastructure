namespace Redbridge.Identity
{
    public interface IAuthenticationClientFactory
    {
        IAuthenticationClient Create (string type);
    }
}
