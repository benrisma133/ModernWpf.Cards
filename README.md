# ModernWpf.Cards v1.0.5

A modern, customizable WPF card control library for .NET 8 desktop applications. This release adds action buttons to the footer of `InfoCard`.

## Installation

Install from NuGet.org:

```bash
dotnet add package ModernWpf.Cards -v 1.0.5
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

## What's New in this Version

- **Action buttons:** add buttons such as "Undo", "Retry", or "View Details" to the bottom of a card. The buttons appear only when you add at least one action.
- **`ActionClicked` event:** raised when a button is clicked, with the button's `Id` so you know which one was pressed.
- **Automatic close:** clicking any action button closes the card.
- **Shared button style:** action buttons use `CardActionButtonStyle`, so they look the same everywhere.

## Quick Start

Add the namespace to your XAML root element:

```xml
xmlns:components="clr-namespace:ModernWpf.Cards.Controls.Components;assembly=ModernWpf.Cards"
```

Show a card with an Undo button:

```xml
<components:InfoCard InfoTitle="File deleted"
                     InfoDescription="The file was moved to the recycle bin."
                     CardType="Warning"
                     ActionClicked="InfoCard_ActionClicked">
    <components:InfoCard.Actions>
        <components:CardAction Id="Undo" Text="Undo" />
        <components:CardAction Id="Details" Text="View Details" />
    </components:InfoCard.Actions>
</components:InfoCard>
```

Handle the click in code-behind:

```csharp
private void InfoCard_ActionClicked(object sender, CardActionEventArgs e)
{
    if (e.ActionId == "Undo")
    {
        RestoreFile();
    }
}
```

Or add the card and its actions in code:

```csharp
var card = new InfoCard
{
    InfoTitle = "File deleted",
    InfoDescription = "The file was moved to the recycle bin.",
    CardType = InfoCard.InfoCardType.Warning
};

card.Actions.Add(new CardAction { Id = "Undo", Text = "Undo" });
card.Actions.Add(new CardAction { Id = "Details", Text = "View Details" });
card.ActionClicked += (s, e) =>
{
    if (e.ActionId == "Undo") RestoreFile();
};

MyContainer.Children.Add(card); // MyContainer is any Panel, e.g. a StackPanel
```

### Handling Close

The card hides itself when the countdown finishes, the close button is clicked, or an action button is clicked. It then raises `CloseClicked`, so you can remove it from your layout:

```csharp
card.CloseClicked += (s, e) => MyContainer.Children.Remove(card);
```

## License

MIT License.