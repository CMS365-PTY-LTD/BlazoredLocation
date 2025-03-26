using BlazoredLocation.Entities;
using Microsoft.JSInterop;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazoredLocation.Services
{
    public class BrowserLocation : IBrowserLocation, IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        public BrowserLocation(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BlazoredLocation/scripts/BrowserLocation.js").AsTask());
        }

        public async Task<Geolocation> GetBrowserLocation()
        {
            Geolocation geolocation = null;
            try
            {
                var module = await moduleTask.Value;
                GeolocationPosition geolocationPosition = await module.InvokeAsync<GeolocationPosition>("getBrowserLocation");
                geolocation = new Geolocation() { GeolocationPosition = geolocationPosition };
                return geolocation;
            }
            catch (Exception ex)
            {
                geolocation = JsonSerializer.Deserialize<Geolocation>(ex.Message);
            }
            return geolocation;
        }
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
