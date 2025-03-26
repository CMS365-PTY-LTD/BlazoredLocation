using BlazoredLocation.Entities;
using System.Threading.Tasks;

namespace BlazoredLocation.Services
{
    public interface IDeviceLocation
    {
        public Task<Geolocation> GetDeviceLocation(bool getCachedLocationIfAvailable);
    }
}
