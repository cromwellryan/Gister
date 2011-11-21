using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    public class UploadingGists
    {
        [Test]
        public void UploadTheGistToGitHub()
        {
            var gitHubSender = new MockGitHubSender();
            var gistApi = new GistApi { GitHubSender = gitHubSender };

            gistApi.Create("file3.cs", "My oh my", "get", "real");

            gitHubSender.SentAGist.Should().Be.True();
        }
    }
}