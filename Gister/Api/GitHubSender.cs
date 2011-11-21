namespace EchelonTouchInc.Gister.Api
{
    public interface GitHubSender
    {
        void SendGist(string fileName, string content, string githubusername, string githubpassword);
    }
}