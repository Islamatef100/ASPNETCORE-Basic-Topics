using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Relarning.Enum;
using System.Security.Claims;

namespace Relarning.Authentications
{
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public Permission permission { get; }
        public AuthorizationAttribute(Permission _permission)
        {
            permission = _permission;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasAttribute = context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is AuthorizationAttribute);
            if (hasAttribute is not null)
            {
                var attibute = ((AuthorizationAttribute)hasAttribute).permission;
                var identity = (ClaimsIdentity)context.HttpContext.User.Identity;
                if (!identity.IsAuthenticated || identity is null)
                    context.Result  = new ForbidResult("User not have the required permissions");
                // here call database and check is user have this permission or not.
            }
        }
    }
}
