using MongoDB.Bson;

namespace Restaurant.Models;

public class Items
{

public ObjectId Id {get;set;}
public string? ItemName {get;set;}
public ItemCategory ItemCategory{get; set;}
public ItemType ItemType {get;set;}

}

public enum ItemCategory{
 Biryani = 1,
 Juice,
 MainCourse,
 Starters

}
public enum ItemType {
    Veg = 1,
    NonVeg
}


//factory pattern
// public enum RestaurantType 
// {
//     Veg,
//     NonVeg
// }

// public abstract class RestaurantDetails()
// {
//     public abstract void PrintDetails();
    
// }

// public class VegRestaurant: RestaurantDetails
// {
//     public override void PrintDetails()
//     {
//         Console.WriteLine("Veg");
//     }
// }
// public class NonVegRestaurant: RestaurantDetails
// {
//     public override void PrintDetails()
//     {
//         Console.WriteLine("Non Veg");
//     }
// }

// public interface IRestaurantFactory
// {
//     RestaurantDetails ResType(RestaurantType restaurantType);
// }

// public class RestaurantFactory: IRestaurantFactory
// {
//     public RestaurantDetails ResType(RestaurantType restaurantType)
//     {
//         switch(restaurantType)
//         {
//             case RestaurantType.Veg:
//              return new VegRestaurant();
//             case RestaurantType.NonVeg:
//               return new NonVegRestaurant();
//             default:
//               return null;
//         }
//     }
// }
