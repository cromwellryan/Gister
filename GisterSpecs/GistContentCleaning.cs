using System;
using System.Text;
using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class GistContentCleaning
    {
        [Test]
        public void TabsWillBeEscaped()
        {
            string result = new GistCleaner().Clean(@"\tusing System;");

            result.Should().Equal("\\tusing System;");
        }

        [Test]
        public void WindowsLineBreakssWillBeEscaped()
        {
            string result = new GistCleaner().Clean(@"\tusing System;
using System.Linq;

public class SomeClass {
var something = ""kudos"";
}");

            result.Should().Equal("\\tusing System;\\r\\nusing System.Linq;\\r\\n\\r\\npublic class SomeClass {\\r\\nvar something = \\\"kudos\\\";\\r\\n}");
        }
        
    }
}
