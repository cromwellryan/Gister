using System;
using System.IO;
using System.Net;
using EchelonTouchInc.Gister.FluentHttp;
using FluentHttp;
using Newtonsoft.Json.Linq;

namespace EchelonTouchInc.Gister.Api
{
    public class HttpGitHubSender : GitHubSender
    {
        #region GitHubSender Members

        public string SendGist(string fileName, string content, GitHubCredentials credentials)
        {
            string gistAsJson = new CreatesGistMessages().CreateMessage(fileName, content);

            FluentHttpAsyncResult response = new FluentHttpRequest()
                .BaseUrl("https://api.github.com")
                .AuthenticateUsing(new HttpBasicAuthenticator(credentials.Username, credentials.Password))
                .ResourcePath("/gists")
                .Method("POST")
                .Headers(h => h.Add("User-Agent", "Gister"))
                .Headers(h => h.Add("Content-Type", "application/json"))
                .Body(x => x.Append(gistAsJson))
                .OnResponseHeadersReceived((o, e) => e.SaveResponseIn(new MemoryStream()))
                .Execute();

            if (response.Response.HttpWebResponse.StatusCode != HttpStatusCode.Created)
                throw new GitHubUnauthorizedException(response.Response.HttpWebResponse.StatusDescription);

            return PeelOutGistHtmlUrl(response);
        }

        #endregion

        private static string PeelOutGistHtmlUrl(FluentHttpAsyncResult response)
        {
            response.Response.SaveStream.Seek(0, SeekOrigin.Begin);
            string gistJson = FluentHttpRequest.ToString(response.Response.SaveStream);

            dynamic gist = JObject.Parse(gistJson);

            return (string) gist.html_url;
        }
    }

    public class GitHubUnauthorizedException : Exception
    {
        public GitHubUnauthorizedException(string statusDescription) : base(statusDescription)
        {
        }
    }
}