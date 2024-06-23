using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace WebApplication1;

// using System.Web;
// using System.Web.Http.Controllers;




public class MyCustomAuthorisation : IAuthorizationFilter
{
    private readonly string _claimType;
    private readonly string _claimValue;

    public MyCustomAuthorisation(string claimType, string claimValue)
    {
        _claimType = claimType;
        _claimValue = claimValue;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if(context.HttpContext.User.Identities.Any())
        {

        }
        if (!context.HttpContext.User.HasClaim(_claimType, _claimValue))
        {
            // context.Result = new ForbidResult();
        }
    }
   
}