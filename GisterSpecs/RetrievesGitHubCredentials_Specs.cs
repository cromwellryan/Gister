using System.IO;
using EchelonTouchInc.Gister;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class RetrievesGitHubCredentials_Specs
    {
        [Test]
        public void UsernameOnFirstLine()
        {
            var store = new RetrievesGitHubCredentials();
            store.TestPathToCredentials = Path.GetTempFileName();

            File.WriteAllLines(store.TestPathToCredentials, new[] { @"me", @"secret" });

            store.GetCredentials().Username.Should().Equal("me");

        }

        [Test]
        public void PasswordOnSecondLine()
        {
            var store = new RetrievesGitHubCredentials();
            store.TestPathToCredentials = Path.GetTempFileName();

            File.WriteAllLines(store.TestPathToCredentials, new[] { @"me", @"secret" });
            {

                store.GetCredentials().Password.Should().Equal("secret");

            }
        }
    }
}