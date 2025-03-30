using BlazoredLocation.Services;
using BlazoredLocationDemo.Shared.Services;
using BlazoredLocationDemo.Web.Components;
using BlazoredLocationDemo.Web.Services;

namespace BlazoredLocationDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Add device-specific services used by the BlazoredLocationDemo.Shared project
            builder.Services.AddScoped<IFormFactor, FormFactor>();
            builder.Services.AddScoped<IBrowserLocation, BrowserLocation>();

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
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddAdditionalAssemblies(typeof(BlazoredLocationDemo.Shared._Imports).Assembly);

            app.Run();
        }
    }
}
