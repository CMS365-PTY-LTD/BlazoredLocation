using BlazoredLocation.Entities;
using BlazoredLocation.Services;
using BlazoredLocationDemo.Shared.Services;

namespace BlazoredLocationDemo.Web.Services
{
    public class FormFactor : IFormFactor
    {
        private readonly IBrowserLocation browserLocation;
        public FormFactor(IBrowserLocation browserLocation)
        {
            this.browserLocation = browserLocation;
        }
        public string GetFormFactor()
        {
            return "Web";
        }

        public async Task<Geolocation> GetGeolocation()
        {
            Geolocation geolocation = await browserLocation.GetBrowserLocation();
            return geolocation;
        }

        public string GetPlatform()
        {
            return Environment.OSVersion.ToString();
        }
    }
}
