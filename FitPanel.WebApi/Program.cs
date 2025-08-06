using FitPanel.Business.Managers;
using FitPanel.Business.Services;
using FitPanel.DataAccess.Contexts;
using FitPanel.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace FitPanel.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); 
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
