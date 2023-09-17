namespace siteMonitor.Net._4._8._1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartResponseTimes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartMonitor = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.chartResponseTimes)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartResponseTimes
            // 
            this.chartResponseTimes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartResponseTimes.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartResponseTimes.Legends.Add(legend1);
            this.chartResponseTimes.Location = new System.Drawing.Point(12, 48);
            this.chartResponseTimes.Name = "chartResponseTimes";
            this.chartResponseTimes.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartResponseTimes.Series.Add(series1);
            this.chartResponseTimes.Size = new System.Drawing.Size(869, 474);
            this.chartResponseTimes.TabIndex = 0;
            this.chartResponseTimes.Text = "chart1";
            // 
            // txtURL
            // 
            this.txtURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtURL.Location = new System.Drawing.Point(96, 12);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(641, 26);
            this.txtURL.TabIndex = 1;
            this.txtURL.Text = "http://www.keithjasper.co.uk";
            this.txtURL.TextChanged += new System.EventHandler(this.txtURL_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Site URL:";
            // 
            // btnStartMonitor
            // 
            this.btnStartMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartMonitor.AutoSize = true;
            this.btnStartMonitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnStartMonitor.Location = new System.Drawing.Point(743, 7);
            this.btnStartMonitor.Name = "btnStartMonitor";
            this.btnStartMonitor.Size = new System.Drawing.Size(138, 35);
            this.btnStartMonitor.TabIndex = 3;
            this.btnStartMonitor.Text = "Start Monitoring";
            this.btnStartMonitor.UseVisualStyleBackColor = true;
            this.btnStartMonitor.Click += new System.EventHandler(this.BtnStartMonitor_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 525);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(893, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusMessage
            // 
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(48, 17);
            this.statusMessage.Text = "Waiting";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 547);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnStartMonitor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.chartResponseTimes);
            this.Name = "MainForm";
            this.Text = "Site Monitor";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartResponseTimes;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartMonitor;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
    }
}

