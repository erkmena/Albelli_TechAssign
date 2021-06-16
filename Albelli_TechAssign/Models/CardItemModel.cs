namespace Albelli_TechAssign.Models
{
    public class CardItemModel
    {
        public int CardItemId { get; set; }
        public int CardId { get; set; }
        public int Quantity { get; set; }
        public ProductTypeModel ProductType { get; set; }
    }
}