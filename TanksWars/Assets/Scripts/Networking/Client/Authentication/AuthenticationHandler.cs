using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using static Networking.Client.Authentication.AuthenticationState;

namespace Networking.Client.Authentication
{
    public static class AuthenticationHandler
    {
        private const int ReauthenticationWaitTime = 1000;
        private const int CheckingAuthenticatingProccesWaitTime = 200;

        private static AuthenticationState authenticationState = NotAuthenticated;

        public static AuthenticationState AuthenticationState => authenticationState;

        public static async Task<AuthenticationState> StartAuthenticationAsync(int maxTries = 5)
        {
            if (authenticationState == Authenticated)
            {
                return authenticationState;
            }

            if (authenticationState == Authenticating)
            {
                Debug.LogWarning("Already authenticating");
                await CheckAuthenticatingProcces();
                return authenticationState;
            }

            await SignInAnonymuslyAsync(maxTries);

            return authenticationState;
        }

        private static async Task SignInAnonymuslyAsync(int maxTries)
        {
            authenticationState = Authenticating;

            int tries = 0;

            while (authenticationState == Authenticating && tries < maxTries)
            {
                try
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                    if (AuthenticationService.Instance.IsSignedIn && AuthenticationService.Instance.IsAuthorized)
                    {
                        authenticationState = Authenticated;
                        break;
                    }
                }
                catch (AuthenticationException authException)
                {
                    Debug.LogError(authException);
                    authenticationState = Error;
                }
                catch (RequestFailedException requestException)
                {
                    Debug.LogError(requestException);
                    authenticationState = Error;
                }

                tries++;

                await Task.Delay(ReauthenticationWaitTime);
            }

            if (authenticationState != Authenticated)
            {
                Debug.LogWarning($"Player was not signed in successfully after {maxTries} tries");
                authenticationState = TimeOut;
            }
        }

        private static async Task<AuthenticationState> CheckAuthenticatingProcces()
        {
            while (authenticationState == Authenticating || authenticationState == NotAuthenticated)
            {
                await Task.Delay(CheckingAuthenticatingProccesWaitTime);
            }

            return authenticationState;
        }
    }
}
