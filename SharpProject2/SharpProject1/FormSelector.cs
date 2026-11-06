using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace SharpProject1
{
    /// <summary>
    /// Summary for FormSelector
    /// </summary>
    public partial class FormSelector : System.Windows.Forms.Form
    {
        public FormSelector()
        {
            InitializeComponent();
            //
            //TODO: Add the constructor code here
            //
        }
        public FormSelector(string filePath, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            // read candlestick data from the CSV file
            candlestickList = readCandlesticksFromCsv(filePath);
            // filter the candlestick data based on the selected date range
            System.Collections.Generic.List<aCandlestick> filteredData = filterCandlestickData(candlestickList, startDate, endDate);
            //// send the updated list to the DataGridView for display
            //canldestickDataGridView->DataSource = filteredData;
            // Bind the chart to the list of candlesticks (DataSource should be a list or table, not a filename)
            chartStockData.DataSource = new System.ComponentModel.BindingList<aCandlestick>(filteredData);
            // Refresh the chart to reflect the new data
            chartStockData.DataBind();
        }

        // List to hold candlestick data
        private List<aCandlestick> candlestickList = new List<aCandlestick>();
        private List<aCandlestick> filteredCandlestickList = new List<aCandlestick>();
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStockData;

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialogSelectFile.ShowDialog();
        }

        private void openFileDialogSelectFile_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // get the selected file path
            string filePath = openFileDialogSelectFile.FileName;
            // read candlestick data from the CSV file
            candlestickList = readCandlesticksFromCsv(filePath);
            //// display the data in the DataGridView
            //canldestickDataGridView->DataSource = candlestickList;

            // initialize start and end dates from the DateTimePickers
            DateTime startDate = dateTimePickerStartDate.Value;
            DateTime endDate = dateTimePickerEndDate.Value;

            // filter the candlestick data based on the selected date range
            System.Collections.Generic.List<aCandlestick> filteredData = filterCandlestickData(candlestickList, startDate, endDate);
            //// send the updated list to the DataGridView for display
            //canldestickDataGridView->DataSource = filteredData;


            //// Bind the chart to the list of candlesticks (DataSource should be a list or table, not a filename)
            //chartStockData->DataSource = gcnew System::ComponentModel::BindingList<aCandlestick^>(filteredData);
            //// Refresh the chart to reflect the new data
            //chartStockData->DataBind();

            // find the number of files the user selected
            int numFiles = openFileDialogSelectFile.FileNames.Length;

            // for every file the user selected, create a new form and display it
            for (int i = 0; i < numFiles; i++)
            {
                // instantiate a form of type form
                SharpProject1.FormMain f = new SharpProject1.FormMain(openFileDialogSelectFile.FileNames[i], startDate, endDate);
                // modify the title at the top of the form to include the file name
                f.Text = "Candlestick Chart - " + Path.GetFileName(openFileDialogSelectFile.FileNames[i]);
                f.Show();
            }
        }

        /// <summary>
        /// Reads candlestick data from a CSV file, where each line represents a candlestick with the format "date,open,high,low,close,volume".
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public System.Collections.Generic.List<aCandlestick> readCandlesticksFromCsv(string path)
        {
            // Create a new list to hold the candlestick data
            System.Collections.Generic.List<aCandlestick> list = new System.Collections.Generic.List<aCandlestick>();
            // Open the CSV file for reading
            System.IO.StreamReader sr = new System.IO.StreamReader(path);
            // Read each line of the CSV file and parse it into aCandlestick objects
            string line;
            // read header
            line = sr.ReadLine();
            // read data lines while not end of file
            while ((line = sr.ReadLine()) != null)
            {
                string[] separators = new string[] { ",", "\"" };
                // split the line by commas and double quotes
                string[] parts = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                // seperators for both commans and
                // check if we have at least 5 parts (date, open, high, low, close)
                if (parts.Length >= 5)
                {
                    // create a new aCandlestick object and populate its properties
                    aCandlestick c = new aCandlestick();

                    // parse common CSV: date,open,high,low,close[,volume] and round to two decimlam places for open, high, low, close. Volume is optional and defaults to 0 if not present.
                    c.date = DateTime.Parse(parts[0], CultureInfo.InvariantCulture);
                    c.open = Math.Round(Double.Parse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture), 2);
                    c.high = Math.Round(Double.Parse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture), 2);
                    c.low = Math.Round(Double.Parse(parts[3], NumberStyles.Float, CultureInfo.InvariantCulture), 2);
                    c.close = Math.Round(Double.Parse(parts[4], NumberStyles.Float, CultureInfo.InvariantCulture), 2);
                    c.volume = UInt64.Parse(parts[5], CultureInfo.InvariantCulture);

                    // add the candlestick to the list
                    list.Add(c);
                }
            }
            // close the stream reader
            sr.Close();

            // update member list and return
            this.candlestickList = list;
            return list;
        }

        /// <summary>
        /// Filters candlestick data based on a date range input by the user. Returns a new list containing only the candlesticks that fall within the specified date range.
        /// </summary>
        /// <param name="candlestickListParam"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public System.Collections.Generic.List<aCandlestick> filterCandlestickData(System.Collections.Generic.List<aCandlestick> candlestickListParam, DateTime startDate, DateTime endDate)
        {
            // Create a new list to hold the filtered candlestick data
            System.Collections.Generic.List<aCandlestick> result = new System.Collections.Generic.List<aCandlestick>();

            // Check if the input list is not null and has elements, and that the start date is less than or equal to the end date
            if (candlestickListParam != null && candlestickListParam.Count > 0 && startDate <= endDate)
            {
                // Iterate through each candlestick in the input list
                foreach (aCandlestick c in candlestickListParam)
                {
                    // Check if the candlestick's date falls within the specified date range, if it does than add it to the result list
                    if (c.date >= startDate && c.date <= endDate)
                    {
                        result.Add(c);
                    }
                }
            }
            return result;
        }
    }
}
