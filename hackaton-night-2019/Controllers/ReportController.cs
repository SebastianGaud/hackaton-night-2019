using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hackaton_night_2019.Config;
using hackaton_night_2019.Models;
using hackaton_night_2019.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hackaton_night_2019.Controllers
{
    [EnableCors]
    public class ReportController : Controller
    {
        private readonly Dictionary<string, IEnumerable<string>> _intentRegistry;
        private readonly IDatabaseContext _context;
        public ReportController(IDatabaseContext context)
        {
            _context = context;
            var r = new Dictionary<string, IEnumerable<string>>
            {
                {
                    "Connessione lenta", new List<string>
                    {
                        "ConnessioneLenta ", " ConnessioneLenta - Risolto", " ApriTicket - Connessione ",
                        " RifiutoAperturaTicketConnessione", " ConfermaAperturaTicketConnessione"
                    }
                },
                {
                    "Connessione non funziona", new List<string>
                    {
                        "AntennaCollegata", "AntennaCollegata - Risolto", "RiavvioElettrico ",
                        " RiavvioElettrico - Risolto", " Guida ", " ApriTicket ",
                        " ConfermaAperturaTicketConnessioneNonFunzionante",
                        " RifiutoAperturaTicketConnessioneNonFunzionante", " Guida - Risolto"
                    }
                },
                {
                    "Telefono non funziona", new List<string>
                    {
                        "TelefonoNonChiama ", " TelefonoNonChiama - Risolto", " Composizione ", " TelefonoDiverso ",
                        " TelefonoDiverso - ApriTicket ", " ConfermaAperturaTicketTelefonoNonChiama",
                        " RifiutoAperturaTicketTelefonoNonChiama", " TelefonoDiverso - Risolto",
                        " Composizione - Risolto"
                    }
                },
                {
                    "Telefono riceve ma non ne effettua", new List<string>
                    {
                        "TelefonoNonChiamaENonRiceve ", " TelefonoNonChiamaENonRiceveRisolto",
                        " TelefonoNonChiamaENonRiceveRiavvia ", " TelefonoNonChiamaENonRiceveRiavviaRisolto",
                        " TelefonoNonChiamaENonRiceveRiavviaTicket ",
                        " ConfermaAperturaTicketTelefonoNonChiamaENonRiceve",
                        " RifiutoAperturaTicketTelefonoNonChiamaENonRiceve"
                    }
                },
                {
                    "Telefono effettua ma non riceve", new List<string>
                    {
                        "TelefonoNonRiceve ", " TelefonoDifferente ", " TelefonoDifferente - Risolto",
                        " TelefonoDifferente - ApriTicket ", " RifiutoAperturaTicketTelefonoNonRiceve",
                        " ConfermaAperturaTicketTelefonoNonRiceve", " TelefonoNonRiceve - Risolto"
                    }
                },
                {
                    "Telefono collegato ma non funziona", new List<string>
                    {
                        "TelefonoNonFunziona ", " TelefonoNonFunziona - Risolto", " TelefonoRiavvio ",
                        "CambioTelefono ", " CambioTelefono - ApriTicket ",
                        "Conferma AperturaTicketTelefonoNonFunziona", " RifiutoAperturaTicketTelefonoNonFunziona",
                        "CambioTelefono - Risolto", " TelefonoRiavvio - Risolto"
                    }
                },
                {
                    "Disconnessione", new List<string>
                    {
                        "Disconnessione", "Disconnessione - Guida", "Disconnessione - Guida - Risolto",
                        "RifiutoAperturaTicketDisconnessione", " ConfermaAperturaTicketDisconnesione",
                        "Disconnessione - Guida - no", "Disconnessione - Risolto"
                    }
                }
            };

            _intentRegistry = new Dictionary<string, IEnumerable<string>>();
            foreach (var k in r.Keys)
            {
                _intentRegistry[k] = r[k].Select(x => x.TrimEnd().TrimStart());
            }
        }

        public string GetValue(string intentName)
        {
            foreach (var k in _intentRegistry.Keys)
            {
                if (_intentRegistry[k].Contains(intentName))
                {
                    return k;
                }
            }

            return "Generico";
        }


        public IActionResult GetIntentAnalisys()
        {
            var data = _context.Get<MessageDescriptor>(Consts.MessageDescriptorTable).FindAll();

            var count = data.Where(x => !string.IsNullOrWhiteSpace(x.IntentName)).GroupBy(x => x.IntentName).ToDictionary(k => k.Key, v => v.Count());

            return Ok(new
            {
                data = data.Where(x => !string.IsNullOrWhiteSpace(x.IntentName)).Select(descriptor => new
                {
                    desc = GetValue(descriptor.IntentName),
                    c = count[descriptor.IntentName]
                }).GroupBy(m => m.desc).ToDictionary(k => k.Key, v => v.Sum(c => c.c)).Select(x => new
                {
                    desc = x.Key,
                    c = x.Value
                })
            });
        }
    }
}
