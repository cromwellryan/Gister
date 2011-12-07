using System.IO;
using EchelonTouchInc.Gister;
using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class AppliesCachedGitHubCredentials_Specs
    {
        [Test]
        public void UsernameOnFirstLine()
        {
            var cache = new AppliesCachedGitHubCredentials();

            cache.TestPathToCredentials = Path.GetTempFileName();
            File.WriteAllLines(cache.TestPathToCredentials, new[] { @"me", @"secret" });

            var receiver = new MockCanReceiveCredentials();
            cache.Apply(receiver);

            receiver.Username.Should().Equal("me");
        }

        [Test]
        public void PasswordOnSecondLine()
        {
            var cache = new AppliesCachedGitHubCredentials();

            cache.TestPathToCredentials = Path.GetTempFileName();
            File.WriteAllLines(cache.TestPathToCredentials, new[] { @"me", @"secret" });

            var receiver = new MockCanReceiveCredentials();
            cache.Apply(receiver);

            receiver.Password.Should().Equal("secret");
        }

        [Test]
        public void IsNotAvailableIfFileNotFound()
        {
            var cache = new AppliesCachedGitHubCredentials();

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