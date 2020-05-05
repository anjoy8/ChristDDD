using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Christ3D.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Christ3D.Infrastruct.Identity.Models
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _configuration;

        public AspNetUser(IHttpContextAccessor accessor, IConfiguration configuration)
        {
            _accessor = accessor;
            _configuration = configuration;
        }

        //public string Name => _accessor.HttpContext.User.Identity.Name;
        public string Name => _configuration["Authentication:IdentityServer4:Enabled"].ObjToBool() ? GetClaimsIdentity().FirstOrDefault(c => c.Type == "name")?.Value : _accessor.HttpContext.User.Identity.Name;

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}
