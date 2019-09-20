using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAiSDK;
using ApiAiSDK.Model;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Mvc;


namespace hackaton_night_2019.Controllers
{
    public class DialogController : Controller
    {
        [HttpGet]
        public IActionResult GetResponseFromDialogFlow(string question,string context)
        {
            //var test= ApiAi.Models.
            //var query = new QueryInput
            //{
            //    Text = new TextInput
            //    {
            //        Text = question,
            //        LanguageCode = "it-IT",
            //    }
            //};

            //var sessionId = Guid.NewGuid().ToString();
            //var agent = "HackatonNight";
            //var creds = GoogleCredential.FromJson("{\n  \"type\": \"service_account\",\n  \"project_id\": \"hackathonnight-asnias\",\n  \"private_key_id\": \"55ba67c6a9735c7b2bde6ff42d6b98fd3d00ce04\",\n  \"private_key\": \"-----BEGIN PRIVATE KEY-----\\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDGvmzPZDkROl0t\\nGlkp1bIEDQUkCI9VJqCvQyWxwIj1KhCV/CuhGB76sqF/ACPydRgM8vKYU9UhpVQh\\nJmoHxLBFDgo9lYnoaymyl66RcDKzh3XO6s5vMy6jhT0ZA+qjbVJrAcTKdhc0paDD\\nlDKF0ek4IDi2FdP3ycUTy0tCMQ1pVBwQ5Y1+IoEnbXWDKbD96ajrE7E8p0b511VB\\nSmbsH9C4IHCX08ISsjXH97QaR8K00iR1XADNRVW2ufR30Gcaw6YYIYClH5/TxIJG\\n2RCosDZawwM3ZzIgvooEMkMDZvuDNT/jh6rsJhNo+DZxvRJxc6OalpMxHduNQBut\\nzmYKKDGhAgMBAAECggEAGDGSOWTv9YLvWNA+TnNvKkMTfyFzoWAEa5F3Az527bFj\\nN37tKL0w+D/KnRU9SwSqGtlwYs7BwUjJBwgQHEX7V0ywPnL3yM1S1CTW4WmRIn24\\n/6lh2/OVF97eYy8w3xgt0kzy0dqyVHxdJ7HgvJ2jEGwLu07I8P8k+JXk4XfrNAkM\\nLwaQtp6i4y6CjgNVgE6K49p8Sfj06VXeIBRNGKNb6SUKnadRoVD7f88vactJur6H\\nLkGCeZ1a8XuK1lkF3CFlN8vFQFQsKnfWiqoDSwfs7bOpsjy4a2GlZIWWcfXZ2nN+\\nblLmtgMOoNujDxYBDGJEgM9cN1FIbGOdYU1djiDuSQKBgQDseXYFvieC9c/3mEol\\nR9iaAFZ51bKfkYv10MIHIBDGsAwOSkjojF8ZNKg3S30bI8zQQwshDhfh4dinscrQ\\nirUzfV/CukmYSrGocPiSMezDlLWACqDvUzbXnURoz8wSIKXrvTPz+TsS5cvvu/To\\nP3wLtV3ICXrixq8FQJPqzgB4uwKBgQDXJ2x1SxJeduGHA4VD2FXhmHwjCySxmiJy\\nDW43dtFZFLEcVAa7ufeR3Vkjki+/cGkwnhw50IK5oPIal05LHkug90R/+9x+0ag4\\nrBSK4XMqM7Yfk4H0ws98S8BBikxNK3QGgIQy6JfPCPjwDHNBSUkBPRV2rUSkAlBX\\nR5jbhBTXUwKBgQCgCmwH4vPWvAot5ZF4vbEoOMZN2oTzXYu9p7VJITh5X7gpC7lg\\nOXA7HNoj4iEfdxEsedNUYbdkFneMttUuYlHUMSvYHD+mpBasixiPyN4WV9SmfYsB\\nre3V6wfbb5cLRbFFZF1+5tRGK+Pjse9EJ4MKYYrA0TmrtQ/KnDP/m7/R9QKBgGnO\\nTGr+KSFlaxE/bPjpWYqgt2NwZnIkr43RBwOlndwl7ddVhx4onRQRe3WfvXVyrXgo\\nOQ9BeBgbixQClpEga+bT0s7xqASBzC1aipultUHqvkSXANDCQNEKW5ifj1hf5yYP\\na3OxqH47FljOdpuDk7BVPsnm+ZuCO8pAPm6tbKZbAoGBANLVxbuhJojIqBJI6j9+\\nMPnBLQ+8Zz5lQulEJfGupqPzZkMJWXXoGlKd6vLQXZDuBzu3+ltX2+YCj9WDBXhe\\nWboy1LfA97Z0OMf5qmvW4she1zRu89bqpGHUsgfaKYou3A4GBAnqLyCjKSZ56JS3\\nbYwSd4DVvMM81+oDKQPnIEEw\\n-----END PRIVATE KEY-----\\n\",\n  \"client_email\": \"dialogflow-ylfcvg@hackathonnight-asnias.iam.gserviceaccount.com\",\n  \"client_id\": \"114190958223755547222\",\n  \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\",\n  \"token_uri\": \"https://oauth2.googleapis.com/token\",\n  \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\",\n  \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/dialogflow-ylfcvg%40hackathonnight-asnias.iam.gserviceaccount.com\"\n}\n");
            //var channel = new Grpc.Core.Channel(SessionsClient.DefaultEndpoint.Host,
            //    creds.ToChannelCredentials());

            //var client = SessionsClient.Create(channel);



            ////var dialogFlow = client.DetectIntent(new SessionName(agent, sessionId),query);

            //var detectIntentRequest = new DetectIntentRequest
            //{
            //    SessionAsSessionName = new SessionName(agent, sessionId),
            //    QueryInput = query,
            //    QueryParams = new QueryParameters
            //    {
            //        Contexts =
            //        {
            //            new Context
            //            {
            //                Name = context ?? ""
            //            }
            //        },

            //    }

            //};


            //var dialogFlow = client.DetectIntent(detectIntentRequest);



            var config = new AIConfiguration("27a5dfa1bde94f2eb109717143748e78", SupportedLanguage.Italian);

            var apiAi = new ApiAiSDK.ApiAi(config);

            var contexts = new List<AIContext> {new AIContext {Name = context}};

            var requestExtras= new RequestExtras
            {
                Contexts = contexts
            };

            var response = apiAi.TextRequest(question/*, requestExtras*/);

            return Ok(new
            {
                data = response.Result.Fulfillment.Speech   
            }); //dialogFlow.QueryResult.OutputContexts.;
        }
    }
}