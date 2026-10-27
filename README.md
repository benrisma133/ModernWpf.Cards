# ModernWpf.Cards
 
A modern, clean, and customizable WPF card control library for **.NET 8** desktop applications. Smooth animations, auto-close timer bars, pause-on-hover, and rich styling options.
 
---
 
## ✨ Features
 
- 🎨 **Six built-in card types** with automatic colors and icons
- ⏱️ **Auto-close** with a smooth, scaling `TimerBar`
- 🖱️ **Pause on hover**: the countdown stops while the mouse is over the card
- 🖌️ **Custom styling**: override icons, icon backgrounds, and progress bar color
- 👁️ **Icon control**: show or hide the icon with a single property
---
 
## 📦 Installation
 
The package is published to **GitHub Packages**. First, add the source to your environment (one time) with a personal access token that has the `read:packages` scope:
 
```bash
dotnet nuget add source https://nuget.pkg.github.com/OWNER/index.json \
  --name github \
  --username YOUR_USERNAME \
  --password YOUR_TOKEN \
  --store-password-in-clear-text
```
 
Then reference the package in your `.csproj`:
 
```xml
<PackageReference Include="ModernWpf.Cards" Version="1.0.4" />
```
 
> Replace `OWNER`, `YOUR_USERNAME`, and `YOUR_TOKEN` with your own values.
 
---
 
## 🚀 Quick Start
 
Add the namespace to your XAML root element, then place an `InfoCard` anywhere in your layout:
 
```xml
xmlns:components="clr-namespace:YourNamespace.Components;assembly=ModernWpf.Cards"
 
<components:InfoCard InfoTitle="Welcome"
                     InfoDescription="This is a basic info card."
                     CardType="Information" />
```
 
---
 
## 🎨 Card Types
 
Each card type comes with its own color scheme and icon.
 
| Card Type     | Typical Use                          |
|---------------|--------------------------------------|
| `Information` | General messages and tips            |
| `Success`     | Completed operations                 |
| `Warning`     | Cautions that need attention         |
| `Error`       | Failures and problems                |
| `Question`    | Prompts that ask for input           |
| `Idea`        | Suggestions and highlights           |
| `Default`     | Neutral, unstyled messages           |
 
```xml
<components:InfoCard InfoTitle="Success"
                     InfoDescription="Operation completed successfully."
                     CardType="Success" />
```
 
---
 
## ⏱️ Auto-Close & Timer
 
Set `AutoCloseDelay` (in milliseconds) and the card dismisses itself once the countdown finishes. A `TimerBar` shows the remaining time with a smooth animation.
 
```xml
<components:InfoCard InfoTitle="Auto Closing"
                     InfoDescription="This card will dismiss automatically."
                     CardType="Warning"
                     AutoCloseDelay="3000" />
```
 
### Pause on Hover
 
Hovering over the card pauses the countdown. Moving the mouse away resumes it.
 
```xml
<components:InfoCard InfoTitle="Interactive Timer"
                     InfoDescription="Hover over me to pause the countdown!"
                     CardType="Information"
                     AutoCloseDelay="5000" />
```
 
---
 
## 🖌️ Custom Icons & Backgrounds
 
Take full control of the look with three properties:
 
- `InfoIcon`: the icon to display
- `IconBackground`: the icon background color, which also colors the progress bar
- `ShowIcon`: toggles icon visibility
```xml
<components:InfoCard InfoTitle="Custom Download"
                     InfoDescription="Your file is downloading with custom branding."
                     InfoIcon="Download"
                     IconBackground="#8B5CF6"
                     ShowIcon="True"
                     AutoCloseDelay="5000" />
```
 
---
 
## 🧩 Properties
 
| Property          | Type     | Description                                                      |
|-------------------|----------|------------------------------------------------------------------|
| `InfoTitle`       | `string` | Title text shown at the top of the card                          |
| `InfoDescription` | `string` | Body text of the card                                            |
| `CardType`        | `enum`   | Visual style: `Information`, `Success`, `Warning`, `Error`, `Question`, `Idea`, `Default` |
| `AutoCloseDelay`  | `int`    | Time in milliseconds before the card closes automatically        |
| `InfoIcon`        | `string` | Icon name to display in place of the default icon                |
| `IconBackground`  | `Color`  | Icon background color; also applied to the progress bar          |
| `ShowIcon`        | `bool`   | Shows or hides the icon                                          |
 
---
 
## 🗂️ Changelog
 
| Version | Highlights |
|---------|------------|
| **1.0.4** 🌟 | Custom icons (`InfoIcon`), custom icon backgrounds (`IconBackground`), and icon visibility (`ShowIcon`) |
| **1.0.3** | Pause the auto-close timer on hover and resume on leave |
| **1.0.2** | Auto-close countdown with a smooth `TimerBar` and the `AutoCloseDelay` property |
| **1.0.1** | Standard card types (Success, Error, Warning, Question, Idea, Default) with automatic styling and built-in FontAwesome icons |
| **1.0.0** | Initial release with the `InfoCard` component, title and description support |
 
---
 
## 📄 License
 
This project is licensed under the [MIT License](LICENSE).