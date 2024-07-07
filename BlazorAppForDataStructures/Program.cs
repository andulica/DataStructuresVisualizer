using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazorAppForDataStructures.Data;

namespace BlazorAppForDataStructures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("BlazorAppForDataStructuresContextConnection") ?? throw new InvalidOperationException("Connection string 'BlazorAppForDataStructuresContextConnection' not found.");

                                    builder.Services.AddDbContext<BlazorAppForDataStructuresContext>(options =>
                options.UseSqlServer(connectionString));

                                                builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BlazorAppForDataStructuresContext>();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
                        app.UseAuthentication();;

            app.Run();
        }
    }
}