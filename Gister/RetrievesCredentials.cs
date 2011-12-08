using EchelonTouchInc.Gister.Api;
using EchelonTouchInc.Gister.Api.Credentials;

namespace EchelonTouchInc.Gister
{
    public interface RetrievesCredentials
    {
        bool IsAvailable();
        GitHubCredentials Retrieve();
    }
}