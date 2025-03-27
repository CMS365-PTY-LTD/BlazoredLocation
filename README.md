# BlazoredLocation: A lightweight, easy-to-use library for geolocation services in MAUI apps.
[![NuGet version](https://img.shields.io/nuget/v/CMS365.BlazoredLocation.svg?maxAge=3600)](https://www.nuget.org/packages/CMS365.BlazoredLocation/)
![GitHub last commit (main)](https://img.shields.io/github/last-commit/CMS365-PTY-LTD/BlazoredLocation/main.svg?logo=github)
![build-dotnet workflow](https://github.com/CMS365-PTY-LTD/BlazoredLocation/actions/workflows/build-dotnet.yml/badge.svg)
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

## Dependency Injection for BlazoredLocationDemo.Web
```pwsh
builder.Services.AddScoped<IBrowserLocation, BrowserLocation>();
```
Inject in the component where you want to access location, for example 

@inject IBrowserLocation BrowserLocation

Create OnAfterRenderAsync If it does not exist and call GetBrowserLocation()

```
protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            BlazoredLocation.Entities.Geolocation geolocation = await BrowserLocation.GetBrowserLocation();
        }
    }
```
Run the web project and when the home component loads, user will be shown a confirmation popup for location sharing.

For example: 

![alt text](https://github.com/CMS365-PTY-LTD/BlazoredLocation/blob/main/BlazoredLocation/Screenshots/user-confirmation.png?raw=true)

geolocation variable is now has the current location or error If any.