using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using Newtonsoft.Json;

namespace DIGESETT.Models
{


    public class ReviewsInfo
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("ticketId")]
        public int TicketId { get; set; }

        [JsonProperty("agentId")]
        public int AgentId { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        //public int userId { get; set; }
        //public int rating { get; set; }
        //public int ticketId { get; set; }
        //public int agentId { get; set; }
        //public string comment { get; set; }


    }
}
