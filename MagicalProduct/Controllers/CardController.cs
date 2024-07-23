using MagicalProduct.API.Payload.Request.Card;
using MagicalProduct.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MagicalProduct.API.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardController : BaseController<CardController>
    {
        private readonly ICardService _cardService;
        public CardController(ILogger<CardController> logger, ICardService paymentService) : base(logger)
        {
            _cardService = paymentService;
        }

        [HttpGet]
        public IActionResult GetCards(string? name, string? cardNum)
        {
            var response = _cardService.Get(name, cardNum);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentMethodById(int id)
        {
            var response = _cardService.GetById(id);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentMethod(CardReq req)
        {
            var response = await _cardService.Create(req);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePaymentMethod(int id, CardReq req)
        {
            var response = await _cardService.Update(id, req);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            var response = await _cardService.Delete(id);
            return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
        }
    }

}