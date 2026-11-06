#include "pch.h"
#include "candlestickUtilities.h"

System::Collections::Generic::List<aCandlestick^>^ candlestickUtilities::readCandlesticksFromCsv(System::String^ path)
{
	System::Collections::Generic::List<aCandlestick^>^ list = gcnew System::Collections::Generic::List<aCandlestick^>();
	System::IO::StreamReader^ sr = gcnew System::IO::StreamReader(path);
	System::String^ line;
	line = sr->ReadLine();
	while ((line = sr->ReadLine()) != nullptr)
	{
		array<System::String^>^ separators = gcnew array<System::String^>{ ",", "\"" };
		array<System::String^>^ parts = line->Split(separators, System::StringSplitOptions::RemoveEmptyEntries);
		if (parts->Length >= 5)
		{
			aCandlestick^ c = gcnew aCandlestick();
			c->date = System::DateTime::Parse(parts[0], System::Globalization::CultureInfo::InvariantCulture);
			c->open = System::Math::Round(System::Double::Parse(parts[1], System::Globalization::NumberStyles::Float, System::Globalization::CultureInfo::InvariantCulture), 2);
			c->high = System::Math::Round(System::Double::Parse(parts[2], System::Globalization::NumberStyles::Float, System::Globalization::CultureInfo::InvariantCulture), 2);
			c->low = System::Math::Round(System::Double::Parse(parts[3], System::Globalization::NumberStyles::Float, System::Globalization::CultureInfo::InvariantCulture), 2);
			c->close = System::Math::Round(System::Double::Parse(parts[4], System::Globalization::NumberStyles::Float, System::Globalization::CultureInfo::InvariantCulture), 2);
			c->volume = System::UInt64::Parse(parts[5], System::Globalization::CultureInfo::InvariantCulture);
			list->Add(c);
		}
	}
	delete sr;
	return list;
}

System::Collections::Generic::List<aCandlestick^>^ candlestickUtilities::filterCandlestickData(System::Collections::Generic::List<aCandlestick^>^ candlestickListParam, System::DateTime startDate, System::DateTime endDate)
{
	System::Collections::Generic::List<aCandlestick^>^ result = gcnew System::Collections::Generic::List<aCandlestick^>();

	if (candlestickListParam != nullptr && candlestickListParam->Count > 0 && startDate <= endDate)
	{
		for each (aCandlestick^ c in candlestickListParam)
		{
			if (c->date >= startDate && c->date <= endDate)
			{
				result->Add(c);
			}
		}
	}
	return result;
}

System::Collections::Generic::List<aSmartCandlestick^>^ candlestickUtilities::buildSmartCandlesticks(System::Collections::Generic::List<aCandlestick^>^ baseList)
{
	System::Collections::Generic::List<aSmartCandlestick^>^ smartList = gcnew System::Collections::Generic::List<aSmartCandlestick^>();

	// Pass 1: Convert base objects to smart objects and assign the index
	for (int i = 0; i < baseList->Count; i++)
	{
		aSmartCandlestick^ sc = gcnew aSmartCandlestick(baseList[i]);
		sc->index = i;
		smartList->Add(sc);
	}

	// Pass 2: Evaluate peaks and valleys
	// Start at 1 and end at Count - 2 to prevent out-of-bounds errors on prev/next
	for (int i = 1; i < smartList->Count - 1; i++)
	{
		aSmartCandlestick^ current = smartList[i];
		aSmartCandlestick^ prev = smartList[i - 1];
		aSmartCandlestick^ next = smartList[i + 1];

		if (current->isPeak(prev, next))
		{
			current->peakValue = current->high; // Valid coordinate for the chart
		}

		if (current->isValley(prev, next))
		{
			current->valleyValue = current->low; // Valid coordinate for the chart
		}
	}

	return smartList;
}
