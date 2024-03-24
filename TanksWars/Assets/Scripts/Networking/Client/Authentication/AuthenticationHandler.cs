using System.Threading.Tasks;
using Unity.Services.Authentication;
using static Networking.Client.Authentication.AuthenticationState;

namespace Networking.Client.Authentication
{
    public static class AuthenticationHandler
    {
        private const int reauthenticationWaitTime = 1000;
        private static AuthenticationState authenticationState = NotAuthenticated;

        public static AuthenticationState AuthenticationState => authenticationState;

        public static async Task<AuthenticationState> StartAuthenticationAsync(int maxTries = 5)
        {
            if (authenticationState == Authenticated)
            {
                return authenticationState;
            }

            authenticationState = Authenticating;

            int tries = 0;

            while (authenticationState == Authenticating && tries < maxTries)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                if (AuthenticationService.Instance.IsSignedIn && AuthenticationService.Instance.IsAuthorized)
                {
                    authenticationState = Authenticated;
                }

                tries++;

                await Task.Delay(reauthenticationWaitTime);
            }

            return authenticationState;
        }
    }
}
