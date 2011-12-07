using System;
using System.Linq;
using EchelonTouchInc.Gister.Api;

namespace EchelonTouchInc.Gister
{
    public class AppliesAppropriateCredentials : AppliesCredentials
    {
        private readonly AppliesCredentials[] possibleAppliers;

        public AppliesAppropriateCredentials(params AppliesCredentials[] possibleAppliers)
        {
            this.possibleAppliers = possibleAppliers;
        }

        public void Apply(CanReceiveCredentials receiver)
        {
            var firstAppropriate = (from applier in possibleAppliers
                                    where applier.IsAvailable()
                                    select applier).First();

            firstAppropriate.Apply(receiver);
        }

        public bool IsAvailable()
        {
            throw new NotImplementedException();
        }
    }
}