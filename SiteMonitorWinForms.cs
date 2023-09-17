using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace siteMonitor.Net._4._8._1
{ 
    public partial class MainForm : Form
    {

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        private void prevent_screensaver(bool sw)
        {
            if (sw)
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            }
            else
            {
                SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            prevent_screensaver(false);
        }

        /// <summary>
        /// Create background worker for access within class and threads.
        /// </summary>
        private BackgroundWorker worker = new BackgroundWorker();

        /// <summary>
        /// Define workerBusy to allow for interface updating and choosing wether to continue to run the task.
        /// </summary>
        private bool workerBusy = false;

        /// <summary>
        /// Define graph variables. Used to specify text.
        /// </summary>

        //TODO: create muiltiple language options.
        private string GraphName = "ResponseTime";
        private string GraphYAxisTitle = "Time"; // TODO Use this.
        private string GraphXAxisTitle = "Response Time(ms)";
        private string btnLabelStart = "Start Monitoring";
        private string btnLabelStop = "Stop Monitoring";


        private List<SiteMonitorData> ResponseData = new List<SiteMonitorData>();

        /// <summary>
        /// Functions main function
        /// 
        /// Create the window and InitializeGraph.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeGraph();

            prevent_screensaver(true);

            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted; 
        }


        /// <summary>
        /// Set graph features.
        /// </summary>
        public void InitializeGraph()
        {
            chartResponseTimes.Series.Clear();
            chartResponseTimes.Series.Add(GraphName);
            chartResponseTimes.Series[GraphName].ChartType = SeriesChartType.Line;
            chartResponseTimes.Series[GraphName].Points.Clear();

            chartResponseTimes.ChartAreas[0].AxisY.Minimum = 0;
            chartResponseTimes.ChartAreas[0].AxisY.Title = GraphXAxisTitle;


            chartResponseTimes.ChartAreas[0].AxisY.Title = GraphXAxisTitle;

            for (int i = 0; i < 10;  i++)
            {
                chartResponseTimes.Series[GraphName].Points
                    .AddXY(DateTime.Now.ToString("HH:mm:ss"), 0);
            }
        }

        /// <summary>
        /// Validates URL string
        /// </summary>
        /// <param name="url">URL string to be tested</param>
        /// <returns>bool</returns>
        private bool validURL(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Button action to begin procesing and update UI to enable process stopping
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">EventArgs</param>
        private void BtnStartMonitor_Click(object sender, EventArgs e)
        {
            if (!workerBusy)
            {
                List<SiteMonitorData> responseData = new List<SiteMonitorData>();
                string url = txtURL.Text;
                worker.RunWorkerAsync(url);
                workerBusy = true;
                btnStartMonitor.Text = btnLabelStop;
            }
            else
            {
                btnStartMonitor.Text = btnLabelStart;
                workerBusy = false;
                worker.Dispose();
                var numberOfThreads = Process.GetCurrentProcess().Threads.Count;
                Console.WriteLine($"Running {numberOfThreads} threads");
                foreach (var data in ResponseData)
                {
                    Console.WriteLine(data.getURL() + " " + data.getTimeStamp() + " " + data.getResponseTime());
                }

                // TODO: Give Option to export as XML or JSON
                // var xml = new XElement("ReponseTimes", from data in ResponseData select new XElement("row", new XElement("url", data.getURL()), new XElement("Timetamp", data.getTimeStamp()), new XElement("ResponseTime", data.getResponseTime())));
                // Console.WriteLine(xml);

                var json = JsonConvert.SerializeObject(ResponseData, Newtonsoft.Json.Formatting.Indented);

                Console.WriteLine(json);

                prevent_screensaver(false);
            }

        }

        /// <summary>
        /// Async function to get the response time for HttpClient.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">DoWorkEventArgs</param>
        private async void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = e.Argument.ToString();
            while (workerBusy)
            {
                
                using (HttpClient client = new HttpClient())
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        stopwatch.Stop();
                        
                        TimeSpan responseTime = stopwatch.Elapsed;
                        double responseTimeMs = responseTime.TotalMilliseconds;

                        Console.WriteLine("Responding");
                        UpdateUIWithResponseTime(responseTimeMs);
                        await Task.Delay(1000);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        e.Result = 0;
                        await Task.Delay(1000);
                    }
                }
            }
        }

        /// <summary>
        /// Updates UI with new information, this function crosses threads to update UI.
        /// </summary>
        /// <param name="responseTime">double as Milliseconds</param>
        private void UpdateUIWithResponseTime(double responseTime)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() =>
                {
                    AddData(DateTime.Now, responseTime);
                    statusMessage.Text = responseTime.ToString();
                    if (chartResponseTimes.Series[GraphName].Points.Count > 21)
                    {
                        chartResponseTimes.Series[GraphName].Points.RemoveAt(0);
                    }

                }));
            } 
            else
            {
                AddData(DateTime.Now, responseTime);
                statusMessage.Text = responseTime.ToString();
            }
        }


        /// <summary>
        /// AddData data to graph
        /// 
        /// This was moved here to allow for additional processing of data.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="responseTime"></param>
        private void AddData(DateTime dateTime, double responseTime)
        {
            chartResponseTimes.Series[GraphName].Points
                    .AddXY(dateTime.ToString("HH:mm:ss"), responseTime);

            ResponseData.Add(new SiteMonitorData() { url = txtURL.Text, timeStamp = dateTime.ToString(), responseTime = responseTime });
        }


        /// <summary>
        /// Worker has completed, this seems to run on first run of the loop and then does not run again, the task is still running after this has completed.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">RunWorkerCompletedEventArgs</param>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is double responseTime)
            {
                chartResponseTimes.Series[GraphName].Points.AddXY(DateTime.Now ,responseTime);
                statusMessage.Text = responseTime.ToString();
            }
            else if (e.Result is string responseString)
            {
                Console.WriteLine($"{responseString}");
            } 
            worker.Dispose();
        }

        /// <summary>
        /// Check for valid URL when text changes in txtURL.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtURL_TextChanged(object sender, EventArgs e)
        {
            if (validURL(txtURL.Text))
            {
                btnStartMonitor.Enabled = true;
                txtURL.BackColor = Color.GreenYellow;
            }
            else
            {
                txtURL.BackColor = Color.PeachPuff;
                btnStartMonitor.Enabled = false;
            }
        }

    }
}
