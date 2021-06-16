using Albelli_TechAssign.Models;
using System;
using System.Collections.Generic;

namespace Albelli_TechAssing_UnitTest
{
    public static class MockModels
    {
        public static CardModel MockCardModel(int quantity, int quantity2)
        {
            List<CardItemModel> cardItems = new List<CardItemModel>();
            CardItemModel cardItem = new CardItemModel()
            {
                ProductType = MockProductTypeModel(1),
                Quantity = quantity
            };
            CardItemModel cardItem2 = new CardItemModel()
            {
                ProductType = MockProductTypeModel(4),
                Quantity = quantity2
            };
            cardItems.Add(cardItem);
            cardItems.Add(cardItem2);
            CardModel cardModel = new CardModel()
            {
                CardId = 1,
                CardItem = cardItems
            };
            return cardModel;
        }
        public static ProductTypeModel MockProductTypeModel(int maxWidthSize)
        {
            ProductTypeModel productType = new ProductTypeModel()
            {
                MaxStockSize = maxWidthSize,
                Width = 5,
                ProductTypeName = "TestProduct" + maxWidthSize,
                ProductTypeId = 1
            };
            return productType;
        }
        public static OrderModel MockOrderModel()
        {
            OrderModel order = new OrderModel()
            {
                Card = MockCardModel(1,1),
                CustomerNameSurname = "Test Customer",
                ReqBinWidth = Convert.ToDecimal(20.2),
                OrderId = 1
            };
            return order;
        }
    }
}
