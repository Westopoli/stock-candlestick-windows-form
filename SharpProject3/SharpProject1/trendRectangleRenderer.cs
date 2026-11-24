using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace SharpProject1
{
    // draws an aTrendRectangle on top of the chart, this class knows about pixels, colors and the chart axes
    // the rules that decide whether a rectangle is valid stay in aTrendRectangle
    class trendRectangleRenderer
    {
        // the ratio lines, as fractions of the way from the start corner to the end corner: 23.6%, 38.2%, 50%, 61.8%, 78.6%
        public double[] ratios;
        // how thick each ratio line is, as a fraction of the rectangle height (2%)
        public double lineThickness = 0.02;

        // an invalid rectangle is black, a valid one is red when it started on a peak (price falling) and green when it started on a valley (price rising)
        public System.Drawing.Color invalidColor = System.Drawing.Color.Black;
        public System.Drawing.Color fallingColor = System.Drawing.Color.Red;
        public System.Drawing.Color risingColor = System.Drawing.Color.Green;
        // the dot drawn on any open, high, low or close that lands on a ratio line (orange)
        public System.Drawing.Color dotColor = System.Drawing.Color.FromArgb(255, 128, 0);

        /// <summary>
        /// fills in the ratio list
        /// </summary>
        public trendRectangleRenderer()
        {
            ratios = new double[] { 0.236, 0.382, 0.5, 0.618, 0.786 };
        }

        /// <summary>
        /// draws the rectangle, and when it is valid also the ratio lines and the dots
        /// this must be called while the chart is painting, because the chart only converts prices to pixels during a paint event
        /// </summary>
        /// <param name="g">the graphics of the chart's paint event</param>
        /// <param name="chart">the chart the rectangle is drawn on</param>
        /// <param name="rectangle">the rectangle to draw</param>
        /// <param name="candles">the candlesticks the chart is showing</param>
        /// <param name="dragging">true while the mouse is down, so the drawing is dashed</param>
        /// <returns>the number of touches, which is the number of dots drawn (0 for an invalid rectangle)</returns>
        public int draw(Graphics g, Chart chart, aTrendRectangle rectangle, System.Collections.Generic.List<aSmartCandlestick> candles, bool dragging)
        {
            // the price chart is the first chart area, the volume chart under it is the second
            ChartArea area = chart.ChartAreas[0];

            // turn the two corners into pixels
            // the chart places candles by their number counting from 1 (not by date and not from 0), so candle number 0 sits at position 1
            int startX = (int)Math.Round(area.AxisX.ValueToPixelPosition(rectangle.startIndex + 1));
            int startY = (int)Math.Round(area.AxisY.ValueToPixelPosition(rectangle.startPrice));
            int endX = (int)Math.Round(area.AxisX.ValueToPixelPosition(rectangle.endIndex + 1));
            int endY = (int)Math.Round(area.AxisY.ValueToPixelPosition(rectangle.endPrice));

            // the outline is the box between the two corners, whichever way the user dragged
            Rectangle outline = new Rectangle(Math.Min(startX, endX), Math.Min(startY, endY), Math.Abs(endX - startX), Math.Abs(endY - startY));

            // an invalid rectangle is only a black outline, no lines and no dots, so it has no touches
            if (!rectangle.isValid(candles))
            {
                drawOutline(g, invalidColor, outline, dragging);
                return 0;
            }

            // a valid rectangle is red when it started on a peak and green when it started on a valley
            Color color = fallingColor;
            if (rectangle.kind == extremeKind.Valley)
            {
                color = risingColor;
            }
            drawOutline(g, color, outline, dragging);

            // the ratio lines, drawn in the same color as the outline
            Rectangle[] lines = buildLines(outline, startY, endY);
            drawLines(g, color, lines, dragging);

            // the number of dots drawn is the number of touches
            return drawDots(g, area, rectangle, candles, lines);
        }

        /// <summary>
        /// draws every ratio line, dashed while the mouse is still down and solid after it is released
        /// </summary>
        /// <param name="g">the graphics to draw on</param>
        /// <param name="color">the color of the lines</param>
        /// <param name="lines">the line rectangles made by buildLines</param>
        /// <param name="dragging">true while the mouse is down, so the lines are dashed</param>
        private void drawLines(Graphics g, Color color, Rectangle[] lines, bool dragging)
        {
            foreach (Rectangle line in lines)
            {
                // the pen is as thick as the line rectangle, so it covers the same band that the dots are tested against
                Pen pen = new Pen(color, (float)line.Height);
                if (dragging)
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                }
                // the line runs through the middle of its rectangle, from its left side to its right side
                float middleY = line.Top + line.Height / 2.0f;
                g.DrawLine(pen, (float)line.Left, middleY, (float)line.Right, middleY);
                pen.Dispose();
            }
        }

        /// <summary>
        /// draws the box outline, dashed while the mouse is still down and solid after it is released
        /// </summary>
        /// <param name="g">the graphics to draw on</param>
        /// <param name="color">the color of the outline</param>
        /// <param name="outline">the box between the two corners</param>
        /// <param name="dragging">true while the mouse is down, so the outline is dashed</param>
        private void drawOutline(Graphics g, Color color, Rectangle outline, bool dragging)
        {
            Pen pen = new Pen(color, 2);
            if (dragging)
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            }
            g.DrawRectangle(pen, outline);
            pen.Dispose();
        }

        /// <summary>
        /// builds one thin rectangle per ratio, each as wide as the outline
        /// </summary>
        /// <param name="outline">the box between the two corners</param>
        /// <param name="startY">the pixel height of the start corner</param>
        /// <param name="endY">the pixel height of the end corner</param>
        /// <returns>one line rectangle per ratio, in the order of the ratio list</returns>
        private Rectangle[] buildLines(Rectangle outline, int startY, int endY)
        {
            // each line is a thin rectangle, lineThickness times as thick as the outline is tall, but never thinner than 1 pixel
            int thickness = (int)Math.Round(outline.Height * lineThickness);
            if (thickness < 1)
            {
                thickness = 1;
            }

            Rectangle[] lines = new Rectangle[ratios.Length];
            for (int i = 0; i < ratios.Length; i++)
            {
                // the ratio counts from the start corner toward the end corner, so the same formula works for every drag direction
                double centerY = startY + ratios[i] * (endY - startY);
                int top = (int)Math.Round(centerY - thickness / 2.0);
                // one pixel wider than the outline, so a candle sitting on the far edge still counts
                lines[i] = new Rectangle(outline.X, top, outline.Width + 1, thickness);
            }
            return lines;
        }

        /// <summary>
        /// draws a dot on every open, high, low or close under the rectangle that lands inside one of the line rectangles
        /// </summary>
        /// <param name="g">the graphics to draw on</param>
        /// <param name="area">the price chart area, used to turn candle numbers and prices into pixels</param>
        /// <param name="rectangle">the rectangle being drawn</param>
        /// <param name="candles">the candlesticks the chart is showing</param>
        /// <param name="lines">the line rectangles made by buildLines</param>
        /// <returns>the number of dots drawn, which is the number of touches</returns>
        private int drawDots(Graphics g, ChartArea area, aTrendRectangle rectangle, System.Collections.Generic.List<aSmartCandlestick> candles, Rectangle[] lines)
        {
            // the candles under the rectangle, from its leftmost corner to its rightmost corner
            int first = Math.Min(rectangle.startIndex, rectangle.endIndex);
            int last = Math.Max(rectangle.startIndex, rectangle.endIndex);

            // every dot drawn is one touch
            int touches = 0;
            SolidBrush brush = new SolidBrush(dotColor);
            for (int i = first; i <= last; i++)
            {
                aSmartCandlestick c = candles[i];
                int x = (int)Math.Round(area.AxisX.ValueToPixelPosition(i + 1));
                double[] prices = new double[] { c.open, c.high, c.low, c.close };

                foreach (double price in prices)
                {
                    int y = (int)Math.Round(area.AxisY.ValueToPixelPosition(price));
                    // a price that lands inside any line rectangle gets a dot centered on it
                    foreach (Rectangle line in lines)
                    {
                        if (line.Contains(x, y))
                        {
                            g.FillEllipse(brush, x - 5, y - 5, 10, 10);
                            touches++;
                        }
                    }
                }
            }
            brush.Dispose();
            return touches;
        }
    }
}
