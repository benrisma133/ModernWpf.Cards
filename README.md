# ModernWpf.Cards v1.0.4

A modern, customizable WPF card control library for .NET 8 desktop applications. This release adds custom icons, icon backgrounds, and icon visibility control to `InfoCard`.

## Installation

Install from NuGet.org:

```bash
dotnet add package ModernWpf.Cards -v 1.0.4
```

Install from GitHub Packages (optional):

Register the source once with a personal access token that has the `read:packages` scope:

```bash
dotnet nuget add source https://nuget.pkg.github.com/OWNER/index.json \
  --name github \
  --username YOUR_USERNAME \
  --password YOUR_TOKEN \
  --store-password-in-clear-text
```

Then install the package:

```bash
dotnet add package ModernWpf.Cards -v 1.0.4 --source github
```

> Replace `OWNER`, `YOUR_USERNAME`, and `YOUR_TOKEN` with your own values.

## What's New in this Version

- **`InfoIcon`:** choose any icon from FontAwesome (`IconChar`), such as `Download` or `Bell`.
- **`IconBackground`:** set the icon background with any `Brush`. The progress bar uses the same brush.
- **`ShowIcon`:** show or hide the icon. It is shown by default.

## Quick Start

Add the namespace to your XAML root element:

```xml
xmlns:components="clr-namespace:ModernWpf.Cards.Controls.Components;assembly=ModernWpf.Cards"
```

Show a card with a custom icon and background:

```xml
<components:InfoCard InfoTitle="Custom Download"
                     InfoDescription="Your file is downloading with custom branding."
                     InfoIcon="Download"
                     IconBackground="#8B5CF6"
                     ShowIcon="True"
                     AutoCloseDelay="5000" />
```

Hide the icon:

```xml
<components:InfoCard InfoTitle="Plain Notice"
                     InfoDescription="This card has no icon."
                     ShowIcon="False" />
```

Or create the card in code:

```csharp
using System.Windows.Media;
using FontAwesome.Sharp;
using ModernWpf.Cards.Controls.Components;

var card = new InfoCard
{
    InfoTitle = "Custom Download",
    InfoDescription = "Your file is downloading with custom branding.",
    InfoIcon = IconChar.Download,
    IconBackground = new SolidColorBrush(Color.FromRgb(0x8B, 0x5C, 0xF6)), // #8B5CF6
    ShowIcon = true,
    AutoCloseDelay = 5000
};

MyContainer.Children.Add(card); // MyContainer is any Panel, e.g. a StackPanel
```

### Handling Close

The card hides itself when the countdown finishes or the close button is clicked. It then raises `CloseClicked`, so you can remove it from your layout:

```csharp
card.CloseClicked += (s, e) => MyContainer.Children.Remove(card);
```

> If you set `CardType` together with a custom `InfoIcon` or `IconBackground`, the card type's values can overwrite them. Set `CardType` first, then set the custom values in code, or leave `CardType` out.

## License

MIT License.