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
        int currentLine = -1;
        public ShowLyricController()
        {
            allLyricByLine = new List<LineKaraoke>();
        }
        public void showAtTime(double timeInSecond)
        {
            if (currentLine < allLyricByLine.Count - 2)
            {
                if (timeInSecond > allLyricByLine[currentLine + 1].BeginTime - 2)
                {
                    currentLine += 1;
                    Debug.WriteLine(string.Format("Current line playing is {0}/{1}", currentLine+1, allLyricByLine.Count));
                    if (currentLine % 2 == 0)
                    {
                        lineOne.setTextAndTimes(allLyricByLine[currentLine].LyricLRC);
                    }
                    else
                    {
                        lineTwo.setTextAndTimes(allLyricByLine[currentLine].LyricLRC);
                    }
                }
            }
            try
            {
                if (lineOne.BeginTime.TotalSeconds - 1 <= timeInSecond && lineOne.EndTime.TotalSeconds + 1 >= timeInSecond)
                {
                    lineOne.setCurrentTime(timeInSecond);
                }
                if (lineTwo.BeginTime.TotalSeconds - 1 <= timeInSecond && lineTwo.EndTime.TotalSeconds + 1 >= timeInSecond)
                {
                    lineTwo.setCurrentTime(timeInSecond);
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
        public bool readKaraokeLyric(string input)
        {
            string realString = "";
            string timestr = "";
            TimeSpan beginTime = TimeSpan.FromSeconds(0);
            TimeSpan tmptime = TimeSpan.FromSeconds(0);
            int state = 0; //0:read string, 1: read time
            int lastLinePos = 0;
            if (input[input.Length - 1] != '\n')
            {
                input += "\n";
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\n')
                {
                    //split line
                    realString = input.Substring(lastLinePos, i - lastLinePos).Trim();
                    lastLinePos = i + 1;
                    if (realString != "")
                    {
                        if (i > 0 && input[i - 1] != ']')
                        {
                            tmptime += TimeSpan.FromSeconds(1);
                            realString += string.Format("[{0}]", tmptime.ToString(@"hh\:mm\:ss\.fff"));
                        }
                        LineKaraoke tmp = new LineKaraoke();
                        tmp.BeginTime = beginTime.TotalSeconds;
                        tmp.EndTime = tmptime.TotalSeconds;
                        tmp.LyricLRC = realString;
                        allLyricByLine.Add(tmp);
                        Console.WriteLine(realString);
                        state = 0;
                        beginTime = TimeSpan.FromSeconds(0);
                        tmptime = TimeSpan.FromSeconds(0);
                        realString = "";
                        timestr = "";
                    }
                }
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
                            try
                            {
                                tmptime = TimeSpan.ParseExact(timestr, "c", null); ;
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
                                beginTime = tmptime;
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
            return true;
        }
        public override string ToString()
        {
            string tmp = "";
            for (int i = 0; i < allLyricByLine.Count; i++)
            {
                tmp += allLyricByLine[i].LyricLRC + "\n";

            }
            return tmp;
        }
    }
}
