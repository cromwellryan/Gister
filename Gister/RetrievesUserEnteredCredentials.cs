using System;
using EchelonTouchInc.Gister.Api;
using EchelonTouchInc.Gister.Api.Credentials;

namespace EchelonTouchInc.Gister
{
    public class RetrievesUserEnteredCredentials : IRetrievesCredentials
    {
        public RetrievesUserEnteredCredentials()
        {
            CreatePrompt = () => new GitHubCredentialsPrompt();
        }

        private GitHubCredentials GetCredentials()
        {
            var prompt = CreatePrompt();

            prompt.Prompt();
            if (prompt.Result != true)
                return GitHubCredentials.Anonymous;

            return new GitHubUserCredentials(prompt.Username, prompt.Password);
        }

        public bool IsAvailable()
        {
            return true;
        }

        public Func<ICredentialsPrompt> CreatePrompt { get; set; }

        public GitHubCredentials Retrieve()
        {
            return GetCredentials();
        }
    }
}