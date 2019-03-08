using System;
using System.Threading.Tasks;

namespace MyShop.Infrastructure.Mongo
{
    public class MongoDbInitializer : IMongoDbInitializer
    {
        private bool _initialized;

        public async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }

            RegisterConventions();
            _initialized = true;
             
            await Task.CompletedTask;
        }

        private void RegisterConventions()
        {
            throw new NotImplementedException();
        }
    }
}