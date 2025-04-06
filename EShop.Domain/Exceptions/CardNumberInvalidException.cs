using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Exceptions
{
    public class CardNumberInvalidException : Exception
    {
        public CardNumberInvalidException() : base("The card number is invalid according to luhn's algorithm")
        { 
        
        
        }


    }
}
