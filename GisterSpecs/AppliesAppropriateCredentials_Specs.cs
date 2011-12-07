using EchelonTouchInc.Gister;
using EchelonTouchInc.Gister.Api;
using NUnit.Framework;
using Should.Fluent;

namespace GisterSpecs
{
    [TestFixture]
    public class AppliesAppropriateCredentials_Specs
    {
        [Test]
        public void AppliesFirstAvailable()
        {
            var notAppropriate = new NotAppropriate();
            var appropriate = new Appropriate();

            var applies = new AppliesAppropriateCredentials( notAppropriate, appropriate);
            var receives = new MockCanReceiveCredentials();

            applies.Apply(receives);

            notAppropriate.WasApplied.Should().Be.False();
            appropriate.WasApplied.Should().Be.True();

        }

        class Appropriate : AppliesCredentials 
        {
            public void Apply(CanReceiveCredentials receiver)
            {
                WasApplied = true;
            }

            public bool IsAvailable()
            {
                return true;
            }

            public bool WasApplied { get; private set; }
        }

        class NotAppropriate : AppliesCredentials
        {
            public void Apply(CanReceiveCredentials receiver)
            {
                WasApplied = false;
            }

            public bool IsAvailable()
            {
                return false;
            }

            public bool WasApplied { get; private set; }
        }
    }
}
