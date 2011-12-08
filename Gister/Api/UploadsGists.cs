using System;

namespace EchelonTouchInc.Gister.Api
{
    public class UploadsGists : CanReceiveCredentials
    {

        private GitHubCredentials gitHubCredentials = GitHubCredentials.Anonymous;

        public UploadsGists()
        {
            GitHubSender = new NoWhereGitHubSender();
            Uploaded = url => { };
            CredentialsAreBad = () => { };
        }

        public void Upload(string fileName, string content)
        {
            string gistUrl;

            try
            {
                gistUrl = GitHubSender.SendGist(fileName, content, gitHubCredentials);
            }
            catch (GitHubUnauthorizedException)
            {
                CredentialsAreBad();
                return;
            }

            Uploaded(gistUrl);
        }

        public void UseCredentials(GitHubCredentials credentials)
        {
            gitHubCredentials = credentials;
        }

        public GitHubSender GitHubSender { get; set; }

        public Action<string> Uploaded { get; set; }

        public Action CredentialsAreBad { get; set; }
    }

}
