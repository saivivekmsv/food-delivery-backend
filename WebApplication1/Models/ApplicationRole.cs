using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace WebApplication1;
[CollectionName("Roles")]
public class ApplicationRole : MongoIdentityRole<Guid>
{

}
