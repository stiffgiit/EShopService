namespace EShop.Application.Models
{
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string? CardType { get; }

        public ValidationResult(bool isValid, string? cardType)
        {
            IsValid = isValid;
            CardType = cardType;
        }
    }
}
