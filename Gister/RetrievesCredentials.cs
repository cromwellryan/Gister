using EchelonTouchInc.Gister.Api;

namespace EchelonTouchInc.Gister
{
    public interface RetrievesCredentials
    {
        bool IsAvailable();
        GitHubCredentials Retrieve();
    }
}