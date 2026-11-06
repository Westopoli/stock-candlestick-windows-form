using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SharpProject1
{
    public partial class FormMain : Form
    {
        // List to hold all candlestick data read from the CSV file
        List<aCandlestick> candlestickList = new List<aCandlestick>();
        // List to hold filtered candlestick data based on user-selected date range
        List<aCandlestick> filteredCandlestickList = new List<aCandlestick>();
        public FormMain()
        {
            InitializeComponent();
        }

        // unused button
        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        // Show the OpenFileDialog to allow the user to select a CSV file containing candlestick data
        public void buttonSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialogSelectFile.ShowDialog();
        }

        /// <summary>
        /// This event handler is triggered when the user selects a file using the OpenFileDialog. It reads 
        /// the candlestick data from the selected CSV file, populates the candlestickList, and binds it to 
        /// the DataGridView. It also filters the list based on the user's selected start and end dates from 
        /// the DateTime pickers and updates the DataGridView to display only the filtered candlesticks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void openFileDialogSelectFile_FileOk(object sender, CancelEventArgs e)
        {
            candlestickList = readCandlesticksFromCsv(openFileDialogSelectFile.FileName);
            

            List<aCandlestick> filteredCandlestickList = new List<aCandlestick>();
            filteredCandlestickList = filterCandlestickData(candlestickList, dateTimePickerStart.Value, dateTimePickerEnd.Value);

            // only bind to list based on the user's entered DateTime picker 
            // filteredCandlestickList = candlestickList.Where(c => c.date >= dateTimePickerStart.Value && c.date <= dateTimePickerEnd.Value).ToList();
            // candlestickDataGridView.DataSource = filteredCandlestickList;

            // Bind the grid to the list of candlesticks (DataSource should be a list or table, not a filename)
            candlestickDataGridView.DataSource = filteredCandlestickList;
        }

        /// <summary>
        /// This method reads candlestick data from a CSV file and populates the candlestickList with 
        /// aCandlestick objects. It takes the file path as an argument, reads the CSV file line by line, 
        /// and creates a new aCandlestick object for each line (excluding the header). 
        /// The method returns the populated list of candlesticks.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private List<aCandlestick> readCandlesticksFromCsv(string path)
        {
            // Read the CSV file and populate the candlestickList
            path = openFileDialogSelectFile.FileName;

            // Clear the existing list before reading new data
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                // Read the header line and ignore it
                line = sr.ReadLine();
                // Read the rest of the lines and create aCandlestick objects
                while ((line = sr.ReadLine()) != null)
                {
                    // Create a new aCandlestick object from the CSV line
                    aCandlestick c = new aCandlestick(line);
                    // Add the new candlestick to the list
                    candlestickList.Add(c);
                }
            }
            return candlestickList;
        }

        // filter the candlestick data in it's own function instead of inside the openFileDialogSelectFile_FileOk function
        public List<aCandlestick> filterCandlestickData(List<aCandlestick> candlestickList, DateTime startDate, DateTime endDate)
        {
            List<aCandlestick> filteredCandlestickList = new List<aCandlestick>();
            // only true after the list of candlestick has a count greater than 1. 
            // double check null first, as instructed
            // also check that the start date is after the end date
            if (candlestickList != null && candlestickList.Count > 1 && startDate <= endDate)
            {
                // rebind to the updated list based on the user's entered DateTime picker
                filteredCandlestickList = candlestickList.Where(c => c.date >= startDate && c.date <= endDate).ToList();
                return filteredCandlestickList;
            }
            return new List<aCandlestick>();
        }

        // unused 
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        // also unused
        private void candlestickDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// This event handler is triggered when the redisplay button is clicked. It filters the candlestick 
        /// list based on the user's selected start and end dates from the DateTime pickers and updates the 
        /// DataGridView to display only the filtered candlesticks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void redisplayButton_Click(object sender, EventArgs e)
        {
            filteredCandlestickList = filterCandlestickData(candlestickList, dateTimePickerStart.Value, dateTimePickerEnd.Value);

            // only true after the list of candlestick has a count greater than 1. 
            // double check null first, as instructed
            // also check that the start date is after the end date
            if (candlestickList != null && candlestickList.Count > 1 && dateTimePickerStart.Value <= dateTimePickerEnd.Value)
            {
                // rebind to the updated list based on the user's entered DateTime picker
                filteredCandlestickList = candlestickList.Where(c => c.date >= dateTimePickerStart.Value && c.date <= dateTimePickerEnd.Value).ToList();
                candlestickDataGridView.DataSource = filteredCandlestickList;
            }
        }
    }
}
