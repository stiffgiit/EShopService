using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.Models;

namespace EShop.Application.Services
{
    public interface ICreditCardService
    {
        ValidationResult Validate(string cardNumber);


    }
}
