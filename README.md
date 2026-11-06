# Stock candlestick viewer (Windows Forms)

Windows Forms apps that load daily, weekly or monthly stock price files (CSV) and show them as a candlestick chart next to a data grid, with a start/end date filter. The same program is written twice, in C# and in C++/CLI, in stages. Stages 1 and 2 are complete. Stage 3 is under way, so expect more commits.

**Windows only.** Both versions target .NET Framework 4.7.2 and open in Visual Studio. They do not run on macOS or Linux, and this repository has no automated tests.

## Layout

| Folder | Language | What it adds |
|---|---|---|
| `SharpProject1` | C# | One form: pick a CSV, read it into `aCandlestick` objects, filter by date range, show chart and grid |
| `SharpProject2` | C# | `FormSelector` (pick one or more files and a date range, opens one chart window per file), `aSmartCandlestick` (subclass that records peaks and valleys), `candlestickUtilities` (CSV read, date filter, peak/valley pass) |
| `SharpProject3` | C# | Stage 3, in progress. It starts as a copy of stage 2 and will add a Fibonacci number predictor, additional dots on the chart, and a click-and-drag rectangle tool. Not implemented yet |
| `StockProject1` to `StockProject3` | C++/CLI | The same stages ported to C++/CLI (stage 3 follows the C# version) |
| `Stock Data` | CSV | Daily, weekly and monthly OHLCV files for ABBV, AES, APH, BBY, LHX, NOC, NVDA and USB (copies are also inside each project folder) |

## How peaks and valleys work

`buildSmartCandlesticks` converts the filtered `aCandlestick` list into `aSmartCandlestick` objects, numbers them, then makes a second pass over every candle that has a neighbor on each side. A candle is a **peak** if its high is above both neighbors' highs, and a **valley** if its low is below both neighbors' lows. The first and last candle in a range are never marked, so there is always a neighbor on both sides. The chart plots peaks and valleys as extra series, and they are recalculated whenever the date range changes.

## Input format

CSV with a header row and the columns `Date, Open, High, Low, Close, Volume`, one candle per line (see `Stock Data`).
