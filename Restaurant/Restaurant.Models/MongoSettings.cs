﻿namespace Restaurant.Models;

public class MongoSettings
{
  public string ConnectionURI { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;
}
