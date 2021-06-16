using Albelli_TechAssign.Models;
using Albelli_TechAssign.Repository.Interfaces;
using System;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly string _cardInsertSQL = "INSERT INTO Card DEFAULT Values; SELECT SCOPE_IDENTITY()";
        private readonly string _cardItemInsertSQL = "INSERT INTO CARDITEM(CardId, Quantity, ProductTypeId) VALUES(@CardId, @Quantity, @ProductTypeId)";
        private readonly string _cardItemSelectSQL = "Select * FROM CARDITEM C INNER JOIN ProductType P ON C.ProductTypeId = P.ProductTypeId Where CARDID = @CardId";
        private IConfiguration _configuration;

        public CardRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateCardAsync(CardModel card)
        {
            using (var connection = new SqlConnection(new SqlConfiguration(_configuration).GetConnectionString()))
            {
                var cardId = connection.QueryFirstOrDefault<int>(_cardInsertSQL);
                card.CardId = cardId;
                if (cardId != 0)
                {
                    foreach (var item in card.CardItem)
                    {
                       await connection.QueryAsync(_cardItemInsertSQL, new { CardId = cardId, Quantity = item.Quantity, ProductTypeId = item.ProductType.ProductTypeId });
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<CardModel> GetCardAsync(int cardId)
        {
            using (var connection = new SqlConnection(new SqlConfiguration(_configuration).GetConnectionString()))
            {
                var cardItems = await connection.QueryAsync<CardItemModel,ProductTypeModel, CardItemModel>(_cardItemSelectSQL,(c,p)=>
                {
                    CardItemModel cardItem = new CardItemModel();
                    cardItem = c;
                    cardItem.ProductType = p;
                    return cardItem;
                },new { CardId = cardId }, splitOn : "CardItemId, ProductTypeId");

                if (cardItems.Count() > 0)
                {
                    CardModel card = new CardModel() { CardId = cardId };
                    card.CardItem = cardItems;
                    return card;
                }
                else
                {
                    throw new Exception("Card Can Not Be Find");
                }
            }
        }
    }
}
