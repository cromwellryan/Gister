namespace EchelonTouchInc.Gister.Api
{
    public static class GistApiFactory
    {
        public static GistApi CreateGistApi()
        {
            return new GistApi {GitHubSender = new HttpGitHubSender()};
        }
    }
}