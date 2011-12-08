using FluentHttp;

namespace EchelonTouchInc.Gister.Api.Credentials
{
    public class AnonymousGitHubCredentialsForFluentHttp : CredentialVisitor
    {
        public void Apply(FluentHttpRequest request, GitHubCredentials credentials)
        {
        }
    }
}