using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net;

using OAuthLib;

namespace OAuthDevSupport
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Consumer c =
                    new Consumer("yourConsumerKey", "yourConsumerSecret");

                RequestToken reqToken =
                    c.ObtainUnauthorizedRequestToken("http://twitter.com/oauth/request_token", "http://twitter.com/");

                Process.Start(
                    Consumer.BuildUserAuthorizationURL(
                        "http://twitter.com/oauth/authorize", 
                        reqToken
                    )
                );

                Console.Out.WriteLine("Input verifier");
                String verifier = Console.In.ReadLine();
                verifier = verifier.TrimEnd('\r', '\n');
                AccessToken accessToken =
                   c.RequestAccessToken(verifier,reqToken, "http://twitter.com/oauth/access_token", "http://twitter.com/");

                HttpWebResponse resp = 
                    c.AccessProtectedResource (
                        accessToken ,
                        "http://twitter.com/statuses/home_timeline.xml",
                        "GET",
                        "http://twitter.com/",
                            new Parameter[]{ 
                                new Parameter("since_id","your since id") 
                            }
                        );

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
