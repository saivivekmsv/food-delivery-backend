using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Restaurant.Models;

namespace Restaurant.Controllers;

public class MongoService
{
    private readonly IMongoCollection<Restaurant.Models.Restaurant> _restaurantCollection;
    public MongoService(IOptions<MongoSettings> mongoSettings)
    {
        MongoClient client = new MongoClient(mongoSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        _restaurantCollection = database.GetCollection<Restaurant.Models.Restaurant>(mongoSettings.Value.CollectionName); 
    }

    public async Task<List<Restaurant.Models.Restaurant>> GetAsync() { 
       return await _restaurantCollection.Find(new BsonDocument()).ToListAsync();
    }
    public async Task InsertAsync(Restaurant.Models.Restaurant restaurant){
       await _restaurantCollection.InsertOneAsync(restaurant);
       
    }

}
