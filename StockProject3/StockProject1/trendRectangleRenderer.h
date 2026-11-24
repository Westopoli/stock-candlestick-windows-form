#pragma once
#include "aTrendRectangle.h"

using namespace System;

// draws an aTrendRectangle on top of the chart, this class knows about pixels, colors and the chart axes
// the rules that decide whether a rectangle is valid stay in aTrendRectangle
ref class trendRectangleRenderer
{
public:
	// the ratio lines, as fractions of the way from the start corner to the end corner: 23.6%, 38.2%, 50%, 61.8%, 78.6%
	array<double>^ ratios;
	// how thick each ratio line is, as a fraction of the rectangle height (2%)
	double lineThickness = 0.02;

	// an invalid rectangle is black, a valid one is red when it started on a peak (price falling) and green when it started on a valley (price rising)
	System::Drawing::Color invalidColor = System::Drawing::Color::Black;
	System::Drawing::Color fallingColor = System::Drawing::Color::Red;
	System::Drawing::Color risingColor = System::Drawing::Color::Green;
	// the dot drawn on any open, high, low or close that lands on a ratio line (orange)
	System::Drawing::Color dotColor = System::Drawing::Color::FromArgb(255, 128, 0);

	/// <summary>
	/// fills in the ratio list (C++/CLI does not allow a list of values to be written directly on the field above)
	/// </summary>
	trendRectangleRenderer()
	{
		ratios = gcnew array<double>{ 0.236, 0.382, 0.5, 0.618, 0.786 };
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
	int draw(System::Drawing::Graphics^ g, System::Windows::Forms::DataVisualization::Charting::Chart^ chart, aTrendRectangle^ rectangle, System::Collections::Generic::List<aSmartCandlestick^>^ candles, bool dragging);

private:
	/// <summary>
	/// draws the box outline, dashed while the mouse is still down and solid after it is released
	/// </summary>
	/// <param name="g">the graphics to draw on</param>
	/// <param name="color">the color of the outline</param>
	/// <param name="outline">the box between the two corners</param>
	/// <param name="dragging">true while the mouse is down, so the outline is dashed</param>
	void drawOutline(System::Drawing::Graphics^ g, System::Drawing::Color color, System::Drawing::Rectangle outline, bool dragging);
	/// <summary>
	/// builds one thin rectangle per ratio, each as wide as the outline
	/// </summary>
	/// <param name="outline">the box between the two corners</param>
	/// <param name="startY">the pixel height of the start corner</param>
	/// <param name="endY">the pixel height of the end corner</param>
	/// <returns>one line rectangle per ratio, in the order of the ratio list</returns>
	array<System::Drawing::Rectangle>^ buildLines(System::Drawing::Rectangle outline, int startY, int endY);
	/// <summary>
	/// draws every ratio line, dashed while the mouse is still down and solid after it is released
	/// </summary>
	/// <param name="g">the graphics to draw on</param>
	/// <param name="color">the color of the lines</param>
	/// <param name="lines">the line rectangles made by buildLines</param>
	/// <param name="dragging">true while the mouse is down, so the lines are dashed</param>
	void drawLines(System::Drawing::Graphics^ g, System::Drawing::Color color, array<System::Drawing::Rectangle>^ lines, bool dragging);
	/// <summary>
	/// draws a dot on every open, high, low or close under the rectangle that lands inside one of the line rectangles
	/// </summary>
	/// <param name="g">the graphics to draw on</param>
	/// <param name="area">the price chart area, used to turn candle numbers and prices into pixels</param>
	/// <param name="rectangle">the rectangle being drawn</param>
	/// <param name="candles">the candlesticks the chart is showing</param>
	/// <param name="lines">the line rectangles made by buildLines</param>
	/// <returns>the number of dots drawn, which is the number of touches</returns>
	int drawDots(System::Drawing::Graphics^ g, System::Windows::Forms::DataVisualization::Charting::ChartArea^ area, aTrendRectangle^ rectangle, System::Collections::Generic::List<aSmartCandlestick^>^ candles, array<System::Drawing::Rectangle>^ lines);
};
