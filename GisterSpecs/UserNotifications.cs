using System;
using System.Collections.Generic;
using System.Linq;
using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class UserNotifications
    {
        [Test]
        public void WillTellTheUserWhenItStarts()
        {
            var statusUpdates = new MockStatusUpdates();
            var gistApi = new GistApi { StatusUpdates = statusUpdates };

            gistApi.Create("file1.cs", "Dum diddy, dum diddy", "get", "real");

            // Really annoying that ShouldFluent Contains wasn't working...
            statusUpdates.Notifications.FirstOrDefault(x => x == "Creating gist for file1.cs").Should().Not.Be.Null();
        }

        [Test]
        public void WillTellTheUserWhenItsSuccessful()
        {
            var statusUpdates = new MockStatusUpdates();
            var gistApi = new GistApi { StatusUpdates = statusUpdates };

            gistApi.Create("file2.cs", "Zippity do dah, zippity ah", "get", "real");

            statusUpdates.Notifications.FirstOrDefault(x => x == "Gist created successfully.  Url placed in the clipboard.")
                .Should().Not.Be.Null();
        }

        [Test]
        public void WillTellTheUserWhenItScrewsUp()
        {
            var statusUpdates = new MockStatusUpdates();
            var sender = new MockGitHubSender();
            sender.FailWith("Your password is terrible.");

            var gistApi = new GistApi { StatusUpdates = statusUpdates, GitHubSender = sender };

            gistApi.Create("file3.cs", "Out of fun kids songs.", "me", "nahnah");

            statusUpdates.LastUpdate.Should().Equal("Gist not created.  Your password is terrible.");
        }
    }

    public class MockGitHubSender : GitHubSender
    {
        private string failureStatusDescription;

        public bool SentAGist { get; private set; }

        public void SendGist(string fileName, string content, string githubusername, string githubpassword)
        {
            if (ShouldThrow())
                throw new ApplicationException(failureStatusDescription);

            SentAGist = true;
        }

        private bool ShouldThrow()
        {
            return !string.IsNullOrEmpty(failureStatusDescription);
        }

        public void FailWith(string statusDescription)
        {
            failureStatusDescription = statusDescription;
        }
    }

    public class MockStatusUpdates : StatusUpdates
    {
        public List<string> Notifications = new List<string>();

        public string LastUpdate { get; private set; }

        public void NotifyUserThat(string messagetotelltheuser)
        {
            Notifications.Add(messagetotelltheuser);
            LastUpdate = messagetotelltheuser;
        }
    }
}
