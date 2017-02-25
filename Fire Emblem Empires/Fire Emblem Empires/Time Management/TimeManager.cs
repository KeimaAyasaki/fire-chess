using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fire_Emblem_Empires.Unit_Management
{
    class TimeManager
    {
        static Stopwatch stopWatch = new Stopwatch();
        static public bool isOn { get; private set; }

        static public void Start()
        {
            stopWatch.Start();
            isOn = true;
        }

        static public void Stop()
        {
            stopWatch.Stop();
            isOn = false;
        }

        static public String TimeElapsedSinceStart()
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            return elapsedTime;
        }

        static public bool TimeElapsed(string earlierTime, string laterTime, out string timeDifference)
        {
            // Regex for timespans
            string timeRegex = "^([0-9]+):([0-9]+):([0-9]+)\\.([0-9]+)$";

            // Extracting information from the time strings
            int earlierTimeHours, earlierTimeMinutes, earlierTimeSeconds, earlierTimeMilliseconds;
            int laterTimeHours, laterTimeMinutes, laterTimeSeconds, laterTimeMilliseconds;

            if (!TimeExtractor(timeRegex, earlierTime, out earlierTimeHours, out earlierTimeMinutes, out earlierTimeSeconds, out earlierTimeMilliseconds))
            {
                timeDifference = "-error-";
                return false;
            }
            if (!TimeExtractor(timeRegex, laterTime, out laterTimeHours, out laterTimeMinutes, out laterTimeSeconds, out laterTimeMilliseconds))
            {
                timeDifference = "-error-";
                return false;
            }

            // Adding the information from the time strings
            int timeSumHours = laterTimeHours - earlierTimeHours;
            int timeSumMinutes = laterTimeMinutes - earlierTimeMinutes;
            int timeSumSeconds = laterTimeSeconds - earlierTimeSeconds;
            int timeSumMilliseconds = laterTimeMilliseconds - earlierTimeMilliseconds;

            if (timeSumMilliseconds > 999)
            {
                timeSumMilliseconds %= 1000;
                ++timeSumSeconds;
            }

            if (timeSumSeconds > 59)
            {
                timeSumSeconds %= 60;
                ++timeSumMinutes;
            }

            if (timeSumMinutes > 59)
            {
                timeSumMinutes %= 60;
                ++timeSumHours;
            }

            timeDifference = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", timeSumHours, timeSumMinutes, timeSumSeconds, timeSumMilliseconds);
            return true;
        }

        // earlierTime = Earliest time
        // laterTime = Latest time
        static public bool CombineTimes(string earlierTime, string laterTime, out string timeSum)
        {
            // Regex for timespans
            string timeRegex = "^([0-9]+):([0-9]+):([0-9]+)\\.([0-9]+)$";

            // Extracting information from the time strings
            int earlierTimeHours, earlierTimeMinutes, earlierTimeSeconds, earlierTimeMilliseconds;
            int laterTimeHours, laterTimeMinutes, laterTimeSeconds, laterTimeMilliseconds;

            if(!TimeExtractor(timeRegex, earlierTime, out earlierTimeHours, out earlierTimeMinutes, out earlierTimeSeconds, out earlierTimeMilliseconds))
            {
                timeSum = "-error-";
                return false;
            }
            if(!TimeExtractor(timeRegex, laterTime, out laterTimeHours, out laterTimeMinutes, out laterTimeSeconds, out laterTimeMilliseconds))
            {
                timeSum = "-error-";
                return false;
            }

            // Adding the information from the time strings
            int timeSumHours = earlierTimeHours + laterTimeHours;
            int timeSumMinutes = earlierTimeMinutes + laterTimeMinutes;
            int timeSumSeconds = earlierTimeSeconds + laterTimeSeconds;
            int timeSumMilliseconds = earlierTimeMilliseconds + laterTimeMilliseconds;

            if(timeSumMilliseconds > 999)
            {
                timeSumMilliseconds %= 1000;
                ++timeSumSeconds;
            }
            
            if(timeSumSeconds > 59)
            {
                timeSumSeconds %= 60;
                ++timeSumMinutes;
            }

            if(timeSumMinutes > 59)
            {
                timeSumMinutes %= 60;
                ++timeSumHours;
            }

            timeSum = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", timeSumHours, timeSumMinutes, timeSumSeconds, timeSumMilliseconds);
            return true;            
        }

        static bool TimeExtractor(string timeRegex, string time, out int hours, out int minutes, out int seconds, out int milliseconds)
        {
            Match match = Regex.Match(time, timeRegex);

            if(!int.TryParse(match.Groups[1].ToString(), out hours))
            {
                Console.WriteLine("{0} was lacking a value for hours.", time);
                minutes = -1;
                seconds = -1;
                milliseconds = -1;
                return false;
            }
            
            if(!int.TryParse(match.Groups[2].ToString(), out minutes))
            {
                Console.WriteLine("{0} was lacking a value for minutes.", time);
                seconds = -1;
                milliseconds = -1;
                return false;
            }

            if(!int.TryParse(match.Groups[3].ToString(), out seconds))
            {
                Console.WriteLine("{0} was lacking a value for seconds.", time);
                milliseconds = -1;
                return false;
            }

            if(!int.TryParse(match.Groups[4].ToString(), out milliseconds))
            {
                Console.WriteLine("{0} was lacking a value for milliseconds.", time);
                return false;
            }

            return true;
        }

    }
}
