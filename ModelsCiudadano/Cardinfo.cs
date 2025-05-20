

namespace DIGESETT.Models
{
    public class Cardinfo
    {
       //  public int cardId{ get; set; }
        public int userId { get; set; }
        public string? cardNumber { get; set; }
        public string? expirationDate { get; set; }
        public string? cvv { get; set; }
    }
}
