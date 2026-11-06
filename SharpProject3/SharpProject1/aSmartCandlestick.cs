using System;

namespace SharpProject1
{
    // add index, peaks, and valleys to the candlestick class, and add functions isPeak() and isValley() to determine if the candlestick is a peak or valley based on its high and low values compared to its immediate neighbors.
    class aSmartCandlestick : aCandlestick
    {
        public int index { get; set; }
        public double peakValue { get; set; }
        public double valleyValue { get; set; }

        // default constructor
        public aSmartCandlestick() : base()
        {
            index = 0;
            peakValue = Double.NaN;
            valleyValue = Double.NaN;
        }
        // constructor that takes an existing regular aCandlestick and initializes the aSmartCandlestick with its values
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

        // isPeak() function to determine if the candlestick is a peak based on its high value compared to its immediate neighbors
        public bool isPeak(aSmartCandlestick prev, aSmartCandlestick next)
        {
            if (prev == null || next == null)
            {
                return false;
            }
            return (this.high > prev.high && this.high > next.high);
        }

        // isValley() function to determine if the candlestick is a valley based on its low value compared to its immediate neighbors
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
