using System;
using System.Collections.Generic;
using System.Linq;
using FluentHttp;

namespace EchelonTouchInc.Gister.Api.Credentials
{
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
}