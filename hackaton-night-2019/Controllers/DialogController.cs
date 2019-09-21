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

            var contexts = new List<AIContext>();

            //if (context.Contains("CreateTicketMail"))
            //{
            //    try
            //    {
            //        var addr = new System.Net.Mail.MailAddress(question);
            //        contexts.Add(new AIContext { Name = "CorrectEmail", Lifespan = 1 });
            //    }
            //    catch (Exception)
            //    {
            //        contexts.Add(new AIContext { Name = "InvalidEmail", Lifespan = 1 });
            //        question = "error";
            //    }
            //}

            contexts.Add(new AIContext { Name = context, Lifespan = 1 });

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

        public IActionResult GetOpenedChatsAsJson(ReportDto dto)
        {
            var requests = _dbContext.Get<MessageDescriptor>(Consts.MessageDescriptorTable).FindAll().AsQueryable();

            dto.StartDate = dto.StartDate ?? DateTime.MinValue;
            dto.EndDate = dto.EndDate ?? DateTime.MaxValue;

            var openedChats = requests.Where(x => x.TimeStamp >= dto.StartDate && x.TimeStamp <= dto.EndDate)
                .GroupBy(x =>  x.TimeStamp.Date).Select(x=> new {day=x.Key.Date,openedChats = x.Select(y=>y.ConversationId).Distinct().Count()});

            return Ok(new { openedChats = openedChats });
        }

        public IActionResult GetSuccessfulInteractionsAsJson(ReportDto dto)
        {
            var requests = _dbContext.Get<MessageDescriptor>(Consts.MessageDescriptorTable).FindAll().AsQueryable();

            dto.StartDate = dto.StartDate ?? DateTime.MinValue;
            dto.EndDate = dto.EndDate ?? DateTime.MaxValue;

            var successfulInteraction = requests.Where(x => x.TimeStamp >= dto.StartDate && x.TimeStamp <= dto.EndDate).GroupBy(x => x.TimeStamp.Date)
                .Where(x => x.All(y => !y.TicketRefused && !y.OpenTicket)).Select(x=> new {day=x.Key.Date, successfulInteractions = x.Select(y => y.ConversationId).Distinct().Count() });


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

        public IActionResult GetOpenedTicketsAsJson(ReportDto dto)
        {
            var requests = _dbContext.Get<MessageDescriptor>(Consts.MessageDescriptorTable).FindAll().AsQueryable();

            dto.StartDate = dto.StartDate ?? DateTime.MinValue;
            dto.EndDate = dto.EndDate ?? DateTime.MaxValue;

            var openedTicket = requests.Where(x => x.TimeStamp >= dto.StartDate && x.TimeStamp <= dto.EndDate).GroupBy(x => x.TimeStamp.Date)
                .Select(x=> new { date=x.Key.Date,openedTicket=x.Count(y=>y.OpenTicket)});

            return Ok(new { openedTicket = openedTicket });
        }
    }
}