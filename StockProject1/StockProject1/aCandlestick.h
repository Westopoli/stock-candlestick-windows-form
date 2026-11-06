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
	property unsigned long volume;

	// default constructor
	aCandlestick();
	// additional constructors
	aCandlestick(DateTime dateVal, double openVal, double highVal, double lowVal, double closeVal, unsigned long volumeVal);
	// constructor from another candlestick (copy)
	aCandlestick(aCandlestick^ source);
	// constructor from a CSV line
	aCandlestick(String^ csvLine);
};

