using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazorAppForDataStructures.Data;
using BlazorAppForDataStructures.Models;
using BlazorAppForDataStructures.Services;

namespace BlazorAppForDataStructures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BlazorAppForDataStructuresContext>(options =>
                options.UseSqlServer(connectionString));

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string not found. Please set the environment variable 'MYAPP_CONNECTION_STRING'.");
            }

            // Configure Entity Framework and Identity
            builder.Services.AddDbContext<BlazorAppForDataStructuresContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BlazorAppForDataStructuresContext>();

            builder.Services.AddScoped<QuizService>();
            builder.Services.AddScoped<CancellationService>();
            builder.Services.AddScoped<SecureStorageService>();

            builder.Services.AddScoped<NotificationService>();


            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddScoped(sp =>new HttpClient { BaseAddress = new Uri("https://datastructviz-quiz-api-001-hbcza9gdbpb7gzew.canadacentral-01.azurewebsites.net/") } );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
