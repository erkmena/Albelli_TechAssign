using Albelli_TechAssign.Business.Interfaces;
using Albelli_TechAssign.Models;
using Albelli_TechAssign.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Business
{
    public class CardBusiness : ICardBusiness
    {
        private readonly IProductTypeBusiness _productTypeBusiness;
        private readonly ICardRepository _cardRepository;

        public CardBusiness(IProductTypeBusiness productTypeBusiness, ICardRepository cardRepository)
        {
            _productTypeBusiness = productTypeBusiness;
            _cardRepository = cardRepository;
        }
        public async Task<CardModel> GetCardAsync(int cardId)
        {
            CardModel returnCard = await _cardRepository.GetCardAsync(cardId);
            return returnCard;
        }

        public async Task<bool> CreateCardAsync(CardModel card)
        {
            if (IsValidCard(card))
            {
                return await _cardRepository.CreateCardAsync(card);
            }
            return false;
        }

        public decimal CalculateMinBinWidth(CardModel card)
        {
            decimal minWidth= 0;
            foreach (var item in card.CardItem)
            {
                decimal itemSize = Math.Ceiling((decimal)item.Quantity / (decimal)item.ProductType.MaxStockSize);
                minWidth += itemSize * item.ProductType.Width;
            }
            return minWidth;
        }

        public bool IsValidCard(CardModel card)
        {
            bool isValid = true;
            foreach (var item in card.CardItem)
            {
                isValid &= item.Quantity != 0 && item.ProductType.ProductTypeId != 0;
            }
            return isValid;
        }
    }
}