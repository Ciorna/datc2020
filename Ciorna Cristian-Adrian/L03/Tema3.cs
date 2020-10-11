using System;
using System.Collections.Generic;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;

namespace L03
{
    class Tema3
    {
        private static DriveService _service;
         private static string _token;
        private static IEnumerable<string> scopes;

        static void Main(string[] args)
        {
            init();
        }
        static void init()
        {
            string[] scop=new string[]
            {
                DriveService.Scope.Drive,
                 DriveService.Scope.DriveFile
            };
            var clientID="267468784456-3phkbtgdi9v8qss2d4ac4mp1oir91uj3.apps.googleusercontent.com";
            var clientSecret="boz_IkkbFKamM-3cxXY_mAlm";

             var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                 new ClientSecrets
                 {
                     ClientId=clientID,
                     ClientSecret=clientSecret
                 },
                 scopes,
                 Environment.UserName,
                 CancellationToken.None,

                 null
             ).Result;
            _service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
            
            });
            _token=credential.Token.AccessToken;
            Console.Write("Token:" + credential.Token.AccessToken);
                 
        }

    }
}
