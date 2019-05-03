using System;
using Newtonsoft.Json;

namespace MyShop.Services.Orders.Commands
{
    public class ApproveOrder : ICommand
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ApproveOrder(Guid id)
        {
            Id = id;
        }
    }
}