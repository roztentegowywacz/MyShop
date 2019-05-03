namespace MyShop.Core.Domain.Orders
{
    public enum OrderStatus : byte
    {
        Created = 0,
        Approved = 1,
        Completed = 2,
        Revoked = 3,
        Canceled = 4
    }
}