using System.IO;
using EchelonTouchInc.Gister;
using EchelonTouchInc.Gister.Api.Credentials;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class CachesGitHubCredentials_Specs
    {
        private CachesGitHubCredentials cache;

        [SetUp]
        public void Before()
        {
            cache = new CachesGitHubCredentials
            {
                Encrypt = s => s,
                Decrypt = s => s
            };
        }

        [Test]
        public void UsernameOnFirstLine()
        {
            cache.TestPathToCredentials = Path.GetTempFileName();
            File.WriteAllLines(cache.TestPathToCredentials, new[] { @"me", @"secret" });

            var credentials = cache.Retrieve().Should().Be.OfType<GitHubUserCredentials>() as GitHubUserCredentials;

            credentials.Username.Should().Equal("me");
        }

        [Test]
        public void PasswordOnSecondLine()
        {
            cache.TestPathToCredentials = Path.GetTempFileName();
            File.WriteAllLines(cache.TestPathToCredentials, new[] { @"me", @"secret" });

            var credentials = cache.Retrieve().Should().Be.OfType<GitHubUserCredentials>() as GitHubUserCredentials;

            credentials.Password.Should().Equal("secret");
        }

        [Test]
        public void IsNotAvailableIfFileNotFound()
        {
            cache.TestPathToCredentials = Path.GetTempFileName();
            File.Delete(cache.TestPathToCredentials);

            cache.IsAvailable().Should().Be.False();
        }

        [Test]
        public void CachesUserCredentials()
        {
            cache.TestPathToCredentials = Path.GetTempFileName();
            File.Delete(cache.TestPathToCredentials);

            cache.Cache(new GitHubUserCredentials("a", "b"));

            File.Exists(cache.TestPathToCredentials).Should().Be.True();
        }

        [Test]
        public void DoesNotCacheAnonymousCredentials()
        {
            cache.TestPathToCredentials = Path.GetTempFileName();
            File.Delete(cache.TestPathToCredentials);

            cache.Cache(GitHubCredentials.Anonymous);

            File.Exists(cache.TestPathToCredentials).Should().Be.False();
        }

        [Test]
        public void ReturnsGitHubUserCredentials()
        {
            cache.TestPathToCredentials = Path.GetTempFileName();
            File.WriteAllLines(cache.TestPathToCredentials, new[] { @"me", @"secret" });

            cache.Retrieve().Should().Be.OfType<GitHubUserCredentials>();
        }

        [Test]
        public void WillEncryptCache()
        {
            cache.TestPathToCredentials = Path.GetTempFileName();

            cache.Encrypt = (input) => "encrypted";
            cache.Decrypt = input => input;

            cache.Cache(new GitHubUserCredentials("a", "b"));

            var creds = cache.Retrieve() as GitHubUserCredentials;

            creds.Username.Should().Equal("encrypted");
            creds.Password.Should().Equal("encrypted");
        }

        [Test]
        public void WillDecryptCache()
        {
            cache.TestPathToCredentials = Path.GetTempFileName();

            cache.Encrypt = input => input;
            cache.Decrypt = input => "decrypted";

            cache.Cache(new GitHubUserCredentials("b", "a"));

            var creds = cache.Retrieve() as GitHubUserCredentials;

            creds.Username.Should().Equal("decrypted");
            creds.Password.Should().Equal("decrypted");
        }
    }
}