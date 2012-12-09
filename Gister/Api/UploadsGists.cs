﻿using System;
using EchelonTouchInc.Gister.Api.Credentials;

namespace EchelonTouchInc.Gister.Api
{
    public class UploadsGists 
    {

        private GitHubCredentials gitHubCredentials = GitHubCredentials.Anonymous;

        public UploadsGists()
        {
            GitHubSender = new NoWhereGitHubSender();
            Uploaded = url => { };
            CredentialsAreBad = () => { };
        }

        public void Upload(string fileName, string content,string description,bool isPublic)
        {
            string gistUrl;

            try
            {
                gistUrl = GitHubSender.SendGist(fileName, content, description,isPublic, gitHubCredentials);
            }
            catch (GitHubUnauthorizedException)
            {
                CredentialsAreBad();
                return;
            }

            Uploaded(gistUrl);
        }

        public void UseCredentials(GitHubCredentials credentials)
        {
            gitHubCredentials = credentials;
        }

        public GitHubSender GitHubSender { get; set; }

        public Action<string> Uploaded { get; set; }

        public Action CredentialsAreBad { get; set; }
    }

}
