using Crudbyme.Dtos.Crudbyme.Models;
using Crudbyme.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Configuration;
using System.Text;
using Crudbyme;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Add services for MVC and Swagger
        builder.Services.AddControllersWithViews(); // For MVC views
        builder.Services.AddEndpointsApiExplorer(); // For API endpoint exploration

        builder.Services.AddControllersWithViews();
      
        var provider = builder.Services.BuildServiceProvider();
        var config = provider.GetRequiredService<IConfiguration>();
         builder.Services.AddDbContext<studentdbcontext>(options =>
        options.UseSqlServer(config.GetConnectionString("dbcs")));

        //builder.Services.AddSwaggerGen(); // Add Swagger generation

        builder.Services.AddScoped<IStudentRepository, StudentRepository>();
        builder.Services.AddScoped<ICourse, CourseRepository>();
        builder.Services.AddScoped<IDepartment, Departmentrepo>();
        //   var key = Encoding.ASCII.GetBytes(Configuration["Jwt:SecretKey"]);
        builder.Services.AddScoped<JwtTokenService>();

        var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]);


        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        builder.Services.AddControllersWithViews();


       
        builder.Services.AddAutoMapper(typeof(StudentProfile)); // Register AutoMapper

        //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //              .AddCookie(options =>
        //              {
        //                  options.LoginPath = "/Auth/Login"; // Path to login page
        //                  options.AccessDeniedPath = "/Auth/AccessDenied"; // Path to access denied page
        //              });


        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {

            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseExceptionHandler("/Home/Error");
            //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //app.UseHsts();
        }
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseAuthentication();

        // Use Authorization Middleware
        app.UseRouting();
       
       // app.UseAuthorization();
        app.UseHsts();  // Enable HTTP Strict Transport Security (HSTS)


        app.MapStaticAssets();
        app.UseAuthorization();

        //app.UseEndpoints(endpoints =>
        //{
        //    endpoints.MapControllerRoute(
        //        name: "default",
        //        pattern: "{controller=Admin}/{action=Index}/{id?}");
        //});

        //app.MapControllerRoute(
        //    name: "default",
        //    pattern: "{controller=Admin }/{action=Index}/{id?}")
        //    .WithStaticAssets();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}");
        });

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();


        app.Run();
    }
}