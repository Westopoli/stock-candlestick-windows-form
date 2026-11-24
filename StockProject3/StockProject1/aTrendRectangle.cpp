#include "pch.h"
#include "aTrendRectangle.h"

aTrendRectangle::aTrendRectangle(aSmartCandlestick^ startCandle, extremeKind startKind)
{
	kind = startKind;
	startIndex = startCandle->index;
	// a peak starts at the high of its candle, a valley starts at the low of its candle
	if (startKind == extremeKind::Peak)
	{
		startPrice = startCandle->high;
	}
	else
	{
		startPrice = startCandle->low;
	}
	// the end begins on top of the start, so a click without dragging gives a rectangle with no size
	endIndex = startIndex;
	endPrice = startPrice;
}

void aTrendRectangle::setEnd(int index, double price)
{
	endIndex = index;
	endPrice = price;
}

System::String^ aTrendRectangle::checkRules(System::Collections::Generic::List<aSmartCandlestick^>^ candles)
{
	// the rules run in order and the first one that fails is the one reported
	System::String^ problem = checkSize();
	if (problem == nullptr)
	{
		problem = checkDirection();
	}
	if (problem == nullptr)
	{
		problem = checkBreach(candles);
	}
	return problem;
}

bool aTrendRectangle::isValid(System::Collections::Generic::List<aSmartCandlestick^>^ candles)
{
	return checkRules(candles) == nullptr;
}

// rule 1: the rectangle must have a width and a height
System::String^ aTrendRectangle::checkSize()
{
	if (endIndex == startIndex || endPrice == startPrice)
	{
		return "Zero size: the end is on the start candle or at the start price.";
	}
	return nullptr;
}

// rule 2: the end must move toward the opposite kind, down from a peak and up from a valley
System::String^ aTrendRectangle::checkDirection()
{
	if (kind == extremeKind::Peak && endPrice >= startPrice)
	{
		return "Started on a peak, so the end must be below the start price.";
	}
	if (kind == extremeKind::Valley && endPrice <= startPrice)
	{
		return "Started on a valley, so the end must be above the start price.";
	}
	return nullptr;
}

// rule 3: no open, high, low or close under the rectangle may stick out of its top (roof) or bottom (floor)
System::String^ aTrendRectangle::checkBreach(System::Collections::Generic::List<aSmartCandlestick^>^ candles)
{
	// the roof and floor are the higher and lower of the two corner prices, whichever corner each came from
	double roofPrice = Math::Max(startPrice, endPrice);
	double floorPrice = Math::Min(startPrice, endPrice);
	// the candles under the rectangle run from its leftmost corner to its rightmost corner, so dragging left works too
	int first = Math::Min(startIndex, endIndex);
	int last = Math::Max(startIndex, endIndex);

	for (int i = first; i <= last; i++)
	{
		aSmartCandlestick^ c = candles[i];
		array<double>^ values = gcnew array<double>{ c->open, c->high, c->low, c->close };
		array<System::String^>^ names = gcnew array<System::String^>{ "open", "high", "low", "close" };

		for (int v = 0; v < values->Length; v++)
		{
			if (values[v] > roofPrice)
			{
				return "Candle #" + i.ToString() + " " + names[v] + " (" + values[v].ToString("F2") + ") is above the roof (" + roofPrice.ToString("F2") + ").";
			}
			if (values[v] < floorPrice)
			{
				return "Candle #" + i.ToString() + " " + names[v] + " (" + values[v].ToString("F2") + ") is below the floor (" + floorPrice.ToString("F2") + ").";
			}
		}
	}
	return nullptr;
}
