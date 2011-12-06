using EchelonTouchInc.Gister.Api;
using NUnit.Framework;

namespace GisterSpecs
{
    [TestFixture]
    public class EasyIntegrationTest
    {
        [Test]
        [Ignore("This is for playing.")]
        public void DoWop()
        {
            new UploadsGists { GitHubSender = new HttpGitHubSender() }.Create(new GitHubCredentials("get", "real"), "wopwop.js", "dowop");
        }
    }
}
