using System;
using EchelonTouchInc.Gister.Api;

namespace EchelonTouchInc.Gister
{
    public class AppliesUserEnteredCredentials : AppliesCredentials
    {
        public AppliesUserEnteredCredentials()
        {
            CreatePrompt = () => new GitHubCredentialsPrompt();
        }
        public void Apply(CanReceiveCredentials receiver)
        {
            var prompt = CreatePrompt();

            prompt.Prompt();
            if (prompt.Result != true)
                return;

            var credentials = new GitHubCredentials(prompt.Username, prompt.Password);
            receiver.UseCredentials(credentials);
        }

        public bool IsAvailable()
        {
            return true;
        }

        public Func<CredentialsPrompt> CreatePrompt { get; set; }
    }
}