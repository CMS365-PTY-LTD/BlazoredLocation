using BlazoredLocation.Entities;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Threading;
using System.Threading.Tasks;
using BlazoredGeolocation = BlazoredLocation.Entities.Geolocation;
using MauiGeolocation = Microsoft.Maui.Devices.Sensors.Geolocation;

namespace BlazoredLocation.Services
{
    public class DeviceLocation : IDeviceLocation
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;
        public async Task<BlazoredGeolocation> GetDeviceLocation(bool getCachedLocationIfAvailable)
        {
            if (getCachedLocationIfAvailable)
            {
                return await GetCachedLocation();
            }
            return await GetCurrentLocation();
        }
        public async Task<BlazoredGeolocation> GetCurrentLocation()
        {
            BlazoredGeolocation geolocation = null;
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await MauiGeolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

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
                if (geolocation != null)
                {
                    throw new Exception();
                }
            }
            catch (FeatureNotSupportedException)
            {
                geolocation = new() { Message = "Location information is unavailable.", Code = LocationErrorsEnum.POSITION_UNAVAILABLE };
            }
            catch (FeatureNotEnabledException)
            {
                geolocation = new() { Message = "Location information is unavailable.", Code = LocationErrorsEnum.POSITION_UNAVAILABLE };
            }
            catch (PermissionException)
            {
                geolocation = new() { Message = "User denied the request for Geolocation.", Code = LocationErrorsEnum.PERMISSION_DENIED };
            }
            catch (UnauthorizedAccessException)
            {
                geolocation = new() { Message = "Your App does not have permission to access location data.", Code = LocationErrorsEnum.PERMISSION_DENIED };
            }
            catch (Exception)
            {
                geolocation = new() { Message = "An unknown error occurred.", Code = LocationErrorsEnum.UNKNOWN_ERROR };
            }
            finally
            {
                _isCheckingLocation = false;
            }
            return geolocation;
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }
        private async Task<BlazoredGeolocation> GetCachedLocation()
        {
            BlazoredGeolocation geolocation = null;
            try
            {
                Location location = await MauiGeolocation.Default.GetLastKnownLocationAsync();

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
                else
                {
                    geolocation = await GetCurrentLocation();
                }
            }
            catch (FeatureNotSupportedException)
            {
                geolocation = new() { Message = "Location information is unavailable.", Code = LocationErrorsEnum.POSITION_UNAVAILABLE };
            }
            catch (FeatureNotEnabledException)
            {
                geolocation = new() { Message = "Location information is unavailable.", Code = LocationErrorsEnum.POSITION_UNAVAILABLE };
            }
            catch (PermissionException)
            {
                geolocation = new() { Message = "User denied the request for Geolocation.", Code = LocationErrorsEnum.PERMISSION_DENIED };
            }
            catch (UnauthorizedAccessException)
            {
                geolocation = new() { Message = "Your App does not have permission to access location data.", Code = LocationErrorsEnum.PERMISSION_DENIED };
            }
            catch (Exception)
            {
                geolocation = new() { Message = "An unknown error occurred.", Code = LocationErrorsEnum.UNKNOWN_ERROR };
            }
            return geolocation;
        }
    }
}
