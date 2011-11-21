using System;

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

            try
            {
                GitHubSender.SendGist(fileName, content, githubusername, githubpassword);
            }
            catch (ApplicationException ex)
            {
                StatusUpdates.NotifyUserThat(string.Format("Gist not created.  {0}", ex.Message));
                return;
            }

            StatusUpdates.NotifyUserThat("Gist created successfully.  Url placed in the clipboard.");
        }

        public StatusUpdates StatusUpdates { get; set; }

        public GitHubSender GitHubSender { get; set; }
    }
}
