using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    public class UploadsGists_Specs
    {
        [Test]
        public void UploadTheGistToGitHub()
        {
            var gitHubSender = new MockGitHubSender();
            var gistApi = new UploadsGists { GitHubSender = gitHubSender };

            gistApi.Create("file3.cs", "My oh my");

            gitHubSender.SentAGist.Should().Be.True();
        }

        [Test]
        public void GistUrlWillBeAvaible()
        {
            string actualUrl = null;

            var gitHubSender = new MockGitHubSender() { ResultingUrl = "http://gist.github.com/123" };
            var gistApi = new UploadsGists { GitHubSender = gitHubSender, UrlAvailable = url=>actualUrl = url };

            gistApi.Create("file3.cs", "My oh my");

            actualUrl.Should().Equal("http://gist.github.com/123");

        }

        [Test]
        public void WillBeSentWithAppliedCredentials()
        {
            var gitHubSender = new MockGitHubSender();

            var uploads = new UploadsGists{GitHubSender =gitHubSender};

            uploads.UseCredentials(new GitHubCredentials("something", "secret"));

            uploads.Create("file4.cs", "gee wizz");

            gitHubSender.LastUsernameUsed.Should().Equal("something");
            gitHubSender.LastPasswordUsed.Should().Equal("secret");
        }


    }
}