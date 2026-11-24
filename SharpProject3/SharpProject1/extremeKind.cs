using System;

namespace SharpProject1
{
    // the two kinds of turning point a candlestick can be, a local high (Peak) or a local low (Valley)
    // using a named type instead of a bool means the code reads extremeKind.Peak instead of "true"
    enum extremeKind
    {
        Peak,
        Valley
    }
}
