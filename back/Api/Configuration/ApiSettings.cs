using System.Net;
using ImApi.Configuration;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Configuration;

public static class ApiSettings
{

    public static void AddGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    await context.Response.WriteAsync(new GlobalExceptionErrorHandling()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message,
                        Trace = contextFeature.Error.StackTrace
                    }.ToString());
                }
            });
        });
    }

    public static void AddNewtonsoftJsonControllers(this IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(opt =>
        {
            opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });
    }

    // public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    // {
    //     app.UseExceptionHandler(appError =>
    //     {
    //         appError.Run(async context =>
    //         {
    //             context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //             context.Response.ContentType = "application/json";

    //             var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
    //             if (contextFeature != null)
    //             {
    //                 await context.Response.WriteAsync(new GlobalErrorHandling()
    //                 {
    //                     
    //                 }.ToString());
    //             }
    //         });
    //     });
    // }

    // public static void ConfigsStartupProject(this IServiceCollection services)
    // {
    //     services.AddControllers().AddNewtonsoftJson(opt =>
    //     {
    //         opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    //     });
    // }
}