using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModernWpf.Cards.Controls.Components
{
    /// <summary>
    /// Interaction logic for InfoCard.xaml
    /// </summary>
    public partial class InfoCard : UserControl
    {
        private bool _isClosed = false;

        public enum InfoCardType
        {
            Default,      // العادي
            Information,  // معلومة
            Success,      // نجاح
            Warning,      // تحذير
            Error,        // خطأ
            Question,     // سؤال/استفسار
            Idea          // فكرة/تلميح
        }

        public InfoCard()
        {
            InitializeComponent();

            this.Loaded += InfoCard_Loaded;
        }

        // ====================================
        //  Auto Close Delay Dependency Property
        // ====================================

        public static readonly DependencyProperty AutoCloseDelayProperty =
            DependencyProperty.Register(
                    "AutoCloseDelay",
                    typeof(int),
                    typeof(InfoCard),
                    new PropertyMetadata(3000)
                );

        public int AutoCloseDelay
        {
            get { return (int)GetValue(AutoCloseDelayProperty); }
            set { SetValue(AutoCloseDelayProperty, value); }
        }

        // ====================================
        //  Icon Dependency Property
        // ====================================

        public static readonly DependencyProperty InfoIconProperty =
            DependencyProperty.Register(
                    nameof(InfoIcon),
                    typeof(IconChar),
                    typeof(InfoCard),
                    new PropertyMetadata(IconChar.InfoCircle)
                );

        public IconChar InfoIcon
        {
            get { return (IconChar)GetValue(InfoIconProperty); }
            set { SetValue(InfoIconProperty, value); }
        }

        // ====================================
        //  Title Dependency Property
        // ====================================

        public static readonly DependencyProperty InfoTitleProperty =
            DependencyProperty.Register(
                    nameof(InfoTitle),
                    typeof(string),
                    typeof(InfoCard),
                    new PropertyMetadata("Title", OnTitleChanged)
                );

        public string InfoTitle
        {
            get { return (string)GetValue(InfoTitleProperty); }
            set { SetValue(InfoTitleProperty, value); }
        }

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var card = d as InfoCard;
            string? newValue = e.NewValue as string;
        }

        // ====================================
        //  Description Dependency Property
        // ====================================

        public static readonly DependencyProperty InfoDescriptionProperty =
            DependencyProperty.Register(
                    nameof(InfoDescription),
                    typeof(string),
                    typeof(InfoCard),
                    new PropertyMetadata("Description", OnDescriptionChanged)
                );

        public string InfoDescription
        {
            get { return (string)GetValue(InfoDescriptionProperty); }
            set { SetValue(InfoDescriptionProperty, value); }
        }

        private static void OnDescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var card = d as InfoCard;
            string? newValue = e.NewValue as string;
        }

        // ======================================
        //  Icon Background Dependency Property
        // ======================================

        public static readonly DependencyProperty IconBackgroundProperty =
            DependencyProperty.Register(
                    nameof(IconBackground),
                    typeof(Brush),
                    typeof(InfoCard),
                    new PropertyMetadata(null)
                );

        public Brush IconBackground
        {
            get { return (Brush)GetValue(IconBackgroundProperty); }
            set { SetValue(IconBackgroundProperty, value); }
        }


        public static readonly RoutedEvent CloseClickedEvent =
            EventManager.RegisterRoutedEvent(
                "CloseClicked",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(InfoCard)
            );

        public event RoutedEventHandler CloseClicked
        {
            add { AddHandler(CloseClickedEvent, value); }
            remove { RemoveHandler(CloseClickedEvent, value); }
        }

        // 2. ملي كيتكليكا زر الإغلاق لداخل (✖️)
        private async void InfoCard_Loaded(object sender, RoutedEventArgs e)
        {
            // كنتسناو 3 ثواني (3000 ميلي ثانية) بلا ما نبلوكيو الشاشة
            await Task.Delay(AutoCloseDelay);

            // يلا دازت 3 ثواني واليوزر مازال ما سدهاش بيدو
            if (!_isClosed)
            {
                _isClosed = true;
                // كنطلقو الحدث بحال يلا كليكا على الزر
                RaiseEvent(new RoutedEventArgs(CloseClickedEvent));
            }
        }

        // 4. ملي كيكليكي اليوزر على زر (✖️)
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isClosed)
            {
                _isClosed = true;
                RaiseEvent(new RoutedEventArgs(CloseClickedEvent));
            }
        }


        // 1. تسجيل الخاصية مع Event ملي تبدل
        public static readonly DependencyProperty CardTypeProperty =
            DependencyProperty.Register("CardType", typeof(InfoCardType), typeof(InfoCard),
                new PropertyMetadata(InfoCardType.Default, OnCardTypeChanged));

        public InfoCardType CardType
        {
            get { return (InfoCardType)GetValue(CardTypeProperty); }
            set { SetValue(CardTypeProperty, value); }
        }

        // 2. هاد الـ Event غيخدم ملي اليوزر يكتب مثلا: CardType="Success"
        private static void OnCardTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var card = (InfoCard)d;
            var newType = (InfoCardType)e.NewValue;

            // 3. بدّل الأيقونة واللون على حسب النوع
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

                case InfoCardType.Default:
                    card.InfoIcon = IconChar.Circle;
                    card.IconBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#64748B"));
                    break;
            }
        }
    }
}
