using EchelonTouchInc.Gister.Api;
using EchelonTouchInc.Gister.Api.Credentials;

namespace EchelonTouchInc.Gister
{
    public interface IRetrievesCredentials
    {
        bool IsAvailable();
        GitHubCredentials Retrieve();
    }
}