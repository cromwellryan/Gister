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
            UrlAvailable = s => { };
            Uploaded = () => { };
            CredentialsAreBad = () => { };
        }

        public void Upload(string fileName, string content)
        {


            string gistUrl;

            try
            {
                gistUrl = GitHubSender.SendGist(fileName, content, this.gitHubCredentials.Username, this.gitHubCredentials.Password);
            }
            catch (GitHubUnauthorizedException ex)
            {

                CredentialsAreBad();
                return;
            }


            Uploaded();
            UrlAvailable(gistUrl);
        }

        public void UseCredentials(GitHubCredentials credentials)
        {
            gitHubCredentials = credentials;
        }

        public GitHubSender GitHubSender { get; set; }

        public Action<string> UrlAvailable { get; set; }

        public Action Uploaded { get; set; }

        public Action CredentialsAreBad { get; set; }

        private class AnonymousGitHubCredentials : GitHubCredentials
        {
            public AnonymousGitHubCredentials() : base("", "") { }
        }
    }

}
