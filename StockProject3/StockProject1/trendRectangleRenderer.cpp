#include "pch.h"
#include "trendRectangleRenderer.h"

using namespace System::Drawing;
using namespace System::Windows::Forms::DataVisualization::Charting;

int trendRectangleRenderer::draw(Graphics^ g, Chart^ chart, aTrendRectangle^ rectangle, System::Collections::Generic::List<aSmartCandlestick^>^ candles, bool dragging)
{
	// the price chart is the first chart area, the volume chart under it is the second
	ChartArea^ area = chart->ChartAreas[0];

	// turn the two corners into pixels
	// the chart places candles by their number counting from 1 (not by date and not from 0), so candle number 0 sits at position 1
	int startX = (int)Math::Round(area->AxisX->ValueToPixelPosition(rectangle->startIndex + 1));
	int startY = (int)Math::Round(area->AxisY->ValueToPixelPosition(rectangle->startPrice));
	int endX = (int)Math::Round(area->AxisX->ValueToPixelPosition(rectangle->endIndex + 1));
	int endY = (int)Math::Round(area->AxisY->ValueToPixelPosition(rectangle->endPrice));

	// the outline is the box between the two corners, whichever way the user dragged
	Rectangle outline = Rectangle(Math::Min(startX, endX), Math::Min(startY, endY), Math::Abs(endX - startX), Math::Abs(endY - startY));

	// an invalid rectangle is only a black outline, no lines and no dots, so it has no touches
	if (!rectangle->isValid(candles))
	{
		drawOutline(g, invalidColor, outline, dragging);
		return 0;
	}

	// a valid rectangle is red when it started on a peak and green when it started on a valley
	Color color = fallingColor;
	if (rectangle->kind == extremeKind::Valley)
	{
		color = risingColor;
	}
	drawOutline(g, color, outline, dragging);

	// the ratio lines, drawn in the same color as the outline
	array<Rectangle>^ lines = buildLines(outline, startY, endY);
	drawLines(g, color, lines, dragging);

	// the number of dots drawn is the number of touches
	return drawDots(g, area, rectangle, candles, lines);
}

void trendRectangleRenderer::drawLines(Graphics^ g, Color color, array<Rectangle>^ lines, bool dragging)
{
	for each (Rectangle line in lines)
	{
		// the pen is as thick as the line rectangle, so it covers the same band that the dots are tested against
		Pen^ pen = gcnew Pen(color, (float)line.Height);
		if (dragging)
		{
			pen->DashStyle = System::Drawing::Drawing2D::DashStyle::Dash;
		}
		// the line runs through the middle of its rectangle, from its left side to its right side
		float middleY = line.Top + line.Height / 2.0f;
		g->DrawLine(pen, (float)line.Left, middleY, (float)line.Right, middleY);
		delete pen;
	}
}

void trendRectangleRenderer::drawOutline(Graphics^ g, Color color, Rectangle outline, bool dragging)
{
	Pen^ pen = gcnew Pen(color, 2);
	if (dragging)
	{
		pen->DashStyle = System::Drawing::Drawing2D::DashStyle::Dash;
	}
	g->DrawRectangle(pen, outline);
	delete pen;
}

array<Rectangle>^ trendRectangleRenderer::buildLines(Rectangle outline, int startY, int endY)
{
	// each line is a thin rectangle, lineThickness times as thick as the outline is tall, but never thinner than 1 pixel
	int thickness = (int)Math::Round(outline.Height * lineThickness);
	if (thickness < 1)
	{
		thickness = 1;
	}

	array<Rectangle>^ lines = gcnew array<Rectangle>(ratios->Length);
	for (int i = 0; i < ratios->Length; i++)
	{
		// the ratio counts from the start corner toward the end corner, so the same formula works for every drag direction
		double centerY = startY + ratios[i] * (endY - startY);
		int top = (int)Math::Round(centerY - thickness / 2.0);
		// one pixel wider than the outline, so a candle sitting on the far edge still counts
		lines[i] = Rectangle(outline.X, top, outline.Width + 1, thickness);
	}
	return lines;
}

int trendRectangleRenderer::drawDots(Graphics^ g, ChartArea^ area, aTrendRectangle^ rectangle, System::Collections::Generic::List<aSmartCandlestick^>^ candles, array<Rectangle>^ lines)
{
	// the candles under the rectangle, from its leftmost corner to its rightmost corner
	int first = Math::Min(rectangle->startIndex, rectangle->endIndex);
	int last = Math::Max(rectangle->startIndex, rectangle->endIndex);

	// every dot drawn is one touch
	int touches = 0;
	SolidBrush^ brush = gcnew SolidBrush(dotColor);
	for (int i = first; i <= last; i++)
	{
		aSmartCandlestick^ c = candles[i];
		int x = (int)Math::Round(area->AxisX->ValueToPixelPosition(i + 1));
		array<double>^ prices = gcnew array<double>{ c->open, c->high, c->low, c->close };

		for each (double price in prices)
		{
			int y = (int)Math::Round(area->AxisY->ValueToPixelPosition(price));
			// a price that lands inside any line rectangle gets a dot centered on it
			for each (Rectangle line in lines)
			{
				if (line.Contains(x, y))
				{
					g->FillEllipse(brush, x - 5, y - 5, 10, 10);
					touches++;
				}
			}
		}
	}
	delete brush;
	return touches;
}
