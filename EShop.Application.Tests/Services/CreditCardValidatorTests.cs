using EShop.Application.Services;
using EShop.Domain.Exceptions;
using Xunit;

namespace EShop.Application.Tests.Services
{
    public class CreditCardValidatorTests
    {
        [Theory]
        [InlineData("4111 1111 1111 1111", true, "Visa")]
        [InlineData("5500-0000-0000-0004", true, "MasterCard")]
        [InlineData("3782 822463 10005", true, "American Express")]
        public void Validate_ValidCard_ReturnsExpectedResult(string input, bool expectedValid, string expectedType)
        {
            var validator = new CreditCardValidator();
            var result = validator.Validate(input);

            Assert.Equal(expectedValid, result.IsValid);
            Assert.Equal(expectedType, result.CardType);
        }

        [Theory]
        [InlineData("1234 5678 9012 3456")]
        [InlineData("")]
        public void Validate_InvalidCard_ThrowsCardNumberInvalidException(string input)
        {
            var validator = new CreditCardValidator();

            // Sprawdzamy, czy metoda rzuca odpowiedni wyjątek
            var exception = Assert.Throws<CardNumberInvalidException>(() => validator.Validate(input));
            Assert.Equal("The card number is invalid according to Luhn's algorithm.", exception.Message);
        }

        [Theory]
        [InlineData("1234")]
        public void Validate_TooShortCard_ThrowsCardNumberTooShortException(string input)
        {
            var validator = new CreditCardValidator();

            // Sprawdzamy, czy metoda rzuca odpowiedni wyjątek
            var exception = Assert.Throws<CardNumberTooShortException>(() => validator.Validate(input));
            Assert.Equal("The card number is too short.", exception.Message);
        }

        [Theory]
        [InlineData("4111 1111 1111 1111 1111")]
        public void Validate_TooLongCard_ThrowsCardNumberTooLongException(string input)
        {
            var validator = new CreditCardValidator();

            // Sprawdzamy, czy metoda rzuca odpowiedni wyjątek
            var exception = Assert.Throws<CardNumberTooLongException>(() => validator.Validate(input));
            Assert.Equal("The card number is too long.", exception.Message);
        }
    }
}
