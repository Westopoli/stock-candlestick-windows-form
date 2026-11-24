using System;

namespace SharpProject1
{
    // add index, peaks, and valleys to the candlestick class, and add functions isPeak() and isValley() to determine if the candlestick is a peak or valley based on its high and low values compared to its immediate neighbors.
    class aSmartCandlestick : aCandlestick
    {
        public int index { get; set; }
        public double peakValue { get; set; }
        public double valleyValue { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public aSmartCandlestick() : base()
        {
            index = 0;
            peakValue = Double.NaN;
            valleyValue = Double.NaN;
        }
        /// <summary>
        /// constructor that takes an existing regular aCandlestick and initializes the aSmartCandlestick with its values
        /// </summary>
        /// <param name="candlestick">the regular candlestick to copy the values from</param>
        public aSmartCandlestick(aCandlestick candlestick) : base()
        {
            // Initialize the base class properties
            this.date = candlestick.date;
            this.open = candlestick.open;
            this.high = candlestick.high;
            this.low = candlestick.low;
            this.close = candlestick.close;
            this.volume = candlestick.volume;
            this.index = 0;
            this.peakValue = Double.NaN;
            this.valleyValue = Double.NaN;
        }

        /// <summary>
        /// isPeak() function to determine if the candlestick is a peak based on its high value compared to its immediate neighbors
        /// </summary>
        /// <param name="prev">the candlestick before this one</param>
        /// <param name="next">the candlestick after this one</param>
        /// <returns>true when this candlestick's high is above the high of both neighbors</returns>
        public bool isPeak(aSmartCandlestick prev, aSmartCandlestick next)
        {
            if (prev == null || next == null)
            {
                return false;
            }
            return (this.high > prev.high && this.high > next.high);
        }

        /// <summary>
        /// isValley() function to determine if the candlestick is a valley based on its low value compared to its immediate neighbors
        /// </summary>
        /// <param name="prev">the candlestick before this one</param>
        /// <param name="next">the candlestick after this one</param>
        /// <returns>true when this candlestick's low is below the low of both neighbors</returns>
        public bool isValley(aSmartCandlestick prev, aSmartCandlestick next)
        {
            if (prev == null || next == null)
            {
                return false;
            }
            return (this.low < prev.low && this.low < next.low);
        }
    }
}
