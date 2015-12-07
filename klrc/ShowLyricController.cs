using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace klrc
{
    class ShowLyricController
    {
        private struct LineKaraoke
        {
            public double BeginTime;
            public double EndTime;
            public string LyricLRC;
        }
        private List<LineKaraoke> allLyricByLine;
        private karalabel lineOne;
        private karalabel lineTwo;
        public void showAtTime(double timeInSecond)
        {
            try
            {
                if (lineOne.BeginTime.TotalSeconds <= timeInSecond && lineOne.EndTime.TotalSeconds >= timeInSecond)
                {
                    lineOne.setCurrentTime(timeInSecond);
                    return;
                }
                if (lineTwo.BeginTime.TotalSeconds <= timeInSecond && lineTwo.EndTime.TotalSeconds >= timeInSecond)
                {
                    lineTwo.setCurrentTime(timeInSecond);
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("showAtTime error: " + ex.Message);
            }
        }

        public void setLineOne(karalabel line)
        {
            lineOne = line;
        }
        public void setLineTwo(karalabel line)
        {
            lineTwo = line;
        }
        public void setLines(karalabel line1, karalabel line2)
        {
            lineOne = line1;
            lineTwo = line2;
        }
    }
}
