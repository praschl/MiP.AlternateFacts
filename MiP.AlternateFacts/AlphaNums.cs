using System;

namespace MiP.AlternateFacts
{
    [Flags]
    public enum AlphaNums
    {
        Numeric = 0x01,
        AlphaLower = 0x02,
        AlphaUpper = 0x04,
        AlphaNumericLower = Numeric | AlphaLower,
        AlphaNumericUpper = Numeric | AlphaUpper,
        Alpha = AlphaLower | AlphaUpper,
        All = Alpha | Numeric
    }
}