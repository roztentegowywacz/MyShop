using System;
using MyShop.Core.Domain;
using Newtonsoft.Json;

namespace MyShop.Services.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : ICommand, IIdentifiable
    {
        public Guid Id { get; }


        [JsonConstructor]
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}