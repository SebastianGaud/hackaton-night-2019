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
        public IActionResult GetResponseFromDialogFlow(string question,string context, string conversationId)
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
                TimeStamp = DateTime.Now,
                ConversationId = conversationId,
                TicketRefused = response.Result.Metadata.IntentName.Contains("RifiutoAperturaTicket"),
                IntentName = response.Result.Metadata.IntentName

            };

            _dbContext.Set(messageDescriptor, Consts.MessageDescriptorTable);


            return Ok(new
            {
                data = responseText,
                context = response.Result.Contexts.FirstOrDefault()?.Name
            });
        }

        public IActionResult GetOpenedChatsAsJson()
        {
            var requests = _dbContext.Get<MessageDescriptor>(Consts.MessageDescriptorTable).FindAll().AsQueryable();

            var openedChats = requests
                .Select(x => x.ConversationId).Distinct().Count();

            return Ok(new { openedChats = openedChats });
        }

        public IActionResult GetSuccessfulInteractionsAsJson()
        {
            var requests = _dbContext.Get<MessageDescriptor>(Consts.MessageDescriptorTable).FindAll().AsQueryable();


            var successfulInteraction = requests.GroupBy(x => x.ConversationId)
                .Select(x => x.All(y => !y.TicketRefused && !y.OpenTicket)).Count();


            return Ok(new { successfulInteraction = successfulInteraction });
        }

        public IActionResult GetSuccessRateAsJson()
        {
            var requests = _dbContext.Get<MessageDescriptor>(Consts.MessageDescriptorTable).FindAll().AsQueryable();

            var openedChats = requests
                .Select(x => x.ConversationId).Distinct().Count();

            var successfulInteraction = requests.GroupBy(x => x.ConversationId)
                .Select(x => x.All(y => !y.TicketRefused && !y.OpenTicket)).Count();

            var successRate = successfulInteraction / openedChats * 100;


            return Ok(new { successRate = successRate });
        }

        public IActionResult GetOpenedTicketsAsJson()
        {
            var requests = _dbContext.Get<MessageDescriptor>(Consts.MessageDescriptorTable).FindAll().AsQueryable();

            var openedTicket = requests.Count(x => x.OpenTicket);

            return Ok(new { openedTicket = openedTicket });
        }
    }
}