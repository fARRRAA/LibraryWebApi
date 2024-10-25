using Microsoft.EntityFrameworkCore;
using LibraryWebApi.DataBaseContext;
using Microsoft.AspNetCore.CookiePolicy;
using LibraryWebApi.Interfaces;
using LibraryWebApi.Services;
using LibraryWebApi.Controllers;
using Microsoft.AspNetCore.Builder;
using ProxyKit;
namespace LibraryWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddScoped<Check>();
            builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            //builder.Services.AddScoped<IBookService, BookService>();
            //builder.Services.AddScoped<IGenreService, GenreService>();
            //builder.Services.AddScoped<IReaderService, ReaderService>();
            //builder.Services.AddScoped<IRentService, RentService>();
            //builder.Services.AddScoped<IBookExemplarService, BookExemplarService>();
            //builder.Services.AddHttpContextAccessor();

            builder.Services.AddProxy();

            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            //    {
            //        options.TokenValidationParameters = new()
            //        {

            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
            //        };
            //        options.Events = new JwtBearerEvents
            //        {
            //            OnMessageReceived = context =>
            //            {
            //                //var authorizationHeader = context.Request.Headers["Authorization"].ToString();
            //                //if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
            //                //{
            //                //    context.Token = authorizationHeader.Substring("Bearer ".Length).Trim();


            //                //}
            //                context.Token = context.Request.Cookies["wild-cookies"];

            //                return Task.CompletedTask;

            //            }

            //        };

            //    });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            builder.Services.AddDbContext<LibraryWebApiDb>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")), ServiceLifetime.Scoped);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseCors("AllowAllOrigins");
            //app.UseAuthentication();
            //app.UseAuthorization();
            // Rental, Reader, Genre, Auth
            app.UseWhen(context => context.Request.Path.Value.Contains("/api/Books"),
    applicationBuilder => applicationBuilder.RunProxy(context =>
        context.ForwardTo("https://localhost:7018/").AddXForwardedHeaders().Send()));
            app.UseWhen(context => context.Request.Path.Value.Contains("/api/Rental"),
applicationBuilder => applicationBuilder.RunProxy(context =>
context.ForwardTo("https://localhost:7018/").AddXForwardedHeaders().Send()));
            app.UseWhen(context => context.Request.Path.Value.Contains("/api/Genre"),
    applicationBuilder => applicationBuilder.RunProxy(context =>
        context.ForwardTo("https://localhost:7018/").AddXForwardedHeaders().Send()));
            app.UseWhen(context => context.Request.Path.Value.Contains("/api/Reader"),
applicationBuilder => applicationBuilder.RunProxy(context =>
context.ForwardTo("https://localhost:7008/").AddXForwardedHeaders().Send()));
            app.UseWhen(context => context.Request.Path.Value.Contains("/api/Auth"),
applicationBuilder => applicationBuilder.RunProxy(context =>
context.ForwardTo("https://localhost:7008/").AddXForwardedHeaders().Send()));
            //app.UseWhen(context => context.Request.Path.Value.Contains("/api/Genre"),
            //    applicationBuilder => applicationBuilder.RunProxy(context =>
            //        context.ForwardTo("http://localhost:5187").AddXForwardedHeaders().Send()));
            //app.UseWhen(context => context.Request.Path.Value.Contains("/api/Rent"),
            //    applicationBuilder => applicationBuilder.RunProxy(context =>
            //        context.ForwardTo("http://localhost:5187").AddXForwardedHeaders().Send()));
            //app.UseWhen(context => context.Request.Path.Value.Contains("/api/Users"),
            //    applicationBuilder => applicationBuilder.RunProxy(context =>
            //        context.ForwardTo("http://localhost:5178").AddXForwardedHeaders().Send()));

            app.MapControllers();
            app.Run();
        }
    }
}
