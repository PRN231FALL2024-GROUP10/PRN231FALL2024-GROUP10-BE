using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using BMOS.BAL.Exceptions;
using BMOS.BAL.Helpers;

namespace JobScial.BAL.Authorization
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private string[] _roles;
        public PermissionAuthorizeAttribute(params string[] roles)
        {
            this._roles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var expiredClaim = long.Parse(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expiredDate = DateHelper.ConvertUnixTimeToDateTime(expiredClaim);
                if (expiredDate <= DateTime.UtcNow)
                {
                    context.Result = new StatusCodeResult(401);
                }
                else
                {
                    var roleClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type.ToLower() == "role");
                    if (this._roles.FirstOrDefault(x => x.ToLower().Equals(roleClaim.Value.ToLower())) == null)
                    {
                        context.Result = new StatusCodeResult(403);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                context.Result = new StatusCodeResult(401);
            }
        }
    }
}
