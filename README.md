# BlazoredLocation: A lightweight, easy-to-use library for geolocation services in MAUI apps.
[![NuGet version](https://img.shields.io/nuget/v/CMS365.BlazoredLocation.svg?maxAge=3600)](https://www.nuget.org/packages/CMS365.BlazoredLocation/)
![GitHub last commit (main)](https://img.shields.io/github/last-commit/CMS365-PTY-LTD/BlazoredLocation/main.svg?logo=github)
[![License](https://img.shields.io/badge/license-MIT-green)](./LICENSE)

The BlazoredLocation package simplifies geolocation in MAUI apps, enabling easy access to user coordinates (latitude/longitude) with minimal code. It wraps the Geolocation API for seamless async integration.
# Installation

BlazoredLocation is [available on NuGet](https://www.nuget.org/packages/CMS365.BlazoredLocation/). Use the package manager
console in Visual Studio to install it:

```pwsh
Install-Package CMS365.BlazoredLocation
```
# Setup
When you create a project with Blazor hybrid and web app template, you get 3 projects created.

For example: 

![alt text](https://github.com/CMS365-PTY-LTD/BlazoredLocation/blob/main/BlazoredLocation/Screenshots/project-structure.png?raw=true)

Install package in all 3 projects.

Add a new interface method in IFormFactor located at BlazoredLocationDemo.Shared.Services
```
public Task<Geolocation> GetGeolocation();
```

## Dependency Injection for BlazoredLocationDemo.Web
```pwsh
builder.Services.AddScoped<IFormFactor, FormFactor>(); //Chane to AddScoped form AddSingleton
builder.Services.AddScoped<IBrowserLocation, BrowserLocation>();
```

Implement GetGeolocation in FormFactor located at BlazoredLocationDemo.Web.Services
```
private readonly IBrowserLocation browserLocation;
public FormFactor(IBrowserLocation browserLocation)
{
    this.browserLocation = browserLocation;
}
public async Task<Geolocation> GetGeolocation()
{
    Geolocation geolocation = await browserLocation.GetGeolocation();
    return geolocation;
}
```
In the Home.razor or any component where you want to access location, for example 

@inject IFormFactor FormFactor //If it is not already there

Create OnAfterRenderAsync If it does not exist and call GetGeolocation()

```
protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            BlazoredLocation.Entities.Geolocation geolocation = await FormFactor.GetBrowserLocation();
        }
    }
```
Run the web project and when the home component loads, user will be shown a confirmation popup for location sharing.

For example: 

![alt text](https://github.com/CMS365-PTY-LTD/BlazoredLocation/blob/main/BlazoredLocation/Screenshots/web-user-confirmation.png?raw=true)

geolocation variable in the OnAfterRenderAsync now has the current location or error If any.
## Dependency Injection for BlazoredLocationDemo
```pwsh
builder.Services.AddScoped<IFormFactor, FormFactor>();
builder.Services.AddScoped<IDeviceLocation, DeviceLocation>();
```
Implement GetGeolocation in FormFactor located at BlazoredLocationDemo.Services
```
private readonly IDeviceLocation deviceLocation;
public FormFactor(IDeviceLocation deviceLocation)
{
    this.deviceLocation = deviceLocation;
}
public async Task<BlazoredLocation.Entities.Geolocation> GetGeolocation()
{
    BlazoredLocation.Entities.Geolocation geolocation = await deviceLocation.GetDeviceLocation(true);
    return geolocation;
}
```
### Running in Windows Machine

Select Windows Machine and run the project.

geolocation variable in the OnAfterRenderAsync now has the current location or error If any.

You can control location settings in "Location privacy settings" in widows

### Running in Android Emulator

Open AndroidManifest.xml located in BlazoredLocationDemo -> Platforms -> Android -> Resources and assign following permissions

![alt text](https://github.com/CMS365-PTY-LTD/BlazoredLocation/blob/main/BlazoredLocation/Screenshots/android-manifest-permissions.png?raw=true)

Up-to-date information is here https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device/geolocation?view=net-maui-9.0&tabs=android

Select Android Emulator and run the project, you will see a popup and allow permissions

![alt text](https://github.com/CMS365-PTY-LTD/BlazoredLocation/blob/main/BlazoredLocation/Screenshots/android-user-confirmation.png?raw=true)

geolocation variable in the OnAfterRenderAsync now has the current location or error If any.

You can control location settings in Settings in Android device.

### Running in IOS Emulator

Open Info.plist in IOS folder located in BlazoredLocationDemo -> Platforms and assign the following permissions

![alt text](https://github.com/CMS365-PTY-LTD/BlazoredLocation/blob/main/BlazoredLocation/Screenshots/ios-manifest-permissions.png?raw=true)

Open Info.plist in MacCatalyst folder located in BlazoredLocationDemo -> Platforms and assign the following permissions

![alt text](https://github.com/CMS365-PTY-LTD/BlazoredLocation/blob/main/BlazoredLocation/Screenshots/mac-manifest-permissions.png?raw=true)

Up-to-date information https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/device/geolocation?view=net-maui-9.0&tabs=macios

Select IOS Simulator and run the project, you will see a popup and allow permissions

![alt text](https://github.com/CMS365-PTY-LTD/BlazoredLocation/blob/main/BlazoredLocation/Screenshots/ios-user-confirmation.png?raw=true)

**geolocation is being returned as null, possibly due to Simulator I am using and I don't have physical device to test it. Please test and let me know.**