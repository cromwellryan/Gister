using System;
using System.IO;

namespace EchelonTouchInc.Gister
{
    public class GitHubFileSystemCredentialStore
    {

        private string GetPathToCredentialsFile()
        {
            return IsTestPathProvided() ?  TestPathToCredentials : VsProfileCredentials();
        }

        private string VsProfileCredentials()
        {
            var profilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            return Path.Combine(profilePath, "github.creds");
        }

        private bool IsTestPathProvided()
        {
            return !string.IsNullOrEmpty(TestPathToCredentials);
        }

        public string TestPathToCredentials { get; set; }

        public GitHubCredentials GetCredentials()
        {
            var pathToCredentialsFile = GetPathToCredentialsFile();
            var lines = File.ReadAllLines(pathToCredentialsFile);

            return DecodeGitHubCredentialsFromFile(lines);
        }

        private GitHubCredentials DecodeGitHubCredentialsFromFile(string[] lines)
        {
            return new GitHubCredentials(lines[0], lines[1]);
        }
    }


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