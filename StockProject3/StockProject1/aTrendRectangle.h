#pragma once
#include "aSmartCandlestick.h"
#include "extremeKind.h"

using namespace System;

// a trend rectangle drawn by the user, it starts on a peak or valley candle and ends wherever the mouse is
// this class stores candle numbers and prices only (no pixels, no WinForms), and it knows the rules that make a rectangle valid
ref class aTrendRectangle
{
public:
	// the start corner: a candle number, the price on that candle, and whether it is a peak or a valley
	property int startIndex;
	property double startPrice;
	property extremeKind kind;
	// the end corner: a candle number and a price, both follow the mouse
	property int endIndex;
	property double endPrice;

	/// <summary>
	/// starts the rectangle on a peak or valley candle, the end begins on the same spot so the rectangle starts with no size
	/// </summary>
	/// <param name="startCandle">the peak or valley candle the rectangle starts on</param>
	/// <param name="startKind">Peak or Valley, which of the two the start candle is being used as</param>
	aTrendRectangle(aSmartCandlestick^ startCandle, extremeKind startKind);

	/// <summary>
	/// moves the end corner, called on every mouse move
	/// </summary>
	/// <param name="index">the candle number under the mouse</param>
	/// <param name="price">the price under the mouse</param>
	void setEnd(int index, double price);

	/// <summary>
	/// returns nullptr when the rectangle is valid, otherwise a sentence saying which rule failed
	/// </summary>
	/// <param name="candles">the candlesticks the chart is showing</param>
	/// <returns>nullptr, or the message of the first rule that failed</returns>
	System::String^ checkRules(System::Collections::Generic::List<aSmartCandlestick^>^ candles);

	/// <summary>
	/// true when checkRules finds nothing wrong
	/// </summary>
	/// <param name="candles">the candlesticks the chart is showing</param>
	/// <returns>true when the rectangle is valid</returns>
	bool isValid(System::Collections::Generic::List<aSmartCandlestick^>^ candles);

private:
	// one method per rule, each returns nullptr when its rule passes
	/// <summary>
	/// the size rule: the end corner must be on a different candle and at a different price than the start corner
	/// </summary>
	/// <returns>nullptr when the rule passes, otherwise a message</returns>
	System::String^ checkSize();
	/// <summary>
	/// the direction rule: a peak start must end lower and a valley start must end higher
	/// </summary>
	/// <returns>nullptr when the rule passes, otherwise a message</returns>
	System::String^ checkDirection();
	/// <summary>
	/// the breach rule: no open, high, low or close under the rectangle may lie above its roof or below its floor
	/// </summary>
	/// <param name="candles">the candlesticks the chart is showing</param>
	/// <returns>nullptr when the rule passes, otherwise a message</returns>
	System::String^ checkBreach(System::Collections::Generic::List<aSmartCandlestick^>^ candles);
};
