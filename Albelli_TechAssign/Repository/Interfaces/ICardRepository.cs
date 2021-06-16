using Albelli_TechAssign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Repository.Interfaces
{
    public interface ICardRepository
    {
        public Task<bool> CreateCardAsync(CardModel card);
        public Task<CardModel> GetCardAsync(int cardId);
    }
}
