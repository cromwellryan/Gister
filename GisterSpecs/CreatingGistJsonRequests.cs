using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class CreatingGistJsonRequests
    {
        [Test]
        public void CreatesJsonDocumentForOneFile()
        {
            new GistJson().CreateFrom("myfile.cs", "Some super sweet gistiness").Should().Equal(
                @"{
  ""public"": true,
  ""files"": {
    ""myfile.cs"": {
      ""content"": ""Some super sweet gistiness""
    }
  }
}");

        }

    }
}
