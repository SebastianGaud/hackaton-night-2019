using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAiSDK;
using ApiAiSDK.Model;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace hackaton_night_2019.Controllers
{
    [EnableCors]
    public class DialogController : Controller
    {
        [HttpGet]
        public IActionResult GetResponseFromDialogFlow(string question,string context)
        {
            var config = new AIConfiguration("27a5dfa1bde94f2eb109717143748e78", SupportedLanguage.Italian);

            var apiAi = new ApiAiSDK.ApiAi(config);

            var contexts = new List<AIContext> {new AIContext {Name = context,Lifespan = 1}};

            var requestExtras= new RequestExtras
            {
                Contexts = contexts
            };

            var response = apiAi.TextRequest(question, requestExtras);

            var responseText = response.Result.Fulfillment.Speech;

            if (responseText.Contains("questo link"))
            {
                var link = responseText.Substring(responseText.IndexOf("(", StringComparison.Ordinal)+1,
                    responseText.Length - responseText.IndexOf("(", StringComparison.Ordinal)-2);
                responseText = responseText.Remove(responseText.IndexOf("(", StringComparison.Ordinal));
                var htmlLink = "<a href=" + link + "></a>";

                return Ok(new
                {
                    data = responseText + htmlLink,
                    context = response.Result.Contexts.FirstOrDefault()?.Name,
                    link= htmlLink
                });
            }

            return Ok(new
            {
                data = response.Result.Fulfillment.Speech,
                context = response.Result.Contexts.FirstOrDefault()?.Name
            });
        }
    }
}