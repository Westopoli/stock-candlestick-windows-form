using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace SharpProject1
{
    static class candlestickUtilities
    {
        // This class will contain static utility functions for working with candlestick data, specifically converting between aCandlestick and aSmartCandlestick objects, as well as any other common operations needed for candlestick analysis.

        /// <summary>
        /// reads candlestick data from a CSV file, where each line is "date,open,high,low,close,volume"
        /// </summary>
        /// <param name="path">the full path of the CSV file</param>
        /// <returns>one candlestick per line of the file</returns>
        public static System.Collections.Generic.List<aCandlestick> readCandlesticksFromCsv(string path)
        {
            System.Collections.Generic.List<aCandlestick> list = new System.Collections.Generic.List<aCandlestick>();
            System.IO.StreamReader sr = new System.IO.StreamReader(path);
            string line;
            line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                string[] separators = new string[] { ",", "\"" };
                string[] parts = line.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 5)
                {
                    aCandlestick c = new aCandlestick();
                    c.date = System.DateTime.Parse(parts[0], System.Globalization.CultureInfo.InvariantCulture);
                    c.open = System.Math.Round(System.Double.Parse(parts[1], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture), 2);
                    c.high = System.Math.Round(System.Double.Parse(parts[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture), 2);
                    c.low = System.Math.Round(System.Double.Parse(parts[3], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture), 2);
                    c.close = System.Math.Round(System.Double.Parse(parts[4], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture), 2);
                    c.volume = System.UInt64.Parse(parts[5], System.Globalization.CultureInfo.InvariantCulture);
                    list.Add(c);
                }
            }
            sr.Close();
            return list;
        }

        /// <summary>
        /// filters candlestick data to a date range, returns a new list that only has the candlesticks that fall inside the range
        /// </summary>
        /// <param name="candlestickListParam">the candlesticks to filter</param>
        /// <param name="startDate">the first date to keep</param>
        /// <param name="endDate">the last date to keep</param>
        /// <returns>the candlesticks between the two dates</returns>
        public static System.Collections.Generic.List<aCandlestick> filterCandlestickData(System.Collections.Generic.List<aCandlestick> candlestickListParam, System.DateTime startDate, System.DateTime endDate)
        {
            System.Collections.Generic.List<aCandlestick> result = new System.Collections.Generic.List<aCandlestick>();

            if (candlestickListParam != null && candlestickListParam.Count > 0 && startDate <= endDate)
            {
                foreach (aCandlestick c in candlestickListParam)
                {
                    if (c.date >= startDate && c.date <= endDate)
                    {
                        result.Add(c);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// converts regular candlesticks to smart candlesticks, numbers them and marks which ones are peaks and valleys
        /// </summary>
        /// <param name="baseList">the candlesticks to convert</param>
        /// <returns>one smart candlestick per candlestick, with index, peakValue and valleyValue filled in</returns>
        public static System.Collections.Generic.List<aSmartCandlestick> buildSmartCandlesticks(System.Collections.Generic.List<aCandlestick> baseList)
        {
            System.Collections.Generic.List<aSmartCandlestick> smartList = new System.Collections.Generic.List<aSmartCandlestick>();

            // Pass 1: Convert base objects to smart objects and assign the index
            for (int i = 0; i < baseList.Count; i++)
            {
                aSmartCandlestick sc = new aSmartCandlestick(baseList[i]);
                sc.index = i;
                smartList.Add(sc);
            }

            // Pass 2: Evaluate peaks and valleys
            // Start at 1 and end at Count - 2 to prevent out-of-bounds errors on prev/next
            for (int i = 1; i < smartList.Count - 1; i++)
            {
                aSmartCandlestick current = smartList[i];
                aSmartCandlestick prev = smartList[i - 1];
                aSmartCandlestick next = smartList[i + 1];

                if (current.isPeak(prev, next))
                {
                    current.peakValue = current.high; // Valid coordinate for the chart
                }

                if (current.isValley(prev, next))
                {
                    current.valleyValue = current.low; // Valid coordinate for the chart
                }
            }

            return smartList;
        }

        // finds the peak or valley candle closest to the mouse: nearest by candle number first, nearest by price when two are equally close
        /// <summary>
        /// finds the peak or valley candle closest to a mouse position: nearest by candle number first, and nearest by price when two are equally close
        /// kindOut is set to Peak or Valley for the candle that is returned, and null is returned (kindOut untouched) when the list has no peak or valley at all
        /// </summary>
        /// <param name="candles">the smart candlesticks the chart is showing</param>
        /// <param name="indexPosition">the mouse position as a candle number counting from 0</param>
        /// <param name="price">the mouse position as a price</param>
        /// <param name="kindOut">filled in with Peak or Valley for the candle that is returned</param>
        /// <returns>the closest peak or valley candle, or null when there is none</returns>
        public static aSmartCandlestick findNearestExtreme(System.Collections.Generic.List<aSmartCandlestick> candles, double indexPosition, double price, ref extremeKind kindOut)
        {
            aSmartCandlestick best = null;
            double bestDistance = System.Double.MaxValue;
            double bestGap = System.Double.MaxValue;

            foreach (aSmartCandlestick candle in candles)
            {
                // peakValue and valleyValue are NaN on candles that are not a peak or valley
                bool isPeak = !System.Double.IsNaN(candle.peakValue);
                bool isValley = !System.Double.IsNaN(candle.valleyValue);
                if (!isPeak && !isValley)
                {
                    continue;
                }

                // how far the mouse price is from this candle's peak and from its valley, a kind this candle does not have is treated as infinitely far
                double peakGap = System.Double.MaxValue;
                if (isPeak)
                {
                    peakGap = System.Math.Abs(candle.peakValue - price);
                }
                double valleyGap = System.Double.MaxValue;
                if (isValley)
                {
                    valleyGap = System.Math.Abs(candle.valleyValue - price);
                }

                // a candle that is both a peak and a valley counts as whichever is nearer the mouse price, and a tie goes to the peak
                extremeKind candleKind = extremeKind.Peak;
                double gap = peakGap;
                if (valleyGap < peakGap)
                {
                    candleKind = extremeKind.Valley;
                    gap = valleyGap;
                }

                // how many candles away the mouse is
                double distance = System.Math.Abs(candle.index - indexPosition);

                // keep this candle if it is closer by candle number, or equally close and nearer by price
                if (distance < bestDistance || (distance == bestDistance && gap < bestGap))
                {
                    best = candle;
                    bestDistance = distance;
                    bestGap = gap;
                    kindOut = candleKind;
                }
            }
            return best;
        }
    }
}
