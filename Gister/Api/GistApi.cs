using System;
using System.Net;
using System.Security;
using EchelonTouchInc.Gister.FluentHttp;
using FluentHttp;

namespace EchelonTouchInc.Gister.Api
{
    public class GistApi
    {
        public void Create(string fileName, string content)
        {
            var realContent = content.Replace("\n", "\\n").Replace("\t", "\\t").Replace("\r", "\\r");

            var doc = @"{
  ""public"": true,
  ""files"": {
    """ + fileName + @""": {
      ""content"": """ + realContent + @"""
    }
  }
}";

            var response = new FluentHttpRequest()
                .BaseUrl("https://api.github.com")
                .AuthenticateUsing(new HttpBasicAuthenticator("get", "real"))
                .ResourcePath("/gists")
                .Method("POST")
                .Headers(h => h.Add("User-Agent", "Gister"))
                .Headers(h => h.Add("Content-Type", "application/json"))
                .Body(x => x.Append(doc))
                .Execute();

            if(response.Response.HttpWebResponse.StatusCode != HttpStatusCode.Created)
                throw new ApplicationException("");
        }
    }
}
