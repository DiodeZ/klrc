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
        private struct TimeSync
        {
            public int len;
            public Duration duration;
        };
        private List<TimeSync> seriesTime;
        private int indexPlay = 0;
        private DoubleAnimation myAnimate;
        private Storyboard myStoryboard;
        public karalabel()
        {
            InitializeComponent();
            this.FrontColor = new SolidColorBrush(Colors.Red);
            this.BackColor = new SolidColorBrush(Colors.Black);
            seriesTime = new List<TimeSync>();
            lbFr.Width = 0;
            //init animate
            myAnimate = new DoubleAnimation(0, 0, new Duration(TimeSpan.FromMilliseconds(1000)));
            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myAnimate);
            Storyboard.SetTargetProperty(myAnimate, new PropertyPath(TextBlock.WidthProperty));
            Storyboard.SetTarget(myAnimate, this.lbFr);
            myStoryboard.Completed += new EventHandler(StoryBoard_Completed);
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
        public void clearSeriesTime()
        {
            seriesTime.Clear();
        }
        public bool addTimeSync(int textLen, TimeSpan time)
        {
            if (seriesTime.Count > 0)
            {
                if (textLen < seriesTime[seriesTime.Count - 1].len)
                {
                    return false;
                }
            }
            if (textLen > this.Text.Length)
            {
                return false;
            }
            TimeSync tmp = new TimeSync();
            tmp.len = textLen;
            tmp.duration = new Duration(time);
            seriesTime.Add(tmp);
            return true;
        }
        public void startAnimate()
        {

            indexPlay = 0;
            animateIndex(indexPlay);
        }
        private void animateIndex(int i)
        {
            indexPlay += 1;
            if (seriesTime.Count == 0)
            {
                myAnimate.From = 0;
                myAnimate.To = this.lbBg.ActualWidth;
                myAnimate.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                myStoryboard.Begin();
            }
            else
            {
                if (indexPlay - 1 < seriesTime.Count)
                {
                    if (indexPlay > 1)
                    {
                        myAnimate.From = myAnimate.To;
                    }
                    else
                    {
                        myAnimate.From = 0;
                    }
                    Size x = MeasureTextSize(lbBg, seriesTime[indexPlay - 1].len);
                    myAnimate.To = x.Width;
                    if (myAnimate.From > myAnimate.To) //this should be never happen
                    {
                        myAnimate.To = myAnimate.From;
                    }
                    myAnimate.Duration = seriesTime[indexPlay-1].duration;
                    myStoryboard.Begin();

                }

            }
        }
        private void StoryBoard_Completed(object sender, EventArgs e)
        {
            if (indexPlay < seriesTime.Count)
            {
                animateIndex(indexPlay);
            }
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
        new public FontFamily FontFamily
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
        new public double FontSize
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
