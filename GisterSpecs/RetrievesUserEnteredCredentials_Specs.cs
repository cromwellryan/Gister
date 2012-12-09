using EchelonTouchInc.Gister;
using EchelonTouchInc.Gister.Api;
using EchelonTouchInc.Gister.Api.Credentials;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class RetrievesUserEnteredCredentials_Specs
    {
        [Test]
        public void WillApplyCredentialsIfUserOks()
        {
            var applies = new RetrievesUserEnteredCredentials();

            applies.CreatePrompt = () =>
                                       {
                                           var prompt = new MockCredentialPrompt();

                                           prompt.RespondWith("it", "rocks");
                                           prompt.Result = true;

                                           return prompt;
                                       };

            var credentials = applies.Retrieve().Should().Be.OfType<GitHubUserCredentials>() as GitHubUserCredentials;

            credentials.Username.Should().Equal("it");
            credentials.Password.Should().Equal("rocks");
        }

        [Test]
        public void WillNotApplyCredentialsIfUserCancels()
        {
            var applies = new RetrievesUserEnteredCredentials();

            applies.CreatePrompt = () =>
                                       {
                                           var prompt = new MockCredentialPrompt();

                                           prompt.RespondWith("it", "didn't");
                                           prompt.Result = false;

                                           return prompt;
                                       };

            var receiver = applies.Retrieve();

            receiver.Should().Be.OfType<AnonymousGitHubCredentials>();
        }
    }

    public class MockCredentialPrompt : ICredentialsPrompt
    {
        public bool? Result { get; set; }

        public void RespondWith(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void Prompt()
        {
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
    }
}
