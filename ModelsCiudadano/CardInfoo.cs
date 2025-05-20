using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIGESETT.Models
{
     public  class CardInfoo
    {
        public int cardId { get; set; }
        public int userId { get; set; }
        public string? cardNumber { get; set; }
        public string? expirationDate { get; set; }
        public string? cvv { get; set; }
    }
}
