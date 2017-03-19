using System;

namespace MiP.AlternateFacts
{
    /// <summary>
    /// Defines values for kinds of alpha numerics.
    /// </summary>
    [Flags]
    public enum AlphaNums
    {
        /// <summary>
        /// Only numeric digits.
        /// </summary>
        Numeric = 0x01,

        /// <summary>
        /// Lower case alpha characters.
        /// </summary>
        AlphaLower = 0x02,

        /// <summary>
        /// Upper case alpha characters.
        /// </summary>
        AlphaUpper = 0x04,

        /// <summary>
        /// Lower case alpha characters + numeric digits.
        /// </summary>
        AlphaNumericLower = Numeric | AlphaLower,

        /// <summary>
        /// Upper case alpha characters + numeric digits.
        /// </summary>
        AlphaNumericUpper = Numeric | AlphaUpper,

        /// <summary>
        /// Lower case and upper case alpha characters.
        /// </summary>
        Alpha = AlphaLower | AlphaUpper,

        /// <summary>
        /// Lower case and upper case alpha characters + numeric digits.
        /// </summary>
        AlphaNumeric = Alpha | Numeric
    }
}