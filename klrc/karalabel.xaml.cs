using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace klrc
{
    /// <summary>
    /// Interaction logic for karalabel.xaml
    /// </summary>
    public partial class karalabel : UserControl
    {
        private double duration;
        private List<double> tick;
        private DoubleAnimation myAnimate;
        private Storyboard myStoryboard;
        public karalabel()
        {
            InitializeComponent();
            this.FrontColor = new SolidColorBrush(Colors.Red);
            this.BackColor = new SolidColorBrush(Colors.Black);
            duration = 10000;//10 seconds
            tick = new List<double>();
            lbFr.Width = 0;
            //init animate
            myAnimate = new DoubleAnimation(0, 0, new Duration(TimeSpan.FromMilliseconds(1000)));
            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myAnimate);
            Storyboard.SetTargetProperty(myAnimate, new PropertyPath(TextBlock.WidthProperty));
            Storyboard.SetTarget(myAnimate, this.lbFr);
        }
        /// <summary>
        /// Get the required height and width of the specified text. Uses FortammedText
        /// </summary>
        public static Size MeasureTextSize(TextBlock txtblx, int len)
        {/*FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double fontSize*/
            if (len > txtblx.Text.Length) len = txtblx.Text.Length;
            FormattedText ft = new FormattedText(txtblx.Text.Substring(0,len),
                                                 CultureInfo.CurrentCulture,
                                                 FlowDirection.LeftToRight,
                                                 new Typeface(txtblx.FontFamily , txtblx.FontStyle, txtblx.FontWeight, txtblx.FontStretch),
                                                 txtblx.FontSize,
                                                 Brushes.Black);
            return new Size(ft.Width, ft.Height);
        }
        public void startAnimate()
        {

            Size x = MeasureTextSize(lbBg, 11);

            myAnimate.From = 0;
            myAnimate.To = x.Width;// this.lbBg.ActualWidth;
            myAnimate.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            myStoryboard.Begin();
        }
        public string Text { 
            get{
            return lbBg.Text;
        }
            set
            {
                lbBg.Text = value;
                lbFr.Text = value;
            }
        }

        public Brush BackColor
        {
            get
            {
                return lbBg.Foreground;
            }
            set
            {
                lbBg.Foreground = value;
            }
        }
        public Brush FrontColor
        {
            get
            {
                return lbFr.Foreground;
            }
            set
            {
                lbFr.Foreground = value;
            }
        }
        public FontFamily FontFamily
        {
            get
            {
                return lbBg.FontFamily;
            }
            set
            {
                lbBg.FontFamily = value;
                lbFr.FontFamily = value;
            }
        }
        public double FontSize
        {
            get
            {
                return lbBg.FontSize;
            }
            set
            {
                lbBg.FontSize = value;
                lbFr.FontSize = value;
            }
        }
        
        public void test(){
            //lbOverlay.FontSize

            lbFr.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            lbFr.Arrange(new Rect(lbFr.DesiredSize));
            double x = lbFr.ActualWidth;
        }
    }
}
