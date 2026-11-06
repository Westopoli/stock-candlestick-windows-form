#include "pch.h"
#include "aCandlestick.h"

using namespace System;
using namespace System::Globalization;

//
// Additional constructors/initializers for the managed ref class `aCandlestick`.
// (The default constructor is defined inline in the header.)
aCandlestick::aCandlestick()
{
    date = DateTime::MinValue;
    open = 0.0;
    high = 0.0;
    low = 0.0;
    close = 0.0;
    volume = 0;
}

aCandlestick::aCandlestick(DateTime dateVal, double openVal, double highVal, double lowVal, double closeVal, unsigned long long volumeVal)
{
    date = dateVal;
    open = openVal;
    high = highVal;
    low = lowVal;
    close = closeVal;
    volume = volumeVal;
}

aCandlestick::aCandlestick(aCandlestick^ source)
{
    if (source == nullptr)
    {
        date = DateTime::MinValue;
        open = 0.0;
        high = 0.0;
        low = 0.0;
        close = 0.0;
        volume = 0;
    }
    else
    {
        date = source->date;
        open = source->open;
        high = source->high;
        low = source->low;
        close = source->close;
        volume = source->volume;
    }
}

aCandlestick::aCandlestick(String^ csvLine)
{
    array<Char>^ separator = gcnew array<Char>{ ',', '"' };
    array<String^>^ parts = csvLine->Split(separator, StringSplitOptions::RemoveEmptyEntries);

    if (parts->Length >= 6)
    {
        // Expecting format: "yyyy-MM-dd,open,high,low,close,volume"
        date = DateTime::ParseExact(parts[0], "yyyy-MM-dd", nullptr);
        open = Double::Parse(parts[1], CultureInfo::InvariantCulture);
        high = Double::Parse(parts[2], CultureInfo::InvariantCulture);
        low = Double::Parse(parts[3], CultureInfo::InvariantCulture);
        close = Double::Parse(parts[4], CultureInfo::InvariantCulture);
        volume = UInt64::Parse(parts[5], CultureInfo::InvariantCulture);
    }
    else
    {
        // Fallback to defaults if parsing fails
        date = DateTime::MinValue;
        open = 0.0;
        high = 0.0;
        low = 0.0;
        close = 0.0;
        volume = 0;
    }
}