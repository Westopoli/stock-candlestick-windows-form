#pragma once
using namespace System;
public ref class aCandlestick
{
	// variable definitions
	
public:
	property DateTime date; 
	property double open;
	property double high;
	property double low;
	property double close;
	property unsigned long long volume;

	/// <summary>
	/// default constructor, add initialize components to the constructor
	/// </summary>
	aCandlestick();
	/// <summary>
	/// additional constructors
	/// </summary>
	/// <param name="dateVal">the date of the candlestick</param>
	/// <param name="openVal">the opening price</param>
	/// <param name="highVal">the highest price</param>
	/// <param name="lowVal">the lowest price</param>
	/// <param name="closeVal">the closing price</param>
	/// <param name="volumeVal">the volume traded</param>
	aCandlestick(DateTime dateVal, double openVal, double highVal, double lowVal, double closeVal, unsigned long long volumeVal);
	/// <summary>
	/// constructor from another candlestick (copy)
	/// </summary>
	/// <param name="source">the candlestick to copy</param>
	aCandlestick(aCandlestick^ source);
	/// <summary>
	/// constructor from a CSV line
	/// </summary>
	/// <param name="csvLine">one line of a CSV file</param>
	aCandlestick(String^ csvLine);
};

