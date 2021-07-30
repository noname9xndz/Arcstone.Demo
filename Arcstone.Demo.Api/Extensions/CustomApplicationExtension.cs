using Microsoft.AspNetCore.Builder;

namespace Arcstone.Demo.Api.Extensions
{
    public static class CustomApplicationExtension
    {
        public static IApplicationBuilder AddSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "v1");
            });
            return app;
        }
    }
}