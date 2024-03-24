namespace Networking.Client.Authentication
{
    public enum AuthenticationState 
    {
        NotAuthenticated,
        Authenticating,
        Authenticated,
        Error,
        TimeOut
    }
}