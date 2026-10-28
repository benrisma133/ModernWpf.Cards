using System.Windows;

namespace ModernWpf.Cards.Controls.Components
{
    /// <summary>A button shown in the footer of an InfoCard.</summary>
    public class CardAction : DependencyObject
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(CardAction), new PropertyMetadata(string.Empty));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register(nameof(Id), typeof(string), typeof(CardAction), new PropertyMetadata(string.Empty));

        public string Id
        {
            get { return (string)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }
    }

    public delegate void CardActionEventHandler(object sender, CardActionEventArgs e);

    public class CardActionEventArgs : RoutedEventArgs
    {
        public CardAction Action { get; }

        public string ActionId => Action.Id;

        public CardActionEventArgs(RoutedEvent routedEvent, object source, CardAction action)
            : base(routedEvent, source)
        {
            Action = action;
        }
    }
}