using API.Data;
using API.Helper;
using API.Models;
using API.Repos;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureService(IServiceCollection serviceCollection)
    {

        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();

        serviceCollection.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API",
                Version = "v1",
                Description = "My Dating App Apis"
            });
        });

        serviceCollection.AddControllers();

        if (!Configuration.GetValue<Boolean>("ASPNETCORE_ENVIRONMENT", true))
        {
            Console.WriteLine("--> SQL Server is running ...");
            serviceCollection.AddDbContext<AppDbContext>(opt => { opt.UseInMemoryDatabase("InMemory"); });
        }
        else
        serviceCollection.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(Configuration.GetConnectionString("DEFAULT_STRING"))
        .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.NonOwnershipInverseNavigationWarning));
        });

        IdentityBuilder builder = serviceCollection.AddIdentityCore<User>(opt =>
        {
            opt.Password = new PasswordOptions()
            {
                RequireDigit = true,
                RequiredLength = 5,
                RequireNonAlphanumeric = true,
                RequireUppercase = false
            };
        });

        builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
        builder.AddRoleStore<AppDbContext>();
        builder.AddRoleValidator<Role>();
        builder.AddRoleManager<Role>();
        builder.AddSignInManager<SignInManager<User>>();
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });

        serviceCollection.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            options.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));
            options.AddPolicy("VipOnly", policy => policy.RequireRole("VIP"));
        });

        serviceCollection.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.ReferenceHandler =
                    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        serviceCollection.BuildServiceProvider()?.GetService<AppDbContext>()?.Database.Migrate();
        serviceCollection.AddCors();

        // serviceCollection.AddAutoMapper();
        // serviceCollection.AddTransient<Seed>();
        serviceCollection.AddScoped<LogUserActivity>();
        serviceCollection.AddScoped<IDatingRepository, DatingRepository>();
        
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
    
        app.UseCors(policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}
