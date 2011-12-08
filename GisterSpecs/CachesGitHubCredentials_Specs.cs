using System.IO;
using EchelonTouchInc.Gister;
using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class CachesGitHubCredentials_Specs
    {
        [Test]
        public void UsernameOnFirstLine()
        {
            var cache = new CachesGitHubCredentials();

            cache.TestPathToCredentials = Path.GetTempFileName();
            File.WriteAllLines(cache.TestPathToCredentials, new[] { @"me", @"secret" });

            var credentials = cache.Retrieve().Should().Be.OfType<GitHubUserCredentials>() as GitHubUserCredentials;

            credentials.Username.Should().Equal("me");
        }

        [Test]
        public void PasswordOnSecondLine()
        {
            var cache = new CachesGitHubCredentials();

            cache.TestPathToCredentials = Path.GetTempFileName();
            File.WriteAllLines(cache.TestPathToCredentials, new[] { @"me", @"secret" });

            var credentials = cache.Retrieve().Should().Be.OfType<GitHubUserCredentials>() as GitHubUserCredentials;

            credentials.Password.Should().Equal("secret");
        }

        [Test]
        public void IsNotAvailableIfFileNotFound()
        {
            var cache = new CachesGitHubCredentials();

            cache.TestPathToCredentials = Path.GetTempFileName();
            File.Delete(cache.TestPathToCredentials);

            cache.IsAvailable().Should().Be.False();
        }

        [Test]
        public void CachesUserCredentials()
        {
            var cache = new CachesGitHubCredentials();

            cache.TestPathToCredentials = Path.GetTempFileName();
            File.Delete(cache.TestPathToCredentials);

            cache.Cache(new GitHubUserCredentials("a", "b"));

            File.Exists(cache.TestPathToCredentials).Should().Be.True();
        }

        [Test]
        public void DoesNotCacheAnonymousCredentials()
        {
            var cache = new CachesGitHubCredentials();

            cache.TestPathToCredentials = Path.GetTempFileName();
            File.Delete(cache.TestPathToCredentials);

            cache.Cache(GitHubCredentials.Anonymous);

            File.Exists(cache.TestPathToCredentials).Should().Be.False();
        }

        [Test]
        public void ReturnsGitHubUserCredentials()
        {
            var cache = new CachesGitHubCredentials();

            cache.TestPathToCredentials = Path.GetTempFileName();
            File.WriteAllLines(cache.TestPathToCredentials, new[] { @"me", @"secret" });

             cache.Retrieve().Should().Be.OfType<GitHubUserCredentials>();
        }
    }
}