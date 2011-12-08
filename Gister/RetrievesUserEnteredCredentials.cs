using System;
using EchelonTouchInc.Gister.Api;

namespace EchelonTouchInc.Gister
{
    public class RetrievesUserEnteredCredentials : RetrievesCredentials
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

            return new GitHubCredentials(prompt.Username, prompt.Password);
        }

        public bool IsAvailable()
        {
            return true;
        }

        public Func<CredentialsPrompt> CreatePrompt { get; set; }

        public GitHubCredentials Retrieve()
        {
            return GetCredentials();
        }
    }
}