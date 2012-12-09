namespace EchelonTouchInc.Gister
{
    public interface ICredentialsPrompt
    {
        bool? Result { get; }
        string Username { get; }
        string Password { get; }
        void Prompt();
    }
}