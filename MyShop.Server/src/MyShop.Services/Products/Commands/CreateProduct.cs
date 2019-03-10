using System;
using Newtonsoft.Json;

namespace MyShop.Services.Products.Commands
{
    public class CreateProduct : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Vendor { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        [JsonConstructor]
        public CreateProduct(Guid id, string name, 
            string description, string vendor, 
            decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Vendor = vendor;
            Price = price;
            Quantity = quantity;
        }
    }
}