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

namespace klrc
{
    /// <summary>
    /// Interaction logic for karalabel.xaml
    /// </summary>
    public partial class karalabel : UserControl
    {
        public karalabel()
        {
            InitializeComponent();
            this.FrontColor = new SolidColorBrush(Colors.Red);
            this.BackColor = new SolidColorBrush(Colors.Black);
            lbOverlay.Width = lbBg.Width / 2;
        }
        public string Text { 
            get{
            return lbBg.Text;
        }
            set
            {
                lbBg.Text = value;
                lbOverlay.Text = value;
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
                return lbOverlay.Foreground;
            }
            set
            {
                lbOverlay.Foreground = value;
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
                lbOverlay.FontFamily = value;
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
                lbOverlay.FontSize = value;
            }
        }
        
        public void test(){
            //lbOverlay.FontSize

            lbOverlay.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            lbOverlay.Arrange(new Rect(lbOverlay.DesiredSize));
            double x = lbOverlay.ActualWidth;
        }
    }
}
