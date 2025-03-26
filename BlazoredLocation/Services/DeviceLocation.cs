using BlazoredLocation.Entities;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Threading.Tasks;
using Geolocation = BlazoredLocation.Entities.Geolocation;

namespace BlazoredLocation.Services
{
    public class DeviceLocation : IDeviceLocation
    {
        public async Task<Geolocation> GetDeviceLocation(bool getCachedLocationIfAvailable)
        {
            if (getCachedLocationIfAvailable)
            {
                return await GetCachedLocation();
            }
            return null;
        }
        private async Task<Geolocation> GetCachedLocation()
        {
            Geolocation geolocation = null;
            try
            {
                Location location = await Microsoft.Maui.Devices.Sensors.Geolocation.Default.GetLastKnownLocationAsync();

                if (location != null)
                {
                    geolocation = new()
                    {
                        GeolocationPosition = new()
                        {
                            GeolocationCoordinates = new()
                            {
                                Altitude = location.Altitude,
                                Latitude = location.Latitude,
                                Longitude = location.Longitude
                            }
                        }
                    };
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                geolocation = new() { Message = "Location information is unavailable.", Code = LocationErrorsEnum.POSITION_UNAVAILABLE };
            }
            catch (FeatureNotEnabledException fneEx)
            {
                geolocation = new() { Message = "Location information is unavailable.", Code = LocationErrorsEnum.POSITION_UNAVAILABLE };
            }
            catch (PermissionException pEx)
            {
                geolocation = new() { Message = "User denied the request for Geolocation.", Code = LocationErrorsEnum.PERMISSION_DENIED };
            }
            catch (UnauthorizedAccessException uae)
            {
                geolocation = new() { Message = "Your App does not have permission to access location data.", Code = LocationErrorsEnum.PERMISSION_DENIED };
            }
            catch (Exception ex)
            {
                geolocation = new() { Message = "An unknown error occurred.", Code = LocationErrorsEnum.UNKNOWN_ERROR };
            }
            return geolocation;
        }
    }
}
