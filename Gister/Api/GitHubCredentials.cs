namespace EchelonTouchInc.Gister.Api
{
    public class GitHubCredentials
    {
        public GitHubCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Password { get; private set; }

        public string Username { get; private set; }
    }
}