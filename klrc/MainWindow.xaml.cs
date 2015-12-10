using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        private string nicestring = "abcdefghijklmnopqrstuvwxyzăắằẳẵặâấầẩẫậáàảãạđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZĂẮẰẲẴẶÂẤẦẨẪẬÁÀẢÃẠĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
        private struct SyncString
        {
            public double position;
            public string ministring;
        }
        private List<SyncString> seriesString;
        private bool makingKaraoke;
        private int currentIndex;
        ShowLyricController lyricController;
        public MainWindow()
        {
            InitializeComponent();
            seriesString = new List<SyncString>();

            NAudioEngine soundEngine = NAudioEngine.Instance;
            soundEngine.PropertyChanged += NAudioEngine_PropertyChanged;
            waveformTimeline.RegisterSoundPlayer(soundEngine);
            lyricText.Text = @"Đêm nay em ngồi 
lặng yên nghe anh kể 
chuyện xưa bao năm lắng 
trong tim 
Tình mình từ thuở tuổi đôi mươi 
mà ta chưa biết, 
nên để lỡ duyên đời 
Nghiêng nghiêng đôi nét mực xanh 
trong lưu bút ngày xưa 
em đã viết tặng tôi 
Mộng đời còn có đêm nay, 
ta hò hẹn nhau đây, 
ôn lại chuyện chúng mình. 

Đời anh đã bao năm 
gió sương gót chân in 
chiến trường 
Làm quen với đêm canh 
gió lộng với mưa khuya 
núi rừng 
Đời tôi ngày ngày khi 
chiều chết trên đường phố 
Giọng ca nhịp đàn 
mong về tám hướng tâm tư 
Dù xa nhau em ơi, 
lòng ta luôn nhớ hoài 
vì chờ mong còn dài 
Khuya nay anh đi rồi, 
làm sao em ngăn được, 
thà vui đi cho trót đêm nay 
Nhiều lần mình trắng bàn tay, 
như chuyện xa xưa ấy,
thôi đừng nhắc thêm buồn 
Đôi ta không sống vì nhau 
khi kẻ ở người đi, 
thôi thương tiếc mà chi 
Đường về ngõ tối hai nơi, 
có vài vì sao rơi, 
đêm hò hẹn hết rồi 

Đời anh đã bao năm 
gió sương gót chân in 
chiến trường 
Làm quen với đêm canh 
gió lộng với mưa khuya 
núi rừng 
Đời tôi ngày ngày khi 
chiều chết trên đường phố 
Giọng ca nhịp đàn 
mong về tám hướng tâm tư 
Dù xa nhau em ơi, 
lòng ta luôn nhớ hoài 
vì chờ mong còn dài 
Khuya nay anh đi rồi, 
làm sao em ngăn được, 
thà vui đi cho trót đêm nay 
Nhiều lần mình trắng bàn tay, 
như chuyện xa xưa ấy,
thôi đừng nhắc thêm buồn 
Đôi ta không sống vì nhau 
khi kẻ ở người đi, 
thôi thương tiếc mà chi 
Đường về ngõ tối hai nơi, 
có vài vì sao rơi, 
đêm hò hẹn hết rồi  ";
            lyricLRC.Text = @"[00:00:23.189]Đêm [00:00:23.666]nay [00:00:24.108]em [00:00:24.618]ngồi[00:00:25.618]
[00:00:25.706]lặng [00:00:26.115]yên [00:00:26.557]nghe [00:00:26.999]anh [00:00:27.373]kể[00:00:28.373]
[00:00:28.189]chuyện [00:00:28.802]xưa [00:00:29.312]bao [00:00:29.754]năm [00:00:30.230]lắng[00:00:31.230]
[00:00:31.387]trong [00:00:32.135]tim[00:00:33.135]
[00:00:34.448]Tình [00:00:34.890]mình [00:00:35.298]từ [00:00:35.808]thuở [00:00:37.067]tuổi [00:00:37.373]đôi [00:00:37.815]mươi[00:00:38.815]
[00:00:38.325]mà [00:00:38.632]ta [00:00:39.414]chưa [00:00:40.026]biết,[00:00:41.026]
[00:00:41.149]nên [00:00:41.523]để [00:00:42.169]lỡ [00:00:42.747]duyên [00:00:43.564]đời[00:00:44.564]
[00:00:45.706]Nghiêng [00:00:46.149]nghiêng [00:00:46.625]đôi [00:00:46.965]nét [00:00:47.951]mực [00:00:48.428]xanh[00:00:49.428]
[00:00:48.938]trong [00:00:49.414]lưu [00:00:49.822]bút [00:00:51.013]ngày [00:00:51.387]xưa[00:00:52.387]
[00:00:52.543]em [00:00:52.781]đã [00:00:53.223]viết [00:00:53.666]tặng [00:00:54.244]tôi[00:00:55.244]
[00:00:57.067]Mộng [00:00:57.373]đời [00:00:57.883]còn [00:00:58.292]có [00:00:59.346]đêm [00:00:59.720]nay,[00:01:00.720]
[00:01:00.264]ta [00:01:00.604]hò [00:01:00.945]hẹn [00:01:02.101]nhau [00:01:02.679]đây,[00:01:03.679]
[00:01:03.734]ôn [00:01:04.006]lại [00:01:04.448]chuyện [00:01:05.060]chúng [00:01:05.809]mình.[00:01:06.809]
[00:01:08.189]Đời [00:01:08.326]anh [00:01:08.632]đã [00:01:09.074]bao [00:01:09.550]năm[00:01:10.550]
[00:01:10.264]gió [00:01:10.979]sương [00:01:11.455]gót [00:01:11.897]chân [00:01:12.339]in[00:01:13.339]
[00:01:12.917]chiến [00:01:13.836]trường[00:01:14.836]
[00:01:17.917]Làm [00:01:19.380]quen [00:01:19.822]với [00:01:20.332]đêm [00:01:20.672]canh[00:01:21.672]
[00:01:21.353]gió [00:01:22.135]lộng [00:01:22.543]với [00:01:23.087]mưa [00:01:23.462]khuya[00:01:24.462]
[00:01:24.754]núi [00:01:25.264]rừng[00:01:26.264]
[00:01:27.951]Đời [00:01:31.115]tôi [00:01:31.591]ngày [00:01:31.965]ngày [00:01:32.475]khi[00:01:33.475]
[00:01:32.917]chiều [00:01:33.360]chết [00:01:33.904]trên [00:01:34.312]đường [00:01:34.754]phố[00:01:35.754]
[00:01:36.251]Giọng [00:01:36.693]ca [00:01:37.067]nhịp [00:01:37.577]đàn[00:01:38.577]
[00:01:38.019]mong [00:01:38.564]về [00:01:38.938]tám [00:01:39.414]hướng [00:01:39.958]tâm [00:01:40.400]tư[00:01:41.400]
[00:01:41.693]Dù [00:01:41.999]xa [00:01:42.407]nhau [00:01:42.781]em [00:01:43.122]ơi,[00:01:44.122]
[00:01:44.210]lòng [00:01:44.550]ta [00:01:45.060]luôn [00:01:45.570]nhớ [00:01:46.081]hoài[00:01:47.081]
[00:01:46.625]vì [00:01:46.999]chờ [00:01:47.509]mong [00:01:48.428]còn [00:01:49.074]dài[00:01:50.074]
[00:01:53.088]Khuya [00:01:53.496]nay [00:01:53.836]anh [00:01:54.040]đi [00:01:54.346]rồi,[00:01:55.346]
[00:01:55.571]làm [00:01:55.843]sao [00:01:56.251]em [00:01:56.863]ngăn [00:01:57.237]được,[00:01:58.237]
[00:01:58.360]thà [00:01:58.598]vui [00:01:59.176]đi [00:01:59.550]cho [00:02:00.026]trót [00:02:01.217]đêm [00:02:01.727]nay[00:02:02.727]
[00:02:04.550]Nhiều [00:02:04.822]lần [00:02:05.196]mình [00:02:05.673]trắng [00:02:06.421]bàn [00:02:07.135]tay,[00:02:08.135]
[00:02:07.577]như [00:02:08.020]chuyện [00:02:08.428]xa [00:02:08.870]xưa [00:02:09.958]ấy,[00:02:10.958]
[00:02:11.013]thôi [00:02:11.353]đừng [00:02:11.897]nhắc [00:02:12.339]thêm [00:02:13.224]buồn[00:02:14.224]
[00:02:15.571]Đôi [00:02:16.013]ta [00:02:16.387]không [00:02:16.727]sống [00:02:17.986]vì [00:02:18.292]nhau[00:02:19.292]
[00:02:18.938]khi [00:02:19.380]kẻ [00:02:19.788]ở [00:02:20.877]người [00:02:21.115]đi,[00:02:22.115]
[00:02:22.203]thôi [00:02:22.543]thương [00:02:23.088]tiếc [00:02:23.530]mà [00:02:24.040]chi[00:02:25.040]
[00:02:26.897]Đường [00:02:27.305]về [00:02:27.679]ngõ [00:02:28.156]tối [00:02:29.244]hai [00:02:29.618]nơi,[00:02:30.618]
[00:02:30.605]có [00:02:30.911]vài [00:02:31.387]vì [00:02:31.863]sao [00:02:32.305]rơi,[00:02:33.305]
[00:02:33.530]đêm [00:02:33.768]hò [00:02:34.312]hẹn [00:02:34.856]hết [00:02:35.230]rồi[00:02:36.230]
[00:02:57.611]Đời [00:02:57.815]anh [00:02:58.156]đã [00:02:58.666]bao [00:02:59.074]năm[00:03:00.074]
[00:02:59.856]gió [00:03:00.469]sương [00:03:00.911]gót [00:03:01.285]chân [00:03:01.761]in[00:03:02.761]
[00:03:02.441]chiến [00:03:03.224]trường[00:03:04.224]
[00:03:08.496]Làm [00:03:08.768]quen [00:03:09.380]với [00:03:09.890]đêm [00:03:10.264]canh[00:03:11.264]
[00:03:10.945]gió [00:03:11.625]lộng [00:03:12.067]với [00:03:12.509]mưa [00:03:12.952]khuya[00:03:13.952]
[00:03:14.142]núi [00:03:14.856]rừng[00:03:15.856]
[00:03:20.333]Đời [00:03:20.605]tôi [00:03:21.047]ngày [00:03:21.455]ngày [00:03:21.897]khi[00:03:22.897]
[00:03:22.407]chiều [00:03:22.816]chết [00:03:23.360]trên [00:03:23.700]đường [00:03:24.210]phố[00:03:25.210]
[00:03:25.639]Giọng [00:03:26.115]ca [00:03:26.659]nhịp [00:03:27.101]đàn[00:03:28.101]
[00:03:27.509]mong [00:03:28.054]về [00:03:28.428]tám [00:03:28.972]hướng [00:03:29.482]tâm [00:03:29.958]tư[00:03:30.958]
[00:03:31.217]Dù [00:03:31.455]xa [00:03:31.965]nhau [00:03:32.245]em [00:03:32.748]ơi,[00:03:33.748]
[00:03:33.666]lòng [00:03:34.006]ta [00:03:34.550]luôn [00:03:35.026]nhớ [00:03:35.435]hoài[00:03:36.435]
[00:03:36.761]vì [00:03:36.965]chờ [00:03:37.203]mong [00:03:38.054]còn [00:03:38.666]dài[00:03:39.666]
[00:03:42.884]Khuya [00:03:43.088]nay [00:03:43.292]anh [00:03:43.496]đi [00:03:43.904]rồi,[00:03:44.904]
[00:03:45.060]làm [00:03:45.306]sao [00:03:45.775]em [00:03:46.319]ngăn [00:03:46.693]được,[00:03:47.693]
[00:03:47.850]thà [00:03:48.122]vui [00:03:48.530]đi [00:03:49.074]cho [00:03:49.448]trót [00:03:50.605]đêm [00:03:51.149]nay[00:03:52.149]
[00:03:53.972]Nhiều [00:03:54.210]lần [00:03:54.686]mình [00:03:55.094]trắng [00:03:55.741]bàn [00:03:56.421]tay,[00:03:57.421]
[00:03:57.033]như [00:03:57.509]chuyện [00:03:57.850]xa [00:03:58.326]xưa [00:03:59.312]ấy,[00:04:00.312]
[00:04:00.401]thôi [00:04:00.877]đừng [00:04:01.285]nhắc [00:04:01.863]thêm [00:04:02.714]buồn[00:04:03.714]
[00:04:05.197]Đôi [00:04:05.469]ta [00:04:05.911]không [00:04:06.319]sống [00:04:07.441]vì [00:04:07.782]nhau[00:04:08.782]
[00:04:08.292]khi [00:04:08.768]kẻ [00:04:09.074]ở [00:04:10.299]người [00:04:10.639]đi,[00:04:11.639]
[00:04:11.591]thôi [00:04:11.999]thương [00:04:12.578]tiếc [00:04:12.986]mà [00:04:13.564]chi[00:04:14.564]
[00:04:16.251]Đường [00:04:16.625]về [00:04:17.101]ngõ [00:04:17.475]tối [00:04:18.598]hai [00:04:18.938]nơi,[00:04:19.938]
[00:04:20.061]có [00:04:20.401]vài [00:04:20.809]vì [00:04:21.319]sao [00:04:21.795]rơi,[00:04:22.795]
[00:04:22.952]đêm [00:04:23.224]hò [00:04:23.734]hẹn [00:04:24.278]hết [00:04:24.788]rồi[00:04:25.788]
";
            lyricController = new ShowLyricController();
            lyricController.setLines(ktest, ktest1);
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            ////return;
            ////ktest.ElapsedTime += mytest_ElapsedTime;    
            ktest.clearSeriesTime();
            ktest.setTextAndTimes("[00:00:23.2230000]Đêm [00:00:23.7340000]nay [00:00:24.1080000]em [00:00:24.5840000]ngồi [00:00:25.00000]");

            lyricController.readKaraokeLyric(lyricLRC.Text);

            if (NAudioEngine.Instance.CanPlay)
                NAudioEngine.Instance.Play();
        }

        void mytest_ElapsedTime(object sender, EventArgs e)
        {
            MessageBox.Show("OK");
        }
        private void NAudioEngine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NAudioEngine engine = NAudioEngine.Instance;
            switch (e.PropertyName)
            {
                case "ChannelPosition":
                    clockDisplay.Time = TimeSpan.FromSeconds(engine.ChannelPosition);
                    lyricController.showAtTime(engine.ChannelPosition);
                    //ktest.setCurrentTime(engine.ChannelPosition);
                    //if (!ktest.IsPlayingAnimation) ktest.startAnimate(engine.ChannelPosition);
                    break;
                default:
                    // Do Nothing
                    break;
            }

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            NAudioEngine.Instance.Dispose();
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NAudioEngine.Instance.OpenFile(musicPath.Text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            makingKaraoke = false;
            if (NAudioEngine.Instance.CanStop)
            {
                NAudioEngine.Instance.Stop();
                NAudioEngine.Instance.ChannelPosition = 0;
            }
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (NAudioEngine.Instance.CanPause)
            {
                double tmp = NAudioEngine.Instance.ChannelPosition;
                NAudioEngine.Instance.Stop();
                NAudioEngine.Instance.ChannelPosition = tmp;
            }

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                if (makingKaraoke && currentIndex < seriesString.Count)
                {
                    SyncString tmp = seriesString[currentIndex];
                    tmp.position = NAudioEngine.Instance.ChannelPosition;
                    TimeSpan timetmp = TimeSpan.FromSeconds(tmp.position);
                    int beginselect = lyricLRC.Text.Length;
                    string newstr = string.Format("[{0}]{1}", timetmp.ToString(@"hh\:mm\:ss\.fff"), tmp.ministring);
                    lyricLRC.Text += newstr;
                    lyricLRC.Focus();
                    lyricLRC.Select(beginselect, newstr.Length);
                    currentIndex++;
                }
            }
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            lyricLRC.Text = "";
            SplitLyric();
            currentIndex = 0;
            makingKaraoke = true;
            playButton_Click(sender, e);
        }

        private void SplitLyric()
        {
            seriesString.Clear();
            int begin = 0;
            int state = 0; //0: in invalid, 1: in readable
            string mytext = lyricText.Text;
            int i;
            for (i = 0; i < mytext.Length; i++)
            {
                switch (state)
                {
                    case 0:
                        if (!isNotNiceChar(mytext[i]))
                        {
                            if (i > begin)
                            {
                                //add new word to seriesString
                                SyncString tmp = new SyncString();
                                tmp.ministring = mytext.Substring(begin, i - begin);
                                begin = i;
                                seriesString.Add(tmp);                               
                                state = 1;
                            }
                            state = 1;
                        }
                        break;
                    case 1:
                        if (isNotNiceChar(mytext[i]))
                        {
                            state = 0;
                        }
                        break;
                    default:
                        break;
                }
            }
            if (i > begin)
            {
                //add last word to seriesString
                SyncString tmp = new SyncString();
                tmp.ministring = mytext.Substring(begin, i - begin);
                seriesString.Add(tmp);  
            }
        }
        /// <summary>
        /// check this char is special or not
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool isNotNiceChar(char p)
        {
            return nicestring.IndexOf(p) < 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
