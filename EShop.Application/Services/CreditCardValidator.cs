﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EShop.Application.Models;
using EShop.Application.Services;

namespace EShop.Application.Services;

public class CreditCardValidator : ICreditCardService
{
    public ValidationResult Validate(string cardNumber)
    {

        if (string.IsNullOrWhiteSpace(cardNumber))
            throw new ArgumentException("Card number cannot be empty");

        string digits = new string(cardNumber.Where(char.IsDigit).ToArray());


        if (digits.Length < 13)
            throw new ArgumentException("The card number is too short");


        if (digits.Length > 19)
            throw new ArgumentException("The card numbe is too long");


        if (!ValidateCard(digits))
            throw new ArgumentException($"The {digits} number is not valid according to luhn's algorithm");

        var type = GetCardType(digits);
        return new ValidationResult(true, type);
        

        
        
    }

    public bool ValidateCard(string cardNumber)
    {
        cardNumber = cardNumber.Replace(" ", "");
        if (!cardNumber.All(char.IsDigit))
            return false;

        int sum = 0;
        bool alternate = false;

        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int digit = cardNumber[i] - '0';

            if (alternate)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            alternate = !alternate;
        }

        return (sum % 10 == 0);
    }


    public string GetCardType(string cardNumber)
    {
        cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

        if (Regex.IsMatch(cardNumber, @"^4(\d{12}|\d{15}|\d{18})$"))
            return "Visa";
        if (Regex.IsMatch(cardNumber, @"^(5[1-5]\d{14}|2(2[2-9][1-9]|2[3-9]\d{2}|[3-6]\d{3}|7([01]\d{2}|20\d))\d{10})$"))
            return "MasterCard";
        if (Regex.IsMatch(cardNumber, @"^3[47]\d{13}$"))
            return "American Express";
        if (Regex.IsMatch(cardNumber, @"^(6011\d{12}|65\d{14}|64[4-9]\d{13}|622(1[2-9][6-9]|[2-8]\d{2}|9([01]\d|2[0-5]))\d{10})$"))
            return "Discover";
        if (Regex.IsMatch(cardNumber, @"^(352[89]|35[3-8]\d)\d{12}$"))
            return "JCB";
        if (Regex.IsMatch(cardNumber, @"^3(0[0-5]|[68]\d)\d{11}$"))
            return "Diners Club";
        if (Regex.IsMatch(cardNumber, @"^(50|5[6-9]|6\d)\d{10,17}$"))
            return "Maestro";

        return "Unknown";

    }



}
