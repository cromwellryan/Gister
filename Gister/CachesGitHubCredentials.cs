using System;
using System.IO;
using System.Security;
using EchelonTouchInc.Gister.Api;

namespace EchelonTouchInc.Gister
{
    public class CachesGitHubCredentials : RetrievesCredentials
    {
        private const string CredentialsFileName = ".githubcreds";

        public string TestPathToCredentials { get; set; }

        public bool IsAvailable()
        {
            var path = string.IsNullOrEmpty(TestPathToCredentials)? VsProfileCredentials() : TestPathToCredentials;

            return File.Exists(path);
        }

        public GitHubCredentials Retrieve()
        {
            var path = GetPathToCredentialsFile();

            var lines = File.ReadAllLines(path);

            return DecodeGitHubCredentialsFromFile(lines);
        }

        public void AssureNotCached()
        {
            PurgeAnyCache();
        }

        public void Cache(GitHubCredentials credentials)
        {
            var path = VsProfileCredentials();

            PurgeAnyCache();

            File.WriteAllLines(path, new[] { credentials.Username, credentials.Password });
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

        private static GitHubCredentials DecodeGitHubCredentialsFromFile(string[] lines)
        {
            return new GitHubCredentials(lines[0], lines[1]);
        }

        private static void PurgeAnyCache()
        {
            var path = VsProfileCredentials();

            if (File.Exists(path))
                File.Delete(path);
        }
    }
}