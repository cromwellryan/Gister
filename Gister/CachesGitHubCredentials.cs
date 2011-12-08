using System;
using System.IO;
using System.Text;
using EchelonTouchInc.Gister.Api.Credentials;

namespace EchelonTouchInc.Gister
{
    public class CachesGitHubCredentials : RetrievesCredentials
    {
        private const string CredentialsFileName = ".githubcreds";

        public CachesGitHubCredentials()
        {
            Encrypt = input =>
                          {
                              var byt = Encoding.UTF8.GetBytes(input);
                              return Convert.ToBase64String(byt);
                          };
            Decrypt = input =>
                          {

                              var byt = Convert.FromBase64String(input);
                              return Encoding.UTF8.GetString(byt);
                          };
        }

        public string TestPathToCredentials { get; set; }

        public Func<string, string> Encrypt { get; set; }

        public Func<string, string> Decrypt { get; set; }

        public bool IsAvailable()
        {
            var path = GetPathToCredentialsFile();

            return File.Exists(path);
        }

        public void AssureNotCached()
        {
            PurgeAnyCache();
        }

        public void Cache(GitHubCredentials credentials)
        {
            var userCredentials = credentials as GitHubUserCredentials;
            if (userCredentials == null) return;

            var path = GetPathToCredentialsFile();

            PurgeAnyCache();

            File.WriteAllLines(path, new[] { Encrypt(userCredentials.Username), Encrypt(userCredentials.Password) });
        }

        public GitHubCredentials Retrieve()
        {
            var path = GetPathToCredentialsFile();

            var lines = File.ReadAllLines(path);

            var username = Decrypt(lines[0]);
            var password = Decrypt(lines[1]);

            return new GitHubUserCredentials(username, password);
        }

        private string GetPathToCredentialsFile()
        {
            return IsTestPathProvided() ? TestPathToCredentials : VsProfileCredentials();
        }


        private static string VsProfileCredentials()
        {
            var profilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            return Path.Combine(profilePath, CredentialsFileName);
        }

        private bool IsTestPathProvided()
        {
            return !string.IsNullOrEmpty(TestPathToCredentials);
        }

        private void PurgeAnyCache()
        {
            var path = GetPathToCredentialsFile();

            if (File.Exists(path))
                File.Delete(path);
        }
    }
}