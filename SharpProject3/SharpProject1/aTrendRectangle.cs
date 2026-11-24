using System;

namespace SharpProject1
{
    // a trend rectangle drawn by the user, it starts on a peak or valley candle and ends wherever the mouse is
    // this class stores candle numbers and prices only (no pixels, no WinForms), and it knows the rules that make a rectangle valid
    class aTrendRectangle
    {
        // the start corner: a candle number, the price on that candle, and whether it is a peak or a valley
        public int startIndex { get; set; }
        public double startPrice { get; set; }
        public extremeKind kind { get; set; }
        // the end corner: a candle number and a price, both follow the mouse
        public int endIndex { get; set; }
        public double endPrice { get; set; }

        /// <summary>
        /// starts the rectangle on a peak or valley candle, the end begins on the same spot so the rectangle starts with no size
        /// </summary>
        /// <param name="startCandle">the peak or valley candle the rectangle starts on</param>
        /// <param name="startKind">Peak or Valley, which of the two the start candle is being used as</param>
        public aTrendRectangle(aSmartCandlestick startCandle, extremeKind startKind)
        {
            kind = startKind;
            startIndex = startCandle.index;
            // a peak starts at the high of its candle, a valley starts at the low of its candle
            if (startKind == extremeKind.Peak)
            {
                startPrice = startCandle.high;
            }
            else
            {
                startPrice = startCandle.low;
            }
            // the end begins on top of the start, so a click without dragging gives a rectangle with no size
            endIndex = startIndex;
            endPrice = startPrice;
        }

        /// <summary>
        /// moves the end corner, called on every mouse move
        /// </summary>
        /// <param name="index">the candle number under the mouse</param>
        /// <param name="price">the price under the mouse</param>
        public void setEnd(int index, double price)
        {
            endIndex = index;
            endPrice = price;
        }

        /// <summary>
        /// returns null when the rectangle is valid, otherwise a sentence saying which rule failed
        /// </summary>
        /// <param name="candles">the candlesticks the chart is showing</param>
        /// <returns>null, or the message of the first rule that failed</returns>
        public string checkRules(System.Collections.Generic.List<aSmartCandlestick> candles)
        {
            // the rules run in order and the first one that fails is the one reported
            string problem = checkSize();
            if (problem == null)
            {
                problem = checkDirection();
            }
            if (problem == null)
            {
                problem = checkBreach(candles);
            }
            return problem;
        }

        /// <summary>
        /// true when checkRules finds nothing wrong
        /// </summary>
        /// <param name="candles">the candlesticks the chart is showing</param>
        /// <returns>true when the rectangle is valid</returns>
        public bool isValid(System.Collections.Generic.List<aSmartCandlestick> candles)
        {
            return checkRules(candles) == null;
        }

        // one method per rule, each returns null when its rule passes
        // rule 1: the rectangle must have a width and a height
        /// <summary>
        /// the size rule: the end corner must be on a different candle and at a different price than the start corner
        /// </summary>
        /// <returns>null when the rule passes, otherwise a message</returns>
        private string checkSize()
        {
            if (endIndex == startIndex || endPrice == startPrice)
            {
                return "Zero size: the end is on the start candle or at the start price.";
            }
            return null;
        }

        // rule 2: the end must move toward the opposite kind, down from a peak and up from a valley
        /// <summary>
        /// the direction rule: a peak start must end lower and a valley start must end higher
        /// </summary>
        /// <returns>null when the rule passes, otherwise a message</returns>
        private string checkDirection()
        {
            if (kind == extremeKind.Peak && endPrice >= startPrice)
            {
                return "Started on a peak, so the end must be below the start price.";
            }
            if (kind == extremeKind.Valley && endPrice <= startPrice)
            {
                return "Started on a valley, so the end must be above the start price.";
            }
            return null;
        }

        // rule 3: no open, high, low or close under the rectangle may stick out of its top (roof) or bottom (floor)
        /// <summary>
        /// the breach rule: no open, high, low or close under the rectangle may lie above its roof or below its floor
        /// </summary>
        /// <param name="candles">the candlesticks the chart is showing</param>
        /// <returns>null when the rule passes, otherwise a message</returns>
        private string checkBreach(System.Collections.Generic.List<aSmartCandlestick> candles)
        {
            // the roof and floor are the higher and lower of the two corner prices, whichever corner each came from
            double roofPrice = Math.Max(startPrice, endPrice);
            double floorPrice = Math.Min(startPrice, endPrice);
            // the candles under the rectangle run from its leftmost corner to its rightmost corner, so dragging left works too
            int first = Math.Min(startIndex, endIndex);
            int last = Math.Max(startIndex, endIndex);

            for (int i = first; i <= last; i++)
            {
                aSmartCandlestick c = candles[i];
                double[] values = new double[] { c.open, c.high, c.low, c.close };
                string[] names = new string[] { "open", "high", "low", "close" };

                for (int v = 0; v < values.Length; v++)
                {
                    if (values[v] > roofPrice)
                    {
                        return "Candle #" + i.ToString() + " " + names[v] + " (" + values[v].ToString("F2") + ") is above the roof (" + roofPrice.ToString("F2") + ").";
                    }
                    if (values[v] < floorPrice)
                    {
                        return "Candle #" + i.ToString() + " " + names[v] + " (" + values[v].ToString("F2") + ") is below the floor (" + floorPrice.ToString("F2") + ").";
                    }
                }
            }
            return null;
        }
    }
}
