using System;
using MyShop.Core.Domain;
using Newtonsoft.Json;

namespace MyShop.Services.Products.Commands
{
    public class DeleteProduct : ICommand, IIdentifiable
    {
        public Guid Id { get; }


        [JsonConstructor]
        public DeleteProduct(Guid id)
        {
            Id = id;
        }
    }
}