using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace siteMonitor.Net._4._8._1
{
   public class SiteMonitorData {
        public string url { get; set; }
        public string timeStamp { get; set; }
        public double responseTime { get; set; }


        public SiteMonitorData()
        {
        }

        public string getURL()
        {
            return this.url;
        }

        public string getTimeStamp()
        {
            return this.timeStamp;
        }

        public double getResponseTime()
        {
            return (double)this.responseTime;
        }

        

      
    }
}
