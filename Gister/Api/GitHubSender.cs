namespace EchelonTouchInc.Gister.Api
{
    public interface GitHubSender
    {
        string SendGist(string fileName, string content, GitHubCredentials credentials);
    }
}