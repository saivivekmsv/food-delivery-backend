using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Models;


namespace Restaurant.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class RestaurantController: Controller
{

    private readonly ILogger<RestaurantController> _logger;
    private readonly MongoService _mongoService;

    public RestaurantController(ILogger<RestaurantController> logger, MongoService mongoService)
    {
        _logger = logger;
        _mongoService = mongoService;
    }
    [HttpGet]
    public async Task<List<Restaurant.Models.Restaurant>> GetRestaurants()
    {
        
        return await _mongoService.GetAsync();
    }
    [HttpPost]
    public async Task<IActionResult> AddRestaurant([FromBody]Restaurant.Models.Restaurant restaurant)
    {
        await _mongoService.InsertAsync(restaurant);
        return Ok();
    }
    // [HttpGet]
    // public async Task<List<Restaurant.Models.Restaurant>> GetRestaurantsMenu(int id)
    // {
    //     var restaurants = new List<Restaurant.Models.Restaurant>(){
    //         new Models.Restaurant {Id = 1, Name = "mefil", ActiveStatus = true},
    //         new Models.Restaurant {Id = 2, Name = "hyderbad chefs", ActiveStatus = false},
    //         new Models.Restaurant {Id = 3, Name = "rayalseema", ActiveStatus = true},
    //         new Models.Restaurant {Id = 4, Name = "irani", ActiveStatus = false},
    //     };
    //     return restaurants;
    // }

}
