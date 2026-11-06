namespace SharpProject1
{
    partial class FormMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.openFileDialogSelectFile = new System.Windows.Forms.OpenFileDialog();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.canldestickDataGridView = new System.Windows.Forms.DataGridView();
            this.redisplayButton = new System.Windows.Forms.Button();
            this.chartStockData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.canldestickDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStockData)).BeginInit();
            this.SuspendLayout();
            //
            // openFileDialogSelectFile
            //
            this.openFileDialogSelectFile.DefaultExt = "csv";
            this.openFileDialogSelectFile.FileName = "NVDA_Daily.csv";
            this.openFileDialogSelectFile.Filter = "All files (*.*)|*.*| Daily | *_daily.csv*| Weekly  | *_weekly.csv| Montly | *_mon" +
    "thly.csv";
            this.openFileDialogSelectFile.InitialDirectory = "..\\..\\..\\..\\Stock Data";
            this.openFileDialogSelectFile.Multiselect = true;
            this.openFileDialogSelectFile.RestoreDirectory = true;
            this.openFileDialogSelectFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            //
            // dateTimePickerStartDate
            //
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(534, 501);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerStartDate.TabIndex = 1;
            this.dateTimePickerStartDate.Value = new System.DateTime(2026, 6, 1, 0, 0, 0, 0);
            //
            // dateTimePickerEndDate
            //
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(534, 615);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerEndDate.TabIndex = 2;
            this.dateTimePickerEndDate.Value = new System.DateTime(2026, 9, 18, 0, 0, 0, 0);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(607, 475);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start Date";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(607, 591);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "End Date";
            //
            // canldestickDataGridView
            //
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.canldestickDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.canldestickDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.canldestickDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.canldestickDataGridView.Location = new System.Drawing.Point(12, 458);
            this.canldestickDataGridView.Name = "canldestickDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.canldestickDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.canldestickDataGridView.Size = new System.Drawing.Size(502, 207);
            this.canldestickDataGridView.TabIndex = 5;
            //
            // redisplayButton
            //
            this.redisplayButton.Location = new System.Drawing.Point(920, 560);
            this.redisplayButton.Name = "redisplayButton";
            this.redisplayButton.Size = new System.Drawing.Size(150, 75);
            this.redisplayButton.TabIndex = 6;
            this.redisplayButton.Text = "Redisplay";
            this.redisplayButton.UseVisualStyleBackColor = true;
            this.redisplayButton.Click += new System.EventHandler(this.button1_Click);
            //
            // chartStockData
            //
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.Name = "ChartStockData";
            chartArea2.AlignWithChartArea = "ChartStockData";
            chartArea2.Name = "ChartVolume";
            this.chartStockData.ChartAreas.Add(chartArea1);
            this.chartStockData.ChartAreas.Add(chartArea2);
            this.chartStockData.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Name = "Legend1";
            this.chartStockData.Legends.Add(legend1);
            this.chartStockData.Location = new System.Drawing.Point(0, 0);
            this.chartStockData.Name = "chartStockData";
            series1.BorderColor = System.Drawing.Color.Transparent;
            series1.ChartArea = "ChartStockData";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Color = System.Drawing.Color.Black;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=Lime";
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "OHLCV";
            series1.XValueMember = "Date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueMembers = "high,low,open,close";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartVolume";
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend1";
            series2.Name = "Volume";
            series2.XValueMember = "date";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValueMembers = "volume";
            series2.YValuesPerPoint = 2;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt64;
            series3.ChartArea = "ChartStockData";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series3.IsXValueIndexed = true;
            series3.Legend = "Legend1";
            series3.MarkerBorderWidth = 8;
            series3.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series3.Name = "Dots";
            series4.ChartArea = "ChartStockData";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series4.IsXValueIndexed = true;
            series4.Legend = "Legend1";
            series4.MarkerBorderWidth = 8;
            series4.MarkerColor = System.Drawing.Color.DeepSkyBlue;
            series4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series4.Name = "Valleys";
            this.chartStockData.Series.Add(series1);
            this.chartStockData.Series.Add(series2);
            this.chartStockData.Series.Add(series3);
            this.chartStockData.Series.Add(series4);
            this.chartStockData.Size = new System.Drawing.Size(1284, 452);
            this.chartStockData.TabIndex = 7;
            this.chartStockData.Text = "chart1";
            //
            // FormMain
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 677);
            this.Controls.Add(this.chartStockData);
            this.Controls.Add(this.redisplayButton);
            this.Controls.Add(this.canldestickDataGridView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerEndDate);
            this.Controls.Add(this.dateTimePickerStartDate);
            this.Name = "FormMain";
            this.Text = "FormMain";
            ((System.ComponentModel.ISupportInitialize)(this.canldestickDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartStockData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogSelectFile;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView canldestickDataGridView;
        private System.Windows.Forms.Button redisplayButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStockData;
    }
}
