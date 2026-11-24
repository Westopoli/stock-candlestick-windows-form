#pragma once
#include "aCandlestick.h"
#include "aSmartCandlestick.h"
#include "extremeKind.h"

ref class candlestickUtilities
{
	// This class will contain static utility functions for working with candlestick data, specifically converting between aCandlestick and aSmartCandlestick objects, as well as any other common operations needed for candlestick analysis.

public:
	/// <summary>
	/// reads candlestick data from a CSV file, where each line is "date,open,high,low,close,volume"
	/// </summary>
	/// <param name="path">the full path of the CSV file</param>
	/// <returns>one candlestick per line of the file</returns>
	static System::Collections::Generic::List<aCandlestick^>^ readCandlesticksFromCsv(System::String^ path);
	/// <summary>
	/// filters candlestick data to a date range, returns a new list that only has the candlesticks that fall inside the range
	/// </summary>
	/// <param name="candlestickListParam">the candlesticks to filter</param>
	/// <param name="startDate">the first date to keep</param>
	/// <param name="endDate">the last date to keep</param>
	/// <returns>the candlesticks between the two dates</returns>
	static System::Collections::Generic::List<aCandlestick^>^ filterCandlestickData(System::Collections::Generic::List<aCandlestick^>^ candlestickListParam, System::DateTime startDate, System::DateTime endDate);
	/// <summary>
	/// converts regular candlesticks to smart candlesticks, numbers them and marks which ones are peaks and valleys
	/// </summary>
	/// <param name="baseList">the candlesticks to convert</param>
	/// <returns>one smart candlestick per candlestick, with index, peakValue and valleyValue filled in</returns>
	static System::Collections::Generic::List<aSmartCandlestick^>^ buildSmartCandlesticks(System::Collections::Generic::List<aCandlestick^>^ baseList);

	/// <summary>
	/// finds the peak or valley candle closest to a mouse position: nearest by candle number first, and nearest by price when two are equally close
	/// kindOut is set to Peak or Valley for the candle that is returned, and nullptr is returned (kindOut untouched) when the list has no peak or valley at all
	/// </summary>
	/// <param name="candles">the smart candlesticks the chart is showing</param>
	/// <param name="indexPosition">the mouse position as a candle number counting from 0</param>
	/// <param name="price">the mouse position as a price</param>
	/// <param name="kindOut">filled in with Peak or Valley for the candle that is returned</param>
	/// <returns>the closest peak or valley candle, or nullptr when there is none</returns>
	static aSmartCandlestick^ findNearestExtreme(System::Collections::Generic::List<aSmartCandlestick^>^ candles, double indexPosition, double price, extremeKind% kindOut);
};

