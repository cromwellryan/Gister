using System;

namespace EchelonTouchInc.Gister.Api
{
    public class UploadsGists : CanReceiveCredentials
    {
        private readonly Action<string> NoOp = s => { };

        private GitHubCredentials gitHubCredentials = new AnonymousGitHubCredentials();

        public UploadsGists()
        {
            GitHubSender = new NoWhereGitHubSender();
            UrlAvailable = NoOp;
            PresentStatusUpdate = NoOp;
        }

        public void Upload(string fileName, string content)
        {

            NotifyStatusChanged(string.Format("Creating gist for {0}", fileName));

            string gistUrl;

            try
            {
                gistUrl = GitHubSender.SendGist(fileName, content, this.gitHubCredentials.Username, this.gitHubCredentials.Password);
            }
            catch (ApplicationException ex)
            {
                NotifyStatusChanged(string.Format("Gist not created.  {0}", ex.Message));
                return;
            }

            UrlAvailable(gistUrl);
            NotifyStatusChanged("Gist created successfully.  Url placed in the clipboard.");
        }

        public void UseCredentials(GitHubCredentials credentials)
        {
            gitHubCredentials = credentials;
        }

        private void NotifyStatusChanged(string message)
        {
            PresentStatusUpdate(message);
        }

        public GitHubSender GitHubSender { get; set; }

        public Action<string> UrlAvailable { get; set; }

        public Action<string> PresentStatusUpdate { get; set; }

        private class AnonymousGitHubCredentials : GitHubCredentials
        {
            public AnonymousGitHubCredentials() : base("", "") { }
        }
    }

}
