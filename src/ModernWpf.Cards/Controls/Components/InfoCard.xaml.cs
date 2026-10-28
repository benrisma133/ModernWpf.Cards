using FontAwesome.Sharp;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ModernWpf.Cards.Controls.Components
{
    public partial class InfoCard : UserControl
    {
        private bool _isClosed = false;
        private DoubleAnimation? _timerAnimation;
        private AnimationClock? _timerClock;
        private ClockController? _timerController;

        public enum InfoCardType
        {
            Default,
            Information,
            Success,
            Warning,
            Error,
            Question,
            Idea
        }

        public InfoCard()
        {
            InitializeComponent();

            ActionsList.ItemsSource = Actions;
            Actions.CollectionChanged += (s, e) => UpdateFooterVisibility();
            UpdateFooterVisibility();

            this.Loaded += InfoCard_Loaded;
        }

        // --- Action Buttons ---
        public ObservableCollection<CardAction> Actions { get; } = new ObservableCollection<CardAction>();

        public static readonly RoutedEvent ActionClickedEvent =
            EventManager.RegisterRoutedEvent("ActionClicked", RoutingStrategy.Bubble, typeof(CardActionEventHandler), typeof(InfoCard));

        public event CardActionEventHandler ActionClicked
        {
            add { AddHandler(ActionClickedEvent, value); }
            remove { RemoveHandler(ActionClickedEvent, value); }
        }

        private void UpdateFooterVisibility()
        {
            ActionsFooter.Visibility = Actions.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isClosed) return;
            if (((FrameworkElement)sender).DataContext is not CardAction action) return;

            // Tell the host which action was clicked, then close the card
            RaiseEvent(new CardActionEventArgs(ActionClickedEvent, this, action));

            _isClosed = true;
            RaiseEvent(new RoutedEventArgs(CloseClickedEvent));
        }

        // --- Dependency Properties ---
        public static readonly DependencyProperty AutoCloseDelayProperty =
            DependencyProperty.Register("AutoCloseDelay", typeof(int), typeof(InfoCard), new PropertyMetadata(3000));

        public int AutoCloseDelay
        {
            get { return (int)GetValue(AutoCloseDelayProperty); }
            set { SetValue(AutoCloseDelayProperty, value); }
        }

        public static readonly DependencyProperty InfoIconProperty =
            DependencyProperty.Register(nameof(InfoIcon), typeof(IconChar), typeof(InfoCard), new PropertyMetadata(IconChar.InfoCircle));

        public IconChar InfoIcon
        {
            get { return (IconChar)GetValue(InfoIconProperty); }
            set { SetValue(InfoIconProperty, value); }
        }

        public static readonly DependencyProperty InfoTitleProperty =
            DependencyProperty.Register(nameof(InfoTitle), typeof(string), typeof(InfoCard), new PropertyMetadata("Title"));

        public string InfoTitle
        {
            get { return (string)GetValue(InfoTitleProperty); }
            set { SetValue(InfoTitleProperty, value); }
        }

        public static readonly DependencyProperty InfoDescriptionProperty =
            DependencyProperty.Register(nameof(InfoDescription), typeof(string), typeof(InfoCard), new PropertyMetadata("Description"));

        public string InfoDescription
        {
            get { return (string)GetValue(InfoDescriptionProperty); }
            set { SetValue(InfoDescriptionProperty, value); }
        }

        public static readonly DependencyProperty IconBackgroundProperty =
            DependencyProperty.Register(nameof(IconBackground), typeof(Brush), typeof(InfoCard), new PropertyMetadata(null));

        public Brush IconBackground
        {
            get { return (Brush)GetValue(IconBackgroundProperty); }
            set { SetValue(IconBackgroundProperty, value); }
        }

        public static readonly RoutedEvent CloseClickedEvent =
            EventManager.RegisterRoutedEvent("CloseClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(InfoCard));

        public event RoutedEventHandler CloseClicked
        {
            add { AddHandler(CloseClickedEvent, value); }
            remove { RemoveHandler(CloseClickedEvent, value); }
        }

        private void InfoCard_Loaded(object sender, RoutedEventArgs e)
        {
            if (AutoCloseDelay > 0)
            {
                _timerAnimation = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.0,
                    Duration = TimeSpan.FromMilliseconds(AutoCloseDelay),
                    FillBehavior = FillBehavior.HoldEnd
                };

                _timerAnimation.Completed += (s, ev) =>
                {
                    if (!_isClosed && this.Visibility == Visibility.Visible)
                    {
                        _isClosed = true;
                        RaiseEvent(new RoutedEventArgs(CloseClickedEvent));
                    }
                };

                _timerClock = _timerAnimation.CreateClock();
                _timerController = _timerClock.Controller;

                TimerScaleTransform.ApplyAnimationClock(ScaleTransform.ScaleXProperty, _timerClock);
            }
            else
            {
                TimerBar.Visibility = Visibility.Collapsed;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isClosed)
            {
                _isClosed = true;
                RaiseEvent(new RoutedEventArgs(CloseClickedEvent));
            }
        }

        // --- Mouse Hover Pause / Resume ---
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (_timerController != null && _timerClock?.CurrentState == ClockState.Active)
            {
                _timerController.Pause();
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (_timerController != null && _timerClock?.CurrentState == ClockState.Active)
            {
                _timerController.Resume();
            }
        }

        // --- Card Type Logic ---
        public static readonly DependencyProperty CardTypeProperty =
            DependencyProperty.Register("CardType", typeof(InfoCardType), typeof(InfoCard),
                new PropertyMetadata(InfoCardType.Default, OnCardTypeChanged));

        public InfoCardType CardType
        {
            get { return (InfoCardType)GetValue(CardTypeProperty); }
            set { SetValue(CardTypeProperty, value); }
        }

        private static void OnCardTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var card = (InfoCard)d;
            var newType = (InfoCardType)e.NewValue;

            switch (newType)
            {
                case InfoCardType.Success:
                    card.InfoIcon = IconChar.CheckCircle;
                    card.IconBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#10B981"));
                    break;
                case InfoCardType.Error:
                    card.InfoIcon = IconChar.ExclamationCircle;
                    card.IconBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EF4444"));
                    break;
                case InfoCardType.Information:
                    card.InfoIcon = IconChar.InfoCircle;
                    card.IconBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3B82F6"));
                    break;
                case InfoCardType.Warning:
                    card.InfoIcon = IconChar.ExclamationTriangle;
                    card.IconBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F59E0B"));
                    break;
                case InfoCardType.Question:
                    card.InfoIcon = IconChar.CircleQuestion;
                    card.IconBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8B5CF6"));
                    break;
                case InfoCardType.Idea:
                    card.InfoIcon = IconChar.Lightbulb;
                    card.IconBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAB308"));
                    break;
                default:
                    card.InfoIcon = IconChar.Circle;
                    card.IconBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64748B"));
                    break;
            }
        }

        // --- Show Icon ---
        public static readonly DependencyProperty ShowIconProperty =
            DependencyProperty.Register(
                    nameof(ShowIcon),
                    typeof(bool),
                    typeof(InfoCard),
                    new PropertyMetadata(true)
                );

        public bool ShowIcon
        {
            get { return (bool)GetValue(ShowIconProperty); }
            set { SetValue(ShowIconProperty, value); }
        }
    }
}