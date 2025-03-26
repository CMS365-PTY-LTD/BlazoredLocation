using BlazoredLocation.Entities;
using System.Threading.Tasks;

namespace BlazoredLocation.Services
{
    public interface IBrowserLocation
    {
        public Task<Geolocation> GetBrowserLocation();
    }
}
