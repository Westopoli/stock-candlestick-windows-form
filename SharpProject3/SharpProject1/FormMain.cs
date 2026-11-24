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
        /// <summary>
        /// default constructor, only builds the controls
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            //
            //TODO: Add the constructor code here
            //
        }
        /// <summary>
        /// new constructor, pass the individual file path, the start date, and the end date to the new form
        /// </summary>
        /// <param name="filePath">the CSV file to read the candlesticks from</param>
        /// <param name="startDate">the first date to show</param>
        /// <param name="endDate">the last date to show</param>
        public FormMain(string filePath, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            // read candlestick data from the CSV file
            candlestickList = candlestickUtilities.readCandlesticksFromCsv(filePath);

            // filter the candlesticks to the date range, find the peaks and valleys, and show them in the grid and the chart
            displayCandlesticks(startDate, endDate);
        }

        // Lists to hold candlestick data
        private List<aCandlestick> candlestickList = new List<aCandlestick>();
        private List<aCandlestick> filteredCandlestickList = new List<aCandlestick>();
        private System.Collections.Generic.List<aSmartCandlestick> smartFilteredData;

        // the rectangle the user is drawing or has drawn (null when there is none), and the object that draws it
        private aTrendRectangle trendRectangle = null;
        private trendRectangleRenderer renderer = new trendRectangleRenderer();

        private bool mouseDown = false;

        /// <summary>
        /// filters the loaded candlesticks to the date range, finds the peaks and valleys, and shows the result in the grid and the chart
        /// </summary>
        /// <param name="startDate">the first date to show</param>
        /// <param name="endDate">the last date to show</param>
        private void displayCandlesticks(DateTime startDate, DateTime endDate)
        {
            // filter the candlestick data based on the selected date range
            System.Collections.Generic.List<aCandlestick> filteredData = candlestickUtilities.filterCandlestickData(candlestickList, startDate, endDate);
            // convert the filtered data to smart candlesticks so the peaks and valleys are recalculated for the new range
            smartFilteredData = candlestickUtilities.buildSmartCandlesticks(filteredData);
            // the candle numbers inside an old rectangle belong to the old list, so the old rectangle is thrown away
            trendRectangle = null;
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

        /// <summary>
        /// read candlestick data from a CSV file and display to DataGridView
        /// </summary>
        /// <param name="sender">the open file dialog</param>
        /// <param name="e">lets the handler cancel the dialog</param>
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

            // filter the candlesticks to the date range, find the peaks and valleys, and show them in the grid and the chart
            displayCandlesticks(startDate, endDate);

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

        /// <summary>
        /// mistaken event handler for label click, not used in this application
        /// </summary>
        /// <param name="sender">the label</param>
        /// <param name="e">the click event data</param>
        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// filter the candlestick data based on the selected start and end dates and redisplay
        /// </summary>
        /// <param name="sender">the Redisplay button</param>
        /// <param name="e">the click event data</param>
        private void button1_Click(object sender, EventArgs e)
        {
            // initialize start and end dates from the DateTimePickers
            DateTime startDate = dateTimePickerStartDate.Value;
            DateTime endDate = dateTimePickerEndDate.Value;

            // filter the candlesticks to the date range, find the peaks and valleys, and show them in the grid and the chart
            displayCandlesticks(startDate, endDate);
        }

        // function definitions in cpp file
        /// <summary>
        /// click on the chart, not used in this application (the rectangle uses the mouse down, move and up events)
        /// </summary>
        /// <param name="sender">the chart</param>
        /// <param name="e">the click event data</param>
        private void chartStockData_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// turns a mouse X position into a candle position counting from 0 (the chart counts candles from 1, so 1 is subtracted)
        /// </summary>
        /// <param name="mouseX">the mouse X position inside the chart</param>
        /// <returns>the candle position, which is not rounded</returns>
        private double candlePositionAt(int mouseX)
        {
            // Get the primary ChartArea
            System.Windows.Forms.DataVisualization.Charting.ChartArea area = chartStockData.ChartAreas[0];
            // the chart refuses a position outside of itself and the mouse can leave the chart while dragging, so the mouse X is kept inside the chart
            mouseX = Math.Max(0, Math.Min(mouseX, chartStockData.Width - 1));
            // Convert the mouse click position to chart values
            return area.AxisX.PixelPositionToValue(mouseX) - 1;
        }

        /// <summary>
        /// turns a mouse Y position into a price
        /// </summary>
        /// <param name="mouseY">the mouse Y position inside the chart</param>
        /// <returns>the price at that height</returns>
        private double priceAt(int mouseY)
        {
            // Get the primary ChartArea
            System.Windows.Forms.DataVisualization.Charting.ChartArea area = chartStockData.ChartAreas[0];
            // the chart refuses a position outside of itself and the mouse can leave the chart while dragging, so the mouse Y is kept inside the chart
            mouseY = Math.Max(0, Math.Min(mouseY, chartStockData.Height - 1));
            // Convert the mouse click position to chart values
            return area.AxisY.PixelPositionToValue(mouseY);
        }

        /// <summary>
        /// pressing the mouse starts a new rectangle on the peak or valley nearest the mouse
        /// </summary>
        /// <param name="sender">the chart</param>
        /// <param name="e">the mouse position</param>
        private void chartStockData_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // without candlesticks there is nothing to start on
            if (smartFilteredData == null)
            {
                return;
            }

            // snap the start corner to the peak or valley nearest the mouse, kind is filled in with Peak or Valley
            extremeKind kind = extremeKind.Peak;
            aSmartCandlestick startCandle = candlestickUtilities.findNearestExtreme(smartFilteredData, candlePositionAt(e.X), priceAt(e.Y), ref kind);
            // a list with no peaks or valleys has nothing to snap to
            if (startCandle == null)
            {
                return;
            }

            trendRectangle = new aTrendRectangle(startCandle, kind);
            mouseDown = true;
            chartStockData.Invalidate();
        }

        /// <summary>
        /// releasing the mouse leaves the rectangle on screen, and it is drawn solid instead of dashed
        /// </summary>
        /// <param name="sender">the chart</param>
        /// <param name="e">the mouse position</param>
        private void chartStockData_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouseDown = false;
            chartStockData.Invalidate();
        }

        /// <summary>
        /// while the mouse is down, the end corner of the rectangle follows the mouse
        /// </summary>
        /// <param name="sender">the chart</param>
        /// <param name="e">the mouse position</param>
        private void chartStockData_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (mouseDown)
            {
                // the candle number must stay inside the list, because the mouse can leave the chart while dragging
                int index = (int)Math.Round(candlePositionAt(e.X));
                if (index < 0)
                {
                    index = 0;
                }
                if (index > smartFilteredData.Count - 1)
                {
                    index = smartFilteredData.Count - 1;
                }
                trendRectangle.setEnd(index, priceAt(e.Y));
                chartStockData.Invalidate();
            }
        }

        // while dragging, if the trend is valid, make the rectangle green (if it's an uptrend), and red if it's a down trend.
        // while dragging, the rectangle should be dashed, as soon as mouseUp occurs, the rectangle should be solid.
        // the renderer draws the rectangle during the chart's paint event, and an invalid rectangle is drawn black
        /// <summary>
        /// draws the rectangle with its ratio lines and dots, and shows the number of touches
        /// </summary>
        /// <param name="sender">the chart</param>
        /// <param name="e">the graphics to draw on</param>
        private void FormMain_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // the number of touches is 0 when there is no rectangle, or when the rectangle is invalid
            int touches = 0;
            // draw only when there is a rectangle, draw returns the number of touches
            if (trendRectangle != null)
            {
                touches = renderer.draw(e.Graphics, chartStockData, trendRectangle, smartFilteredData, mouseDown);
            }
            // show the number of touches in the textbox under the chart
            textBoxTouches.Text = touches.ToString();
        }
    }
}
