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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mytest.ElapsedTime += mytest_ElapsedTime;    
            mytest.clearSeriesTime();
            mytest.addTimeSync(2, TimeSpan.FromMilliseconds(100));
            mytest.addTimeSync(5, TimeSpan.FromMilliseconds(300));
            mytest.addTimeSync(8, TimeSpan.FromMilliseconds(500));
            mytest.addTimeSync(11, TimeSpan.FromMilliseconds(20));
            mytest.addTimeSync(15, TimeSpan.FromMilliseconds(1000));
            mytest.startAnimate();
        }

        void mytest_ElapsedTime(object sender, EventArgs e)
        {
            MessageBox.Show("OK");
        }
    }
}
