using Albelli_TechAssign.Models;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Business.Interfaces
{
    public interface ICardBusiness
    {
        public Task<CardModel> GetCardAsync(int CardId);
        public Task<bool> CreateCardAsync(CardModel Card);
        public decimal CalculateMinBinWidth(CardModel card);
        public bool IsValidCard(CardModel card);
    }
}
