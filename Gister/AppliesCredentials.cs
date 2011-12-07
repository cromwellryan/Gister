using EchelonTouchInc.Gister.Api;

namespace EchelonTouchInc.Gister
{
    public interface AppliesCredentials
    {
        void Apply(CanReceiveCredentials receiver);
        bool IsAvailable();
    }
}