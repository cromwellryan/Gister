using System;
using System.Threading.Tasks;

namespace EchelonTouchInc.Gister.Api
{
    public class GistApi
    {
        private readonly Action<string> NoOp = s => { };

        public GistApi()
        {
            GitHubSender = new NoWhereGitHubSender();
            UrlAvailable = NoOp;
            PresentStatusUpdate = NoOp;
        }

        public void Create(GitHubCredentials gitHubCredentials, string fileName, string content)
        {
            NotifyStatusChanged(string.Format("Creating gist for {0}", fileName));

            string gistUrl;

            try
            {
                gistUrl = GitHubSender.SendGist(fileName, content, gitHubCredentials.Username, gitHubCredentials.Password);
            }
            catch (ApplicationException ex)
            {
                NotifyStatusChanged(string.Format("Gist not created.  {0}", ex.Message));
                return;
            }

            UrlAvailable(gistUrl);
            NotifyStatusChanged("Gist created successfully.  Url placed in the clipboard.");
        }

        private void NotifyStatusChanged(string message)
        {
            PresentStatusUpdate(message);
        }

        public UpdatesStatus UpdatesStatus { get; set; }

        public GitHubSender GitHubSender { get; set; } 
        public Action<string> UrlAvailable { get; set; }

        public Action<string> PresentStatusUpdate { get; set; }
    }
}
