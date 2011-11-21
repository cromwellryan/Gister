using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class CreatingTheGistApi
    {
        [Test]
        public void WillComeWithAnHttpSender()
        {
            GistApiFactory.CreateGistApi().GitHubSender.Should().Be.OfType<HttpGitHubSender>();
        }
    }
}
