namespace EchelonTouchInc.Gister.Api
{
    public interface CanReceiveCredentials
    {
        void UseCredentials(GitHubCredentials credentials);
    }
}