namespace EchelonTouchInc.Gister.Api.Credentials
{
    public abstract class GitHubCredentials
    {
        static GitHubCredentials()
        {
            Anonymous = new AnonymousGitHubCredentials();
        }

        public static GitHubCredentials Anonymous { get; private set; }
    }

    public class AnonymousGitHubCredentials : GitHubCredentials { }

    public class GitHubUserCredentials : GitHubCredentials
    {
        public GitHubUserCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; private set; }

        public string Password { get; private set; }
    }
}