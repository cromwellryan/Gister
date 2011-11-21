namespace EchelonTouchInc.Gister.Api
{
    public interface GitHubSender
    {
        string SendGist(string fileName, string content, string githubusername, string githubpassword);
    }
}