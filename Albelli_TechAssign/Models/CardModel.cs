using System.Collections.Generic;

namespace Albelli_TechAssign.Models
{
    public class CardModel
    {
        public int CardId { get; set; }
        public IEnumerable<CardItemModel> CardItem { get; set; }
    }
}
