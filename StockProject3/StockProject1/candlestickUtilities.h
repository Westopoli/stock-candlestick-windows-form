#pragma once
#include "aCandlestick.h"
#include "aSmartCandlestick.h"

ref class candlestickUtilities
{
	// This class will contain static utility functions for working with candlestick data, specifically converting between aCandlestick and aSmartCandlestick objects, as well as any other common operations needed for candlestick analysis.

public:
	static System::Collections::Generic::List<aCandlestick^>^ readCandlesticksFromCsv(System::String^ path);
	static System::Collections::Generic::List<aCandlestick^>^ filterCandlestickData(System::Collections::Generic::List<aCandlestick^>^ candlestickListParam, System::DateTime startDate, System::DateTime endDate);
	static System::Collections::Generic::List<aSmartCandlestick^>^ buildSmartCandlesticks(System::Collections::Generic::List<aCandlestick^>^ baseList);
};

