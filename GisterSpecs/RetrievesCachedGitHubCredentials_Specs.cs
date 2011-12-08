using System.IO;
using EchelonTouchInc.Gister;
using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class RetrievesCachedGitHubCredentials_Specs
    {
        [Test]
        public void UsernameOnFirstLine()
        {
            var cache = new CachesGitHubCredentials();

            cache.TestPathToCredentials = Path.GetTempFileName();
            File.WriteAllLines(cache.TestPathToCredentials, new[] { @"me", @"secret" });

            var credentials = cache.Retrieve();

            credentials.Username.Should().Equal("me");
        }

        [Test]
        public void PasswordOnSecondLine()
        {
            var cache = new CachesGitHubCredentials();

            cache.TestPathToCredentials = Path.GetTempFileName();
            File.WriteAllLines(cache.TestPathToCredentials, new[] { @"me", @"secret" });

            var credentials = cache.Retrieve();

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
    }

    public class MockCanReceiveCredentials : CanReceiveCredentials
    {
        public string Username { get; private set; }

        public string Password { get; private set; }

        public void UseCredentials(GitHubCredentials credentials)
        {
            Username = credentials.Username;
            Password = credentials.Password;
        }
    }
}