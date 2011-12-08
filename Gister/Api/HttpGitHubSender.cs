using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using EchelonTouchInc.Gister.FluentHttp;
using FluentHttp;
using Newtonsoft.Json.Linq;

namespace EchelonTouchInc.Gister.Api
{
    public class HttpGitHubSender : GitHubSender
    {

        public string SendGist(string fileName, string content, GitHubCredentials credentials)
        {
            var gistAsJson = new CreatesGistMessages().CreateMessage(fileName, content);

            var request = new FluentHttpRequest()
                .BaseUrl("https://api.github.com")
                .ResourcePath("/gists")
                .Method("POST")
                .Headers(h => h.Add("User-Agent", "Gister"))
                .Headers(h => h.Add("Content-Type", "application/json"))
                .Body(x => x.Append(gistAsJson))
                .OnResponseHeadersReceived((o, e) => e.SaveResponseIn(new MemoryStream()));

            AppliesGitHubCredentialsToFluentHttpRequest.ApplyCredentials(credentials, request);

            var response = request
                .Execute();

            if (response.Response.HttpWebResponse.StatusCode != HttpStatusCode.Created)
                throw new GitHubUnauthorizedException(response.Response.HttpWebResponse.StatusDescription);

            return PeelOutGistHtmlUrl(response);
        }


        private static string PeelOutGistHtmlUrl(FluentHttpAsyncResult response)
        {
            response.Response.SaveStream.Seek(0, SeekOrigin.Begin);
            var gistJson = FluentHttpRequest.ToString(response.Response.SaveStream);

            dynamic gist = JObject.Parse(gistJson);

            return (string)gist.html_url;
        }
    }

    public class AppliesGitHubCredentialsToFluentHttpRequest
    {
        public static void ApplyCredentials(GitHubCredentials credentials, FluentHttpRequest request)
        {
            var map = new Dictionary<Type, CredentialVisitor>()
                          {
                              {typeof (AnonymousGitHubCredentials), new AnonymousGitHubCredentialsForFluentHttp()},
                              {typeof (GitHubUserCredentials), new GitHubUserCredentialsForFluentHttp()}
                          };

            var credentialApplier = (from item in map
                                     where item.Key == credentials.GetType()
                                     select item.Value).First();

            credentialApplier.Apply(request, credentials);
        }
    }

    public class GitHubUserCredentialsForFluentHttp : CredentialVisitor
    {
        public void Apply(FluentHttpRequest request, GitHubCredentials credentials)
        {
            var userCreds = (GitHubUserCredentials)credentials;

            request.AuthenticateUsing(new HttpBasicAuthenticator(userCreds.Username, userCreds.Password));
        }
    }

    public interface CredentialVisitor
    {
        void Apply(FluentHttpRequest request, GitHubCredentials credentials);
    }

    public class AnonymousGitHubCredentialsForFluentHttp : CredentialVisitor
    {
        public void Apply(FluentHttpRequest request, GitHubCredentials credentials)
        {
        }
    }

    public class GitHubUnauthorizedException : Exception
    {
        public GitHubUnauthorizedException(string statusDescription)
            : base(statusDescription)
        {
        }
    }
}