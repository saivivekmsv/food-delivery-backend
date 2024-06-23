using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace WebApplication1;
[CollectionName("Users")]
public class ApplicationUser: MongoIdentityUser<Guid>
{

}
