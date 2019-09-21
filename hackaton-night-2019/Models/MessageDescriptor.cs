using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackaton_night_2019.Models
{
    public class MessageDescriptor
    {
        public DateTime TimeStamp { get; set; }
        public string SessionId { get; set; }
        public string Question { get; set; }
        public string Response { get; set; }
        public bool? OpenTicket { get; set; }
        public int ConversationId { get; set; }
    }
}
