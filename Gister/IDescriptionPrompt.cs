namespace EchelonTouchInc.Gister
{
    public interface IDescriptionPrompt
    {
        bool GistPrivate { get; }
        string Description { get; }
        void Prompt();
    }
}
