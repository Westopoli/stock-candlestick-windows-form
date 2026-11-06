using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SharpProject1
{
    public class aCandlestick
    {
        public DateTime date { get; set; }
        public Decimal open { get; set; }
        public Decimal high { get; set; }
        public Decimal low { get; set; }
        public Decimal close { get; set; }
        public ulong volume { get; set; }


        /// <summary>
        /// Initializes a new instance of the aCandlestick class with default values for date, 
        /// open, high, low, close, and volume.
        /// </summary>
        public aCandlestick()
        {
            // default values
            this.date = new DateTime(0001-01-01);
            this.open = new Decimal(0.0);
            this.high = new Decimal(0.0);
            this.low = new Decimal(0.0);
            this.close = new Decimal(0.0);
            this.volume = 0;
        }

        /// <summary>
        /// Initializes a new instance of the aCandlestick class by copying the values from another aCandlestick instance.
        /// </summary>
        /// <param name="source"></param>
        public aCandlestick(aCandlestick source)
        {
            this.date = source.date;
            this.open = source.open;
            this.high = source.high;
            this.low = source.low;
            this.close = source.close;
            this.volume = source.volume;
        }

        /// <summary>
        /// Initializes a new instance of the aCandlestick class with specified values for date, 
        /// open, high, low, close, and volume.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="open"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <param name="close"></param>
        /// <param name="volume"></param>
        public aCandlestick(DateTime date, Decimal open, Decimal high, Decimal low, Decimal close, ulong volume)
        {
            this.date = date;
            this.open = open;
            this.high = high;
            this.low = low;
            this.close = close;
            this.volume = volume;
        }

        /// <summary>
        /// Initializes a new instance of the aCandlestick class by parsing a CSV line containing candlestick data.
        /// </summary>
        /// <param name="csvLine"></param>
        public aCandlestick(string csvLine)
        {
            char[] separators = new char[] { ',', '"' };
            string[] values = csvLine.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            this.date = DateTime.ParseExact(values[0], "yyyy-MM-dd", null);
            this.open = Decimal.Parse(values[1]);
            this.high = Decimal.Parse(values[2]);
            this.low = Decimal.Parse(values[3]);
            this.close = Decimal.Parse(values[4]);
            this.volume = ulong.Parse(values[5]);
        }
    }
}
