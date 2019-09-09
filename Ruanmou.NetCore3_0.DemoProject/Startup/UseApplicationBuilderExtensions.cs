using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Ruanmou.NetCore3_0.DemoProject
{
    public static class UseApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseAuthorize(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                if (context.User.Identities.First().Claims.Any())
                {
                    Thread.CurrentPrincipal = context.User;
                }
                await next();

            });
        }

    }
}
