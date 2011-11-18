using System;
using System.Net;
using EchelonTouchInc.Gister.FluentHttp;
using FluentHttp;

namespace EchelonTouchInc.Gister.Api
{
    public class GistApi
    {
        public void Create(string fileName, string content)
        {

            var gistAsJson = new GistJson().CreateFrom(fileName, content);

            var response = new FluentHttpRequest()
                .BaseUrl("https://api.github.com")
                .AuthenticateUsing(new HttpBasicAuthenticator("get", "real"))
                .ResourcePath("/gists")
                .Method("POST")
                .Headers(h => h.Add("User-Agent", "Gister"))
                .Headers(h => h.Add("Content-Type", "application/json"))
                .Body(x => x.Append(gistAsJson))
                .Execute();

            if (response.Response.HttpWebResponse.StatusCode != HttpStatusCode.Created)
                throw new ApplicationException("");
        }
    }
}
