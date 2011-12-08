using EchelonTouchInc.Gister.FluentHttp;
using FluentHttp;

namespace EchelonTouchInc.Gister.Api.Credentials
{
    public class GitHubUserCredentialsForFluentHttp : CredentialVisitor
    {
        public void Apply(FluentHttpRequest request, GitHubCredentials credentials)
        {
            var userCreds = (GitHubUserCredentials)credentials;

            request.AuthenticateUsing(new HttpBasicAuthenticator(userCreds.Username, userCreds.Password));
        }
    }
}