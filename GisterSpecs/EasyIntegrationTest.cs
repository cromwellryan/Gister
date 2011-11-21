using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EchelonTouchInc.Gister.Api;
using NUnit.Framework;

namespace GisterSpecs
{
    [TestFixture]
    public class EasyIntegrationTest
    {
        [Test]
        public void DoWop()
        {
            new GistApi() { GitHubSender = new HttpGitHubSender() }.Create("wopwop.js", "dowop", "cromwellryan",
                                                                         "ship it n0w!");
        }
    }
}
