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
            new GistApi { GitHubSender = new HttpGitHubSender() }.Create("wopwop.js", "dowop", "get",
                                                                         "real");
        }
    }
}
