using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace SharpProject1
{
    static class candlestickUtilities
    {
        // This class will contain static utility functions for working with candlestick data, specifically converting between aCandlestick and aSmartCandlestick objects, as well as any other common operations needed for candlestick analysis.

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
    }
}
