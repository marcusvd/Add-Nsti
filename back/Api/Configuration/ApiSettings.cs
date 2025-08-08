using System.Net;
using ImApi.Configuration;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.FileProviders;

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

    public static void UseStaticFilesExtension(this IApplicationBuilder app)
    {
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
            RequestPath = new PathString("/Resources")
        });
    }


    public static void AddCorsApiSettings(this IServiceCollection services)
    {
        
            services.AddCors(opt =>
            {

                opt.AddPolicy("AllowSpecificOrigin", builder =>
                {

                    builder.WithOrigins(
                     "http://sonnyapp.ddns.com.br",
                     "http://sonnyapp.ddns.com.br:80",
                     "http://192.168.200.103",
                     "http://localhost:4200",
                     "http://sonnyapp.intra/"
                     )
                    // builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true);
                });
            });

    }










}