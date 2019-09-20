using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Api.Gax.Grpc;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Dialogflow.V2;
using Grpc.Auth;

namespace hackaton_night_2019.Controllers
{
    public class DialogController : Controller
    {
        public string GetResponseFromDialogFlow(string question,string context)
        {
            var query = new QueryInput
            {
                Text = new TextInput
                {
                    Text = question,
                    LanguageCode = "it-IT",
                }
            };

            var sessionId = Guid.NewGuid().ToString();
            var agent = "HackatonNight";
            var creds = GoogleCredential.FromAccessToken("27a5dfa1bde94f2eb109717143748e78");
            var channel = new Grpc.Core.Channel(SessionsClient.DefaultEndpoint.Host,
                creds.ToChannelCredentials());

            var client = SessionsClient.Create(channel);

           

            //var dialogFlow = client.DetectIntent(new SessionName(agent, sessionId),query);

            var detectIntentRequest = new DetectIntentRequest
            {
                SessionAsSessionName = new SessionName(agent, sessionId),
                QueryInput = query,
                QueryParams = new QueryParameters
                {
                    Contexts =
                    {
                        new Context
                        {
                            Name = context
                        }
                    },

                }

            };


            var dialogFlow = client.DetectIntent(detectIntentRequest);

            return null; //dialogFlow.QueryResult.OutputContexts.;
        }
    }
}