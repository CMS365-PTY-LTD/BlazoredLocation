using BlazoredLocation.Entities;

namespace BlazoredLocationDemo.Shared.Services
{
    public interface IFormFactor
    {
        public string GetFormFactor();
        public string GetPlatform();
        public Task<Geolocation> GetGeolocation();
    }
}
