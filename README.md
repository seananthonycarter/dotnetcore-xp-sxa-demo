# dotnetcore-xp-sxa-demo

## About this Solution
This solution has been created as an exercise in using Headless SXA with .NET Core on XP. You can refer to the [walkthrough: Using the Getting Started template with SXA](https://doc.sitecore.com/xp/en/developers/hd/latest/sitecore-headless-development/walkthrough--using-the-getting-started-template-with-sxa.html) and follow the steps and commit history to see the changes I made to the boilerplate solution that is created via the [Walkthrough: Using the Getting Started template](https://doc.sitecore.com/xp/en/developers/hd/latest/sitecore-headless-development/walkthrough--using-the-getting-started-template.html)
I have borrowed from the  [XM Cloud ASP.NET Core Starter Kit](https://github.com/Sitecore/xmcloud-starter-dotnet) for the .NET Core solution changes required.


For simplicity, this solution does not implement Sitecore Helix conventions for
solution architecture. As you begin building your Sitecore solution,
you should review [Sitecore Helix](https://helix.sitecore.net/) and the
[Sitecore Helix Examples](https://sitecore.github.io/Helix.Examples/) for guidance
on implementing a modular solution architecture.


## TODO and Known Issues
> [!CAUTION]
> This is not a starter project, rather it's an example of the approach you might take to create your own starter project using similar steps. Currently it works in editing host mode using the Skateboard demo featured in XM Cloud :

![Experience Editor](experience-editing.jpg?raw=true "Experience Editor with XM Cloud Demo")

The following are known issues:
- **Rendering host configuration** - While the rendering host operates in editing mode (editing host) the rendering host has some outstanding configuration issues to render the site
- **.NET Version** - The [XM Cloud ASP.NET Core Starter Kit](https://github.com/Sitecore/xmcloud-starter-dotnet) used .NET Core Version 8 whereas the bolier plate project used version 6 and although the solution builds and works in editing mode, there may be some work to do here


## Prerequisites
* .NET 6.0 SDK
* .NET Framework 4.8 SDK
* Visual Studio 2019
* Docker for Windows, with Windows Containers enabled

See Sitecore Containers documentation for more information on system requirements.

## Running this Solution
1. If your local IIS is listening on port 443, you'll need to stop it.
   > This requires an elevated PowerShell or command prompt.
   ```
   iisreset /stop
   ```

1. Before you can run the solution, you will need to prepare the following
   for the Sitecore container environment:
   * A valid/trusted wildcard certificate for `*.dotnetcore_xp_sxa_demo.localhost`
   * Hosts file entries for `dotnetcore_xp_sxa_demo.localhost`
   * Required environment variable values in `.env` for the Sitecore instance
     * (Can be done once, then checked into source control.)

   See Sitecore Containers documentation for more information on these
   preparation steps. The provided `init.ps1` will take care of them,
   but **you should review its contents before running.**

   > You must use an elevated/Administrator Windows PowerShell 5.1 prompt for
   > this command, PowerShell 7 is not supported at this time.

    ```ps1
    .\init.ps1 -InitEnv -LicenseXmlPath "C:\path\to\license.xml" -AdminPassword "DesiredAdminPassword" -Topology xp1
    ```
    The ```-Topology ``` parameter specify topology you need. This parameter is optional. The default value ```xp0```

    If you check your `.env` into source control, other developers
    can prepare a certificate and hosts file entries by simply running:

    ```ps1
    .\init.ps1
    ```


1. After completing this environment preparation, run the startup script
   from the solution root:
    ```ps1
    .\up.ps1
    ```

1. When prompted, log into Sitecore via your browser, and
   accept the device authorization.

1. Wait for the startup script to open browser tabs for the rendered site
   and Sitecore Launchpad.

## Using the Solution
* A publish of the `Platform` project will update the running `cm` service.
* The running `rendering` service uses `dotnet watch` and will recompile
  automatically for any changes you make. You can also run the `Rendering`
  project directly from Visual Studio.
* Review README's found in the projects and throughout the solution
  for additional information.
* To push/pull items In an ADMIN PowerShell, connect to the Sitecore instance:

    ```ps1
    dotnet sitecore login --cm https://cm.dotnetcore_xp_sxa_demo.localhost/ --auth https://id.dotnetcore_xp_sxa_demo.localhost --allow-write true
    ```


To serialize the items to disk, run dotnet sitecore ser pull
To push the items from disk to the Sitecore instance, run dotnet sitecore ser push