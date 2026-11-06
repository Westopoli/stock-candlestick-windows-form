using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SharpProject1
{
    /// <summary>
    /// Summary for FormMain
    /// </summary>
    public partial class FormMain : System.Windows.Forms.Form
    {
        public FormMain()
        {
            InitializeComponent();
            //
            //TODO: Add the constructor code here
            //
        }
        // new constructor, pass the individual file path, the start date, and the end date to the new form
        public FormMain(string filePath, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            // read candlestick data from the CSV file
            candlestickList = candlestickUtilities.readCandlesticksFromCsv(filePath);

            // filter the candlestick data based on the selected date range
            System.Collections.Generic.List<aCandlestick> filteredData = candlestickUtilities.filterCandlestickData(candlestickList, startDate, endDate);
            // convert the old filtered data to the new smart candlestick
            System.Collections.Generic.List<aSmartCandlestick> smartFilteredData = candlestickUtilities.buildSmartCandlesticks(filteredData);
            // send the updated list to the DataGridView for display
            canldestickDataGridView.DataSource = smartFilteredData;

            // Set the X & Y values of the new dot series to show peaks and valleys.
            chartStockData.Series["Dots"].XValueMember = "date";
            chartStockData.Series["Dots"].YValueMembers = "peakValue";
            // Set the X & Y values of the valley series.
            chartStockData.Series["Valleys"].XValueMember = "date";
            chartStockData.Series["Valleys"].YValueMembers = "valleyValue";

            // Bind the chart to the list of candlesticks (DataSource should be a list or table, not a filename)
            chartStockData.DataSource = new System.ComponentModel.BindingList<aSmartCandlestick>(smartFilteredData);
            chartStockData.DataBind();
        }

        // Lists to hold candlestick data
        private List<aCandlestick> candlestickList = new List<aCandlestick>();
        private List<aCandlestick> filteredCandlestickList = new List<aCandlestick>();

        // read candlestick data from a CSV file and display to DataGridView
        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // get the selected file path
            string filePath = openFileDialogSelectFile.FileName;
            // read candlestick data from the CSV file
            candlestickList = candlestickUtilities.readCandlesticksFromCsv(filePath);
            // display the data in the DataGridView
            canldestickDataGridView.DataSource = candlestickList;

            // initialize start and end dates from the DateTimePickers
            DateTime startDate = dateTimePickerStartDate.Value;
            DateTime endDate = dateTimePickerEndDate.Value;

            // filter the candlestick data based on the selected date range
            System.Collections.Generic.List<aCandlestick> filteredData = candlestickUtilities.filterCandlestickData(candlestickList, startDate, endDate);
            // convert the old filtered data to the new smart candlestick
            System.Collections.Generic.List<aSmartCandlestick> smartFilteredData = candlestickUtilities.buildSmartCandlesticks(filteredData);
            // send the updated list to the DataGridView for display
            canldestickDataGridView.DataSource = smartFilteredData;

            // Set the X & Y values of the new dot series to show peaks and valleys.
            chartStockData.Series["Dots"].XValueMember = "date";
            chartStockData.Series["Dots"].YValueMembers = "peakValue";

            // Bind the chart to the list of candlesticks (DataSource should be a list or table, not a filename)
            chartStockData.DataSource = new System.ComponentModel.BindingList<aSmartCandlestick>(smartFilteredData);
            chartStockData.DataBind();

            // find the number of files the user selected
            int numFiles = openFileDialogSelectFile.FileNames.Length;

            // for every file the user selected, create a new form and display it
            for (int i = 0; i < numFiles; i++)
            {
                // instantiate a form of type form
                FormMain f = new FormMain();
                // construct the form with the file path and the start and end dates
                f = new FormMain(openFileDialogSelectFile.FileNames[i], startDate, endDate);
                // modify the title at the top of the form to include the file name
                f.Text = "Candlestick Chart - " + Path.GetFileName(openFileDialogSelectFile.FileNames[i]);
                f.Show();
            }
        }

        // mistaken event handler for label click, not used in this application
        private void label1_Click(object sender, EventArgs e)
        {

        }

        // filter the candlestick data based on the selected start and end dates and redisplay
        private void button1_Click(object sender, EventArgs e)
        {
            // initialize start and end dates from the DateTimePickers
            DateTime startDate = dateTimePickerStartDate.Value;
            DateTime endDate = dateTimePickerEndDate.Value;

            // filter the candlestick data based on the selected date range
            System.Collections.Generic.List<aCandlestick> filteredData = candlestickUtilities.filterCandlestickData(candlestickList, startDate, endDate);
            // convert the filtered data to smart candlesticks so the peaks and valleys are recalculated for the new range
            System.Collections.Generic.List<aSmartCandlestick> smartFilteredData = candlestickUtilities.buildSmartCandlesticks(filteredData);
            // send the updated list to the DataGridView for display
            canldestickDataGridView.DataSource = smartFilteredData;

            // Set the X & Y values of the dot series to show peaks and valleys.
            chartStockData.Series["Dots"].XValueMember = "date";
            chartStockData.Series["Dots"].YValueMembers = "peakValue";
            // Set the X & Y values of the valley series.
            chartStockData.Series["Valleys"].XValueMember = "date";
            chartStockData.Series["Valleys"].YValueMembers = "valleyValue";

            // Bind the chart to the list of smart candlesticks (DataSource should be a list or table, not a filename)
            chartStockData.DataSource = new System.ComponentModel.BindingList<aSmartCandlestick>(smartFilteredData);
            // Refresh the chart to reflect the new data
            chartStockData.DataBind();
        }

        // function definitions in cpp file
    }
}
