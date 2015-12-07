using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.Effects;
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
        DropShadowEffect myShadow;
        private TextAlignment myTextAlign;
        public event EventHandler ElapsedTime;

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
            //
            myShadow = (DropShadowEffect)this.Resources["mydropshadow"];
            myTextAlign = TextAlignment.Left;
            this.TextPadding = 5;
        }
        /// <summary>
        /// Get the required height and width of the specified text. Uses FortammedText
        /// </summary>
        private Size MeasureTextSize(TextBlock txtblx, int len = -1)
        {/*FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double fontSize*/
            if (len > txtblx.Text.Length || len == -1) len = txtblx.Text.Length;
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
            this.BeginTime = TimeSpan.FromSeconds(0);
            this.EndTime = this.BeginTime;
        }
        public bool setTextAndTimes(string input)
        {
            string realString = "";
            string timestr = "";
            TimeSpan tmptime = TimeSpan.FromSeconds(0);
            int state = 0; //0:read string, 1: read time
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\n') continue;
                if (state == 0)
                {
                    if (input[i] == '[')
                    {
                        state = 1;
                    }
                    else
                    {
                        realString += input[i];
                    }
                }
                else
                {
                    if (input[i] == ']')
                    {
                        state = 0;
                        if (tmptime != TimeSpan.FromSeconds(0))
                        {
                            TimeSpan time1;
                            try
                            {
                                time1 = TimeSpan.ParseExact(timestr, "c", null);
                                TimeSync sync = new TimeSync();
                                sync.duration = new Duration(time1 - tmptime);
                                sync.len = realString.Length;
                                seriesTime.Add(sync);
                                tmptime = time1;
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                                return false;
                            }
                        }
                        else
                        {
                            try
                            {
                                tmptime = TimeSpan.ParseExact(timestr, "c", null);
                                this.BeginTime = tmptime;
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                                return false;
                            }
                        }
                        timestr = "";
                    }
                    else
                    {
                        timestr += input[i];
                    }
                }
            }
            this.Text = realString;
            this.EndTime = this.BeginTime;
            for (int i = 0; i < seriesTime.Count; i++)
            {
                this.EndTime += seriesTime[i].duration.TimeSpan;
            }
            return true;
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
            this.EndTime = this.BeginTime;
            for (int i = 0; i < seriesTime.Count; i++)
            {
                this.EndTime += seriesTime[i].duration.TimeSpan;
            }
            return true;
        }
        public void startAnimate(double beginTimeSecond = -1)
        {
            if (beginTimeSecond == -1)
            {
                indexPlay = 0;
                animateIndex(indexPlay);
            }
            else
            {
                double sumtime = this.BeginTime.TotalSeconds;
                if (seriesTime.Count == 0 || beginTimeSecond < sumtime) return ;
                int i;
                for (i = 0; i < seriesTime.Count; i++)
                {
                    sumtime += seriesTime[i].duration.TimeSpan.TotalSeconds;
                    if (sumtime > beginTimeSecond)
                    {
                        break;
                    }
                }
                if (i < seriesTime.Count)
                {
                    indexPlay = i;
                    animateIndex(i, getWidthAt(beginTimeSecond));
                }
            }
        }
        private double getWidthAt(double timeInSecond)
        {

            double sumtime = this.BeginTime.TotalSeconds;
            if (seriesTime.Count == 0 || timeInSecond < sumtime) return 0;
            int i;
            for (i = 0; i < seriesTime.Count; i++)
            {
                sumtime += seriesTime[i].duration.TimeSpan.TotalSeconds;
                if (sumtime > timeInSecond)
                {
                    break;
                }
            }
            double mywidth;
            if (sumtime > timeInSecond)
            {
                mywidth = (i == 0) ? 0 : MeasureTextSize(lbBg, seriesTime[i - 1].len).Width;
                mywidth += (1 - (sumtime - timeInSecond) / seriesTime[i].duration.TimeSpan.TotalSeconds) * (MeasureTextSize(lbBg, seriesTime[i].len).Width - mywidth);

            }
            else
            {
                mywidth = MeasureTextSize(lbBg, seriesTime[i - 1].len).Width;
            }
            return mywidth;
        }
        private void animateIndex(int i, double startValue = -1)
        {
            this.IsPlayingAnimation = true;
            Debug.WriteLine("animateIndex:: index = " + i.ToString());
            indexPlay += 1;
            if (seriesTime.Count == 0)
            {
                myAnimate.From = startValue == -1 ? 0 : startValue;
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
                    if (startValue != -1) myAnimate.From = startValue;
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
        public void setCurrentTime(double timeInSecond)
        {
            lbFr.Width = getWidthAt(timeInSecond);
        }
        private void StoryBoard_Completed(object sender, EventArgs e)
        {
            Debug.WriteLine("StoryBoard_Completed:: done/total = " + indexPlay.ToString() + "/" + seriesTime.Count.ToString());
            if (indexPlay < seriesTime.Count)
            {
                animateIndex(indexPlay);
            }
            else
            {
                OnElapsedTime(EventArgs.Empty);
                this.IsPlayingAnimation = false;
            }
        }
        private void RePositionText()
        {
            lbBg.Width = MeasureTextSize(lbBg).Width;
            switch (myTextAlign)
            {
                case TextAlignment.Right:
                    lbBg.Margin = new Thickness(this.ActualWidth - lbBg.Width-this.TextPadding,0,0,0);//(left, top, right, bottom);
                    lbFr.Margin = lbBg.Margin;
                    break;
                case TextAlignment.Center:
                    lbBg.Margin = new Thickness((this.ActualWidth - lbBg.Width)/2, 0, 0, 0);//(left, top, right, bottom);
                    lbFr.Margin = lbBg.Margin;
                    break;
                default:
                    lbBg.Margin = new Thickness(this.TextPadding, 0, this.ActualWidth - lbBg.Width - this.TextPadding, 0);//(left, top, right, bottom);
                    lbFr.Margin = lbBg.Margin;
                    break;
            }
        }

        public double TextPadding { get; set; }
        public string Text { 
            get{
            return lbBg.Text;
        }
            set
            {
                lbBg.Text = value;
                lbFr.Text = value;
                RePositionText();
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
                bool changed = lbBg.FontFamily == value;
                lbBg.FontFamily = value;
                lbFr.FontFamily = value;
                if (changed) RePositionText();
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
                bool changed = lbBg.FontSize == value;
                lbBg.FontSize = value;
                lbFr.FontSize = value;
                if (changed) RePositionText();
            }
        }
        new public FontStretch FontStretch {
            get
            {
                return lbBg.FontStretch;
            }
            set
            {
                bool changed = lbBg.FontStretch == value;
                lbBg.FontStretch = value;
                lbFr.FontStretch = value;
                if (changed) RePositionText();
            }
        }
        new public FontStyle FontStyle
        {
            get
            {
                return lbBg.FontStyle;
            }
            set
            {
                bool changed = lbBg.FontStyle == value;
                lbBg.FontStyle = value;
                lbFr.FontStyle = value;
                if (changed) RePositionText();
            }
        }
        new public FontWeight FontWeight
        {
            get
            {
                return lbBg.FontWeight;
            }
            set
            {
                bool changed = lbBg.FontWeight == value;
                lbBg.FontWeight = value;
                lbFr.FontWeight = value;
                if (changed) RePositionText();
            }
        }
        public Color ShadowColor
        {
            get
            {
                return myShadow.Color;
            }
            set
            {
                myShadow.Color = value;
            }
        }
        public double ShadowDepth
        {
            get
            {
                return myShadow.ShadowDepth;
            }
            set
            {
                myShadow.ShadowDepth = value;
            }
        }

        public double ShadowBlurRadius
        {
            get
            {
                return myShadow.BlurRadius;
            }
            set
            {
                myShadow.BlurRadius = value;
            }
        }
        public TextAlignment TextAlign
        {
            get
            {
                return myTextAlign;
            }
            set
            {
                TextAlignment tmp = myTextAlign;
                if (value == TextAlignment.Justify)
                {
                    myTextAlign = TextAlignment.Left;
                }
                else
                {
                    myTextAlign = value;
                }
                if (tmp != myTextAlign)
                {
                    RePositionText();
                }
            }
        }
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsPlayingAnimation { get; set; }
        public void test(){
            //myShadow.Color = Colors.Magenta;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RePositionText();
        }
        protected virtual void OnElapsedTime(EventArgs e)
        {
            // Here, you use the "this" so it's your own control. You can also
            // customize the EventArgs to pass something you'd like.

            if (ElapsedTime != null)
                ElapsedTime(this, e);
        }
    }
}
