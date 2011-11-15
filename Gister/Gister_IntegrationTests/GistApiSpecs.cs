using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using EchelonTouchInc.Gister.Api;
using EchelonTouchInc.Gister.FluentHttp;
using FluentHttp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should.Fluent;

namespace Gister_IntegrationTests
{
    [TestClass]
    public class GistApiSpecs
    {
        [TestMethod]
        public void DoesntThrow___Cheater()
        {
            new GistApi().Create("using System;\n\npublic class Class1\n{\n\tpublic Class1()\n\t{\n\t}\n}\n");
        }
    }
}
