using System;
using System.Globalization;

namespace SharpProject1
{
    public class aCandlestick
    {
        // variable definitions

        public DateTime date { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public ulong volume { get; set; }

        /// <summary>
        /// default constructor, add initialize components to the constructor
        /// </summary>
        public aCandlestick()
        {
            date = DateTime.MinValue;
            open = 0.0;
            high = 0.0;
            low = 0.0;
            close = 0.0;
            volume = 0;
        }

        /// <summary>
        /// additional constructors
        /// </summary>
        /// <param name="dateVal">the date of the candlestick</param>
        /// <param name="openVal">the opening price</param>
        /// <param name="highVal">the highest price</param>
        /// <param name="lowVal">the lowest price</param>
        /// <param name="closeVal">the closing price</param>
        /// <param name="volumeVal">the volume traded</param>
        public aCandlestick(DateTime dateVal, double openVal, double highVal, double lowVal, double closeVal, ulong volumeVal)
        {
            date = dateVal;
            open = openVal;
            high = highVal;
            low = lowVal;
            close = closeVal;
            volume = volumeVal;
        }

        /// <summary>
        /// constructor from another candlestick (copy)
        /// </summary>
        /// <param name="source">the candlestick to copy</param>
        public aCandlestick(aCandlestick source)
        {
            if (source == null)
            {
                date = DateTime.MinValue;
                open = 0.0;
                high = 0.0;
                low = 0.0;
                close = 0.0;
                volume = 0;
            }
            else
            {
                date = source.date;
                open = source.open;
                high = source.high;
                low = source.low;
                close = source.close;
                volume = source.volume;
            }
        }

        /// <summary>
        /// constructor from a CSV line
        /// </summary>
        /// <param name="csvLine">one line of a CSV file</param>
        public aCandlestick(string csvLine)
        {
            char[] separator = new char[] { ',', '"' };
            string[] parts = csvLine.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length >= 6)
            {
                // Expecting format: "yyyy-MM-dd,open,high,low,close,volume"
                date = DateTime.ParseExact(parts[0], "yyyy-MM-dd", null);
                open = Double.Parse(parts[1], CultureInfo.InvariantCulture);
                high = Double.Parse(parts[2], CultureInfo.InvariantCulture);
                low = Double.Parse(parts[3], CultureInfo.InvariantCulture);
                close = Double.Parse(parts[4], CultureInfo.InvariantCulture);
                volume = UInt64.Parse(parts[5], CultureInfo.InvariantCulture);
            }
            else
            {
                // Fallback to defaults if parsing fails
                date = DateTime.MinValue;
                open = 0.0;
                high = 0.0;
                low = 0.0;
                close = 0.0;
                volume = 0;
            }
        }
    }
}
