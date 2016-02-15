![logo]
# Rhome Framework
Rhome is for for (R)emote (Home) and provides a unified smart home remote control .NET library. We are targeting a solution that unites all major smart home solutions in one layer to tidy up the mess of different manufacturers and apps.

## How to use
To add this library to one of your projects the simplest way might be the [NuGet package](https://www.nuget.org/packages/Thepagedot.Rhome.HomeMatic).
```
PM> Install-Package Thepagedot.Rhome.HomeMatic -Pre
```

Beside, there is an official [Rhome App](https://github.com/Thepagedot/Rhome-App) that uses the framework and is completely open source. Head over to the repository to see the framework in action and get inspired.

## Ideas and Roadmap
Trello: https://trello.com/b/Nrt0SyCH

## Details
### Supported Home Control solutions
- HomeMatic 
- Philips Hue *(planned)*

> To work with HomeMatic, the [XML-API](https://github.com/hobbyquaker/XML-API) has to be installed on the central unit!

### Supported Devices
These devices can be accessed with our solution by now. Please consider that we are coninously trying to extend this list to support more devices.
#### HomeMatic
##### Should work
- Switchers
- Windows Contacts
- Shutters

##### Tested
- Funk-Schalter 1-Kanal Zwischenstecker (HM-LC-Sw1-Pl)
- Funk-Fenster-Drehgriffkontakt (HM-Sec-RHS)
- Funk-Fensterkontakt (HM-Sec-SC-2)
- Funk-Jalousieaktor, Unterputzmontage (HM-LC-BL1-FM)

[logo]: https://raw.githubusercontent.com/Thepagedot/Rhome/master/Design/Logo.png "Logo"

