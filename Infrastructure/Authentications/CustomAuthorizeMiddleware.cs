using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentications
{
    public static class CustomAuthorizeMiddleware
    {
        public static IApplicationBuilder UseAuthorizeMidleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthorizeMiddleware>();
        }
    }
}
