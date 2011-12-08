using FluentHttp;

namespace EchelonTouchInc.Gister.Api.Credentials
{
    public interface CredentialVisitor
    {
        void Apply(FluentHttpRequest request, GitHubCredentials credentials);
    }
}