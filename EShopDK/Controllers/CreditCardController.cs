using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EShop.Application.Services;
using EShop.Application.Models;
using System.Net;
using EShop.Domain.Exceptions;


namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        protected ICreditCardService _validator;

        public CreditCardController(ICreditCardService validator)
        {
            _validator = validator;
        }

        [HttpPost("validate")]
        public IActionResult Get(string cardNumber)
        {
            try
            {
                var result = _validator.Validate(cardNumber);

                if (!result.IsValid)
                    return BadRequest(new { message = "Invalid card number." });

                if (result.CardType != "Visa" && result.CardType != "MasterCard" && result.CardType != "American Express")
                    return StatusCode(406, new { message = $"Card type '{result.CardType}' is not supported." });

                return Ok(new
                {
                    isValid = result.IsValid,
                    cardType = result.CardType
                });
            }
            catch (CardNumberTooShortException)
            {
                return BadRequest(new { message = "Card number too short." });
            }
            catch (CardNumberTooLongException)
            {
                return StatusCode(414, new { message = "Card number too long." });
            }
            catch (CardNumberInvalidException ex)
            {
                return BadRequest(new { message = ex.Message });
            }



        }
    }
}
