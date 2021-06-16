using Albelli_TechAssign.Business.Interfaces;
using Albelli_TechAssign.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly ICardBusiness _cardBusiness;

        public CardController(ILogger<CardController> logger, ICardBusiness cardBusiness)
        {
            _logger = logger;
            _cardBusiness = cardBusiness;
        }
        [HttpGet]
        public async Task<CardModel> GetCardAsync(int cardId)
        {
            return await _cardBusiness.GetCardAsync(cardId);
        }
        [HttpPost]
        public async Task<bool> CreateCardAsync(CardModel card)
        {
            return await _cardBusiness.CreateCardAsync(card);
        }
    }
}
