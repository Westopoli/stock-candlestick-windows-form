#pragma once
#include "aCandlestick.h"

using namespace System;

// add index, peaks, and valleys to the candlestick class, and add functions isPeak() and isValley() to determine if the candlestick is a peak or valley based on its high and low values compared to its immediate neighbors.
ref class aSmartCandlestick : public aCandlestick
{
	public: 
		property int index;
		property double peakValue;
		property double valleyValue;

	/// <summary>
	/// default constructor
	/// </summary>
	aSmartCandlestick() : aCandlestick()
	{
		index = 0;
		peakValue = Double::NaN;
		valleyValue = Double::NaN;
	}
	/// <summary>
	/// constructor that takes an existing regular aCandlestick and initializes the aSmartCandlestick with its values
	/// </summary>
	/// <param name="candlestick">the regular candlestick to copy the values from</param>
	aSmartCandlestick(aCandlestick^ candlestick) : aCandlestick()
	{
		// Initialize the base class properties
		this->date = candlestick->date;
		this->open = candlestick->open;
		this->high = candlestick->high;
		this->low = candlestick->low;
		this->close = candlestick->close;
		this->volume = candlestick->volume;
		this->index = 0;
		this->peakValue = Double::NaN; 
		this->valleyValue = Double::NaN;
	}

	/// <summary>
	/// isPeak() function to determine if the candlestick is a peak based on its high value compared to its immediate neighbors
	/// </summary>
	/// <param name="prev">the candlestick before this one</param>
	/// <param name="next">the candlestick after this one</param>
	/// <returns>true when this candlestick's high is above the high of both neighbors</returns>
	bool isPeak(aSmartCandlestick^ prev, aSmartCandlestick^ next)
	{
		if (prev == nullptr || next == nullptr)
		{
			return false;
		}
		return (this->high > prev->high && this->high > next->high);
	}

	/// <summary>
	/// isValley() function to determine if the candlestick is a valley based on its low value compared to its immediate neighbors
	/// </summary>
	/// <param name="prev">the candlestick before this one</param>
	/// <param name="next">the candlestick after this one</param>
	/// <returns>true when this candlestick's low is below the low of both neighbors</returns>
	bool isValley(aSmartCandlestick^ prev, aSmartCandlestick^ next)
	{
		if (prev == nullptr || next == nullptr)
		{
			return false;
		}
		return (this->low < prev->low && this->low < next->low);
	}
};

