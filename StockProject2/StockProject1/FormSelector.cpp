#include "pch.h"
#include "FormSelector.h"

using namespace System::Globalization;

/// <summary>
/// Reads candlestick data from a CSV file, where each line represents a candlestick with the format "date,open,high,low,close,volume".
/// </summary>
/// <param name="path"></param>
/// <returns></returns>
System::Collections::Generic::List<aCandlestick^>^ StockProject1::FormSelector::readCandlesticksFromCsv(String^ path)
{
	// Create a new list to hold the candlestick data
	System::Collections::Generic::List<aCandlestick^>^ list = gcnew System::Collections::Generic::List<aCandlestick^>();
	// Open the CSV file for reading
	System::IO::StreamReader^ sr = gcnew System::IO::StreamReader(path);
	// Read each line of the CSV file and parse it into aCandlestick objects
	String^ line;
	// read header
	line = sr->ReadLine();
	// read data lines while not end of file
	while ((line = sr->ReadLine()) != nullptr)
	{
		array<String^>^ separators = gcnew array<String^>{ ",", "\"" };
		// split the line by commas and double quotes
		array<String^>^ parts = line->Split(separators, StringSplitOptions::RemoveEmptyEntries);
		// seperators for both commans and 
		// check if we have at least 5 parts (date, open, high, low, close)
		if (parts->Length >= 5)
		{
			// create a new aCandlestick object and populate its properties
			aCandlestick^ c = gcnew aCandlestick();

			// parse common CSV: date,open,high,low,close[,volume] and round to two decimlam places for open, high, low, close. Volume is optional and defaults to 0 if not present.
			c->date = DateTime::Parse(parts[0], CultureInfo::InvariantCulture);
			c->open = Math::Round(Double::Parse(parts[1], NumberStyles::Float, CultureInfo::InvariantCulture), 2);
			c->high = Math::Round(Double::Parse(parts[2], NumberStyles::Float, CultureInfo::InvariantCulture), 2);
			c->low = Math::Round(Double::Parse(parts[3], NumberStyles::Float, CultureInfo::InvariantCulture), 2);
			c->close = Math::Round(Double::Parse(parts[4], NumberStyles::Float, CultureInfo::InvariantCulture), 2);
			c->volume = UInt64::Parse(parts[5], CultureInfo::InvariantCulture);

			// add the candlestick to the list
			list->Add(c);
		}
	}
	// close the stream reader
	delete sr;

	// update member list and return
	this->candlestickList = list;
	return list;
}

/// <summary>
/// Filters candlestick data based on a date range input by the user. Returns a new list containing only the candlesticks that fall within the specified date range.
/// </summary>
/// <param name="candlestickListParam"></param>
/// <param name="startDate"></param>
/// <param name="endDate"></param>
/// <returns></returns>
System::Collections::Generic::List<aCandlestick^>^ StockProject1::FormSelector::filterCandlestickData(System::Collections::Generic::List<aCandlestick^>^ candlestickListParam, DateTime startDate, DateTime endDate)
{
	// Create a new list to hold the filtered candlestick data
	System::Collections::Generic::List<aCandlestick^>^ result = gcnew System::Collections::Generic::List<aCandlestick^>();

	// Check if the input list is not null and has elements, and that the start date is less than or equal to the end date
	if (candlestickListParam != nullptr && candlestickListParam->Count > 0 && startDate <= endDate)
	{
		// Iterate through each candlestick in the input list
		for each (aCandlestick ^ c in candlestickListParam)
		{
			// Check if the candlestick's date falls within the specified date range, if it does than add it to the result list
			if (c->date >= startDate && c->date <= endDate)
			{
				result->Add(c);
			}
		}
	}
	return result;
}