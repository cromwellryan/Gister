using System;
using System.IO;
using EchelonTouchInc.Gister.Api;

namespace EchelonTouchInc.Gister
{
    public class CachesGitHubCredentials : RetrievesCredentials
    {
        public string TestPathToCredentials { get; set; }

        private GitHubCredentials GetCredentials()
        {
            var pathToCredentialsFile = GetPathToCredentialsFile();
            var lines = File.ReadAllLines(pathToCredentialsFile);

            return DecodeGitHubCredentialsFromFile(lines);
        }

        private string GetPathToCredentialsFile()
        {
            return IsTestPathProvided() ?  TestPathToCredentials : VsProfileCredentials();
        }

        private static string VsProfileCredentials()
        {
            var profilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            return Path.Combine(profilePath, "github.creds");
        }

        private bool IsTestPathProvided()
        {
            return !string.IsNullOrEmpty(TestPathToCredentials);
        }

        private GitHubCredentials DecodeGitHubCredentialsFromFile(string[] lines)
        {
            return new GitHubCredentials(lines[0], lines[1]);
        }

        public bool IsAvailable()
        {
            return File.Exists(VsProfileCredentials());
        }

        public GitHubCredentials Retrieve()
        {
            return GetCredentials();
        }

        public void AssureNotCached()
        {
            PurgeAnyCache();
        }

        private static void PurgeAnyCache()
        {
            var path = VsProfileCredentials();

            if (File.Exists(path))
                File.Delete(path);
        }

        public void Cache(GitHubCredentials credentials)
        {
            var path = VsProfileCredentials();

            PurgeAnyCache();

            File.WriteAllLines(path, new[] {credentials.Username, credentials.Password});
        }
    }
}