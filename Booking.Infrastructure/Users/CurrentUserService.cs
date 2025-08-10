using Booking.Application.Features.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Users
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var claims = _httpContextAccessor.HttpContext?.User?.Claims;
                foreach (var c in claims)
                {
                    Debug.WriteLine($"Claim: {c.Type} = {c.Value}");
                }

                return userId != null ? Guid.Parse(userId) : Guid.Empty;
            }
        }
    }
}
