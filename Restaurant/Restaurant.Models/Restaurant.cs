
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Restaurant.Models;

public class Restaurant
{
// private List<Items> _items = new List<Items>();

public ObjectId Id {get; set;}
public string Name{get;set;}

public bool ActiveStatus {get; set;}
[BsonElement("Items")]
[JsonPropertyName("Items")]
public List<Items> Items {get;set;}
}
