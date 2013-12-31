using EchelonTouchInc.Gister.Api;
using EchelonTouchInc.Gister.Api.Credentials;

namespace GisterSpecs
{

    public class MockGitHubSender : GitHubSender
    {
        private string failureStatusDescription;

        public bool SentAGist { get; private set; }

        public string ResultingUrl { get; set; }

        public GitHubCredentials LastCredentialsApplied { get; private set; }

        public string SendGist(string fileName, string content,string description,bool isPublic, GitHubCredentials credentials)
        {
            LastCredentialsApplied = credentials;

            if (ShouldThrow())
                throw new GitHubUnauthorizedException(failureStatusDescription);

            SentAGist = true;

            return ResultingUrl;
        }

        private bool ShouldThrow()
        {
            return !string.IsNullOrEmpty(failureStatusDescription);
        }

        public void FailWith(string statusDescription)
        {
            failureStatusDescription = statusDescription;
        }
    }
}
