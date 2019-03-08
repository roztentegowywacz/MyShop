using System.Threading.Tasks;

namespace MyShop.Infrastructure.Mongo
{
    public interface IMongoDbInitializer
    {
        Task InitializeAsync();
    }
}