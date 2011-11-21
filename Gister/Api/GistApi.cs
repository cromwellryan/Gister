namespace EchelonTouchInc.Gister.Api
{
    public class GistApi
    {
        public GistApi()
        {
            StatusUpdates = new NoStatusUpdates();
            GitHubSender = new NoWhereGitHubSender();
        }
        public void Create(string fileName, string content, string githubusername, string githubpassword)
        {
            StatusUpdates.NotifyUserThat(string.Format("Creating gist for {0}", fileName));

            GitHubSender.SendGist(fileName, content, githubusername, githubpassword);


            StatusUpdates.NotifyUserThat("Gist created successfully.  Url placed in the clipboard.");
        }

        public StatusUpdates StatusUpdates { get; set; }

        public GitHubSender GitHubSender { get; set; }
    }
}
