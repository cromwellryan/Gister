using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EchelonTouchInc.Gister;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class FileSystemCredentials
    {
        [Test]
        public void UsernameOnFirstLine()
        {
            var path = Path.GetTempFileName();
            File.WriteAllLines(path, new[] {@"me", @"secret"});
            var store = new GitHubFileSystemCredentialStore();

            store.TestPathToCredentials = path;

            store.GetCredentials().Username.Should().Equal("me");
            store.GetCredentials().Password.Should().Equal("secret");

        }
    }
}
