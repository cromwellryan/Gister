using System.Windows.Forms;

namespace EchelonTouchInc.Gister.Api.Credentials
{
    public interface CredentialsPrompt
    {
        bool? Result { get; }
        string Username { get; }
        string Password { get; }
        void Prompt();
    }
}