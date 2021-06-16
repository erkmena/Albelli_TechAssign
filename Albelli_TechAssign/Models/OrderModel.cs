namespace Albelli_TechAssign.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public decimal ReqBinWidth { get; set; }
        public string CustomerNameSurname { get; set; }
        public CardModel Card { get; set; }
    }
}
