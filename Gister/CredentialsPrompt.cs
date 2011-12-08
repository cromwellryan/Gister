namespace EchelonTouchInc.Gister
{
    public interface CredentialsPrompt
    {
        bool? Result { get; }
        string Username { get; }
        string Password { get; }
        void Prompt();
    }
}