using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAiSDK;
using ApiAiSDK.Model;
using hackaton_night_2019.Config;
using hackaton_night_2019.Models;
using hackaton_night_2019.Services;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace hackaton_night_2019.Controllers
{
    [EnableCors]
    public class DialogController : Controller
    {
        private readonly IDatabaseContext _dbContext;

        public DialogController(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

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

            if (responseText.Contains("0") && responseText.Contains("#"))
            {
                var link = responseText.Substring(responseText.IndexOf("#", StringComparison.Ordinal) + 1);
                responseText = responseText.Remove(responseText.IndexOf("#", StringComparison.Ordinal));
                responseText = responseText.Replace("0", link);
            }

            var messageDescriptor = new MessageDescriptor()
            {
                OpenTicket = response.Result.Metadata.IntentName.Contains("ConfermaAperturaTicket"),
                Question = question,
                Response = responseText,
                SessionId = response.SessionId,
                TimeStamp = DateTime.Now
            };

            //_dbContext.Set(messageDescriptor, Consts.MessageDescriptorTable);


            return Ok(new
            {
                data = responseText,
                context = response.Result.Contexts.FirstOrDefault()?.Name
            });
        }
    }
}