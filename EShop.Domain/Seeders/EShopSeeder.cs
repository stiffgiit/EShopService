using EShop.Domain.Models;
using EShop.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Seeders
{
    public class EShopSeeder
    {
        public static void Seed(DataContext context) 
        { 
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "Lenovo ThinkPad X1 Carbon",
                        Price = 10999.00m

                    },

                    new Product
                    {
                        Name = "Dell Precision 5560",
                        Price = 16999.99m
                    },

                    new Product
                    {
                        Name = "Apple MacBook Air ",
                        Price = 6599.00m
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();


            }
            
        }


    }
}
