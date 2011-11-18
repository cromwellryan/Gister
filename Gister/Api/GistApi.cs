using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using EchelonTouchInc.Gister.FluentHttp;
using FluentHttp;

namespace EchelonTouchInc.Gister.Api
{
    public class GistApi
    {
        public void Create(string fileName, string content)
        {
            var realContent = CleanContent(content);

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

            if (response.Response.HttpWebResponse.StatusCode != HttpStatusCode.Created)
                throw new ApplicationException("");
        }

        private static string CleanContent(string content)
        {
            var itemsToBeCleaned = new Dictionary<string,string>
                                       {
                                           {"\t", "\\t"},
                                           {"\r", "\\r"},
                                       };
            return itemsToBeCleaned.Aggregate(content, (current, item) => current.Replace(item.Key, item.Value));
        }
    }
}
