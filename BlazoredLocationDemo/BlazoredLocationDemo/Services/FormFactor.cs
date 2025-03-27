using BlazoredLocation.Services;
using BlazoredLocationDemo.Shared.Services;

namespace BlazoredLocationDemo.Services
{
    public class FormFactor : IFormFactor
    {
        private readonly IDeviceLocation deviceLocation;
        public FormFactor(IDeviceLocation deviceLocation)
        {
            this.deviceLocation = deviceLocation;
        }
        public string GetFormFactor()
        {
            return DeviceInfo.Idiom.ToString();
        }

        public async Task<BlazoredLocation.Entities.Geolocation> GetGeolocation()
        {
            BlazoredLocation.Entities.Geolocation geolocation = await deviceLocation.GetDeviceLocation(true);
            return geolocation;
        }

        public string GetPlatform()
        {
            return DeviceInfo.Platform.ToString() + " - " + DeviceInfo.VersionString;
        }
    }
}
