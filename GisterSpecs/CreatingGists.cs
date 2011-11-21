using System.Collections.Generic;
using System.Linq;
using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class CreatingGists
    {
        [Test]
        public void WillTellTheUserWhenItStarts()
        {
            var statusUpdates = new MockStatusUpdates();
            var gistApi = new GistApi { StatusUpdates = statusUpdates };

            gistApi.Create("file1.cs", "Dum diddy, dum diddy", "cromwellryan", "ship it n0w!");

            // Really annoying that ShouldFluent Contains wasn't working...
            statusUpdates.LastUpdate.FirstOrDefault(x => x == "Creating gist for file1.cs").Should().Not.Be.Null();
        }

        [Test]
        public void WillTellTheUserWhenItsSuccessful()
        {
            var statusUpdates = new MockStatusUpdates();
            var gistApi = new GistApi { StatusUpdates = statusUpdates };

            gistApi.Create("file2.cs", "Zippity do dah, zippity ah", "cromwellryan", "ship it n0w!");

            statusUpdates.LastUpdate.FirstOrDefault(x => x == "Gist created successfully.  Url placed in the clipboard.").Should().Not.Be.Null();
        }
    }

    public class MockStatusUpdates : StatusUpdates
    {
        public List<string> LastUpdate = new List<string>();
        public void NotifyUserThat(string messagetotelltheuser)
        {
            LastUpdate.Add(messagetotelltheuser);
        }
    }
}
