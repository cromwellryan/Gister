using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class UploadsGists_Specs
    {
        [Test]
        public void UploadTheGistToGitHub()
        {
            var gitHubSender = new MockGitHubSender();
            var gistApi = new UploadsGists { GitHubSender = gitHubSender };

            gistApi.Upload("file3.cs", "My oh my");

            gitHubSender.SentAGist.Should().Be.True();
        }

        [Test]
        public void WillBeSentWithAppliedCredentials()
        {
            var gitHubSender = new MockGitHubSender();

            var uploads = new UploadsGists { GitHubSender = gitHubSender };

            var credentials = new GitHubUserCredentials("something", "secret");
            uploads.UseCredentials(credentials);

            uploads.Upload("file4.cs", "gee wizz");

            gitHubSender.LastCredentialsApplied.Should().Equal(credentials);
        }

        [Test]
        public void GistUrlWillBeAvaible()
        {
            string actualUrl = null;

            var gitHubSender = new MockGitHubSender() { ResultingUrl = "http://gist.github.com/123" };
            var gistApi = new UploadsGists { GitHubSender = gitHubSender };

            gistApi.Uploaded = url => actualUrl = url;
            gistApi.Upload("file3.cs", "My oh my");

            actualUrl.Should().Equal("http://gist.github.com/123");

        }

        [Test]
        public void CompleteOccursAtTheEnd()
        {
            var wasSuccessful = false;

            var uploads = new UploadsGists();

            uploads.Uploaded = url => wasSuccessful = true;

            uploads.Upload("asdf", "qwer");

            wasSuccessful.Should().Be.True();
        }

        [Test]
        public void WillAllowUsToGetInvolvedWhenCredentialsAreBad()
        {
            var sender = new MockGitHubSender();
            sender.FailWith("Blah");

            var uploads = new UploadsGists();
            uploads.GitHubSender = sender;

            var didTellUs = false;
            uploads.CredentialsAreBad = () => didTellUs = true;

            uploads.Upload("asdf", "asdF");

            didTellUs.Should().Be.True();
        }
    }
}