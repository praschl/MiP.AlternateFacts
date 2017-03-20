using System;
using System.Collections.Generic;
using System.Linq;

namespace MiP.AlternateFacts
{
    /// <summary>
    /// Generates basic random data.
    /// AlphaNumeric min and max parameters for the methods <see cref="Number"/>, <see cref="Even"/> and <see cref="Odd"/> 
    /// are inclusive values, meaning min and max are values which can be returned,
    /// except for the case when max is equal to <see cref="int.MaxValue"/>, in which case <see cref="int.MaxValue"/>-1
    /// is the highest possible value which can be randomized.
    /// </summary>
    /// <remarks>
    /// A bit inspired by https://github.com/bchavez/Bogus
    /// </remarks>
    public class Randomizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Randomizer"/> class with a random seed.
        /// </summary>
        public Randomizer()
        {
            Random = new Random();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Randomizer"/> class, using the <paramref name="seed"/>.
        /// Passing the same seed will generate reproducable random numbers.
        /// </summary>
        /// <param name="seed">Used to initialize the <see cref="System.Random"/> class.</param>
        public Randomizer(int seed)
        {
            Random = new Random(seed);
        }

        // integer numbers

        internal Random Random { get; }

        /// <summary>
        /// Gets a random <see cref="int"/> between <paramref name="min"/> (inclusive lower bound) 
        /// and <paramref name="max"/> (inclusive upper bound).
        /// </summary>
        /// <remarks>
        /// This method can never return <see cref="int.MaxValue"/>.
        /// Use <see cref="Int32"/> to get a number which can also be <see cref="int.MaxValue"/>.
        /// </remarks>
        /// <param name="min">Lower inclusive bound of the number returned.</param>
        /// <param name="max">Upper inclusive bound of the number returned.</param>
        public int Number(int min = 0, int max = 1)
        {
            // make max a possible result, except when it is equal to int.MaxValue
            if (max < int.MaxValue)
                max = max + 1;

            var number = Random.Next(min, max);

            return number;
        }

        /// <summary>
        /// Generates a random even <see cref="int"/> between <paramref name="min"/> (inclusive lower bound) 
        /// and <paramref name="max"/> (inclusive upper bound).
        /// </summary>
        /// <remarks>
        /// This method can never return <see cref="int.MaxValue"/>.
        /// Use <see cref="Int32"/> to get a number which can also be <see cref="int.MaxValue"/>.
        /// </remarks>        /// <param name="min">Lower inclusive bound of the number returned.</param>
        /// <param name="max">Upper inclusive bound of the number returned.</param>
        public int Even(int min = 0, int max = 1)
        {
            var result = Number(min, max);

            if (result%2 == 0)
                return result;

            if (result == min)
                return result + 1;

            return result - 1;
        }

        /// <summary>
        /// Generates a random odd <see cref="int"/> between <paramref name="min"/> (inclusive lower bound) 
        /// and <paramref name="max"/> (inclusive upper bound).
        /// </summary>
        /// <remarks>
        /// This method can never return <see cref="int.MaxValue"/>.
        /// Use <see cref="Int32"/> to get a number which can also be <see cref="int.MaxValue"/>.
        /// </remarks>        /// <param name="min">Lower inclusive bound of the number returned.</param>
        /// <param name="max">Upper inclusive bound of the number returned.</param>
        public int Odd(int min = 0, int max = 1)
        {
            var result = Number(min, max);

            if (result%2 == 1)
                return result;

            if (result == min)
                return result + 1;

            return result - 1;
        }

        /// <summary>
        /// Gets a random digit between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">Lowest digit to return (inclusive lower bound).</param>
        /// <param name="max">Highest digit to return (inclusive upper bound).</param>
        public int Digit(int min = 0, int max = 9)
        {
            if (min < 0 || min > 9) throw new ArgumentOutOfRangeException(nameof(min), "min must be between 0 and 9 (inclusive).");
            if (max < 0 || max > 9) throw new ArgumentOutOfRangeException(nameof(max), "max must be between 0 and 9 (inclusive).");

            return Number(min, max);
        }

        /// <summary>
        /// Generates a random <see cref="int"/> between <paramref name="min"/> (inclusive lower bound) 
        /// and <paramref name="max"/> (inclusive upper bound).
        /// </summary>
        /// <remarks>
        /// This method can return <see cref="int.MaxValue"/>, but is about 6 times slower when <paramref name="max"/> equal to <see cref="int.MaxValue"/>.
        /// If <paramref name="max"/> is lower than <see cref="int.MaxValue"/>, it will just fall back to the faster <see cref="Number"/> method.
        /// </remarks>
        /// <param name="min">Lowest <see cref="int"/> to return (inclusive lower bound).</param>
        /// <param name="max">Highest <see cref="int"/> to return (inclusive upper bound).</param>
        public int Int32(int min = int.MinValue, int max = int.MaxValue)
        {
            if (max < int.MaxValue)
                return Number(min, max);

            // slower part is about 6 times slower, but only very rarely used
            return (int) Math.Round(Math.Abs(Double()*(max - min)) + min);
        }

        /// <summary>
        /// Generates a random <see cref="uint"/> between <paramref name="min"/> (inclusive lower bound) 
        /// and <paramref name="max"/> (inclusive upper bound).
        /// </summary>
        /// <param name="min">Lowest <see cref="uint"/> to return (inclusive lower bound).</param>
        /// <param name="max">Highest <see cref="uint"/> to return (inclusive upper bound).</param>
        public uint UInt32(uint min = uint.MinValue, uint max = uint.MaxValue)
        {
            return (uint) Math.Round(Double()*(max - min) + min);
        }

        /// <summary>
        /// Generates a random <see cref="long"/> between <paramref name="min"/> (inclusive lower bound) 
        /// and <paramref name="max"/> (inclusive upper bound).
        /// </summary>
        /// <param name="min">Lowest <see cref="long"/> to return (inclusive lower bound).</param>
        /// <param name="max">Highest <see cref="long"/> to return (inclusive upper bound).</param>
        public long Int64(long min = long.MinValue, long max = long.MaxValue)
        {
            return (long) Math.Round(Math.Abs(Double()*(max - min)) + min);
        }

        /// <summary>
        /// Generates a random <see cref="ulong"/> between <paramref name="min"/> (inclusive lower bound) 
        /// and <paramref name="max"/> (inclusive upper bound).
        /// </summary>
        /// <param name="min">Lowest <see cref="ulong"/> to return (inclusive lower bound).</param>
        /// <param name="max">Highest <see cref="ulong"/> to return (inclusive upper bound).</param>
        public ulong UInt64(ulong min = ulong.MinValue, ulong max = ulong.MaxValue)
        {
            return (ulong) Math.Round(Double()*(max - min) + min);
        }

        /// <summary>
        /// Generates a random <see cref="short"/> between <paramref name="min"/> (inclusive lower bound) 
        /// and <paramref name="max"/> (inclusive upper bound).
        /// </summary>
        /// <param name="min">Lowest <see cref="short"/> to return (inclusive lower bound).</param>
        /// <param name="max">Highest <see cref="short"/> to return (inclusive upper bound).</param>
        public short Int16(short min = short.MinValue, short max = short.MaxValue)
        {
            return (short) Number(min, max);
        }

        /// <summary>
        /// Generates a random <see cref="ushort"/> between <paramref name="min"/> (inclusive lower bound) 
        /// and <paramref name="max"/> (inclusive upper bound).
        /// </summary>
        /// <param name="min">Lowest <see cref="ushort"/> to return (inclusive lower bound).</param>
        /// <param name="max">Highest <see cref="ushort"/> to return (inclusive upper bound).</param>
        public ushort UInt16(ushort min = ushort.MinValue, ushort max = ushort.MaxValue)
        {
            return (ushort) Number(min, max);
        }

        // floating point numbers

        /// <summary>
        /// Gets a random <see cref="double"/> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">Lowest <see cref="double"/> to return (inclusive lower bound).</param>
        /// <param name="max">Highest <see cref="double"/> to return (inclusive upper bound).</param>
        public double Double(double min = 0.0, double max = 1.0d)
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator --  will work exactiyl for these cases.
            if (min == 0.0d && max == 1.0d)
                return Random.NextDouble();
            // ReSharper restore CompareOfFloatsByEqualityOperator

            return Random.NextDouble()*(max - min) + min;
        }

        /// <summary>
        /// Gets a random <see cref="float"/> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">Lowest <see cref="float"/> to return (inclusive lower bound).</param>
        /// <param name="max">Highest <see cref="float"/> to return (inclusive upper bound).</param>
        public float Single(float min = 0.0f, float max = 1.0f)
        {
            return (float) Double()*(max - min) + min;
        }

        /// <summary>
        /// Gets a random <see cref="decimal"/> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">Lowest <see cref="decimal"/> to return (inclusive lower bound).</param>
        /// <param name="max">Highest <see cref="decimal"/> to return (inclusive upper bound).</param>
        public decimal Decimal(decimal min = 0.0m, decimal max = 1.0m)
        {
            return (decimal) Double()*(max - min) + min;
        }

        /// <summary>
        /// Gets a random <see cref="byte"/> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">Lowest <see cref="byte"/> to return (inclusive lower bound).</param>
        /// <param name="max">Highest <see cref="byte"/> to return (inclusive upper bound).</param>
        public byte Byte(byte min = byte.MinValue, byte max = byte.MaxValue)
        {
            return (byte) Number(min, max);
        }

        /// <summary>
        /// Gets a number <paramref name="count"/> of random <see cref="byte"/>s.
        /// </summary>
        /// <param name="count">The number of <see cref="byte"/>s to get.</param>
        public byte[] Bytes(int count)
        {
            var result = new byte[count];
            Random.NextBytes(result);
            return result;
        }

        /// <summary>
        /// Gets a random <see cref="sbyte"/> between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">Lowest <see cref="sbyte"/> to return (inclusive lower bound).</param>
        /// <param name="max">Highest <see cref="sbyte"/> to return (inclusive upper bound).</param>
        public sbyte SByte(sbyte min = sbyte.MinValue, sbyte max = sbyte.MaxValue)
        {
            return (sbyte) Number(min, max);
        }

        // other

        /// <summary>
        /// Randomly returns true or false.
        /// </summary>
        public bool Boolean()
        {
            return Number() == 0;
        }

        /// <summary>
        /// Gets a random <see cref="Guid"/>.
        /// </summary>
        public Guid Guid()
        {
            return System.Guid.NewGuid();
        }

        // other

        /// <summary>
        /// Picks a random item from the given <paramref name="items"/> list.
        /// </summary>
        /// <param name="items">List of items to choose from.</param>
        /// <param name="minIndex">Minimum index of returned item, must not be after the last.</param>
        /// <param name="maxIndex">Maximum index of returned item, if greater than the last item, it is set to the index of the last item.</param>
        public T PickFrom<T>(IReadOnlyList<T> items, int minIndex = 0, int maxIndex = int.MaxValue)
        {
            if (items.Count == 0) throw new ArgumentException("List of items must not be empty.", nameof(items));
            if (maxIndex >= items.Count) maxIndex = items.Count - 1;
            if (minIndex > maxIndex) throw new ArgumentOutOfRangeException(nameof(minIndex), "minIndex must not be equal to or greater than the number of items in the list.");

            var index = Number(minIndex, maxIndex);
            return items[index];
        }

        /// <summary>
        /// Picks a random char from the given <paramref name="chars"/> string.
        /// </summary>
        /// <param name="chars">String to chose a character from.</param>
        /// <param name="minIndex">Minimum index of returned char, must not be after the last.</param>
        /// <param name="maxIndex">Maximum index of returned char, if greater than the index of the last char, it is set to the index of the last char.</param>
        public char PickFrom(string chars, int minIndex = 0, int maxIndex = int.MaxValue)
        {
            if (string.IsNullOrEmpty(chars)) throw new ArgumentException("String of items must not be null or empty.", nameof(chars));
            if (maxIndex > chars.Length) maxIndex = chars.Length - 1;
            if (minIndex > maxIndex) throw new ArgumentOutOfRangeException(nameof(minIndex), "minIndex must not be equal to or greater than the length of the string.");

            var index = Number(minIndex, maxIndex);
            return chars[index];
        }

        /// <summary>
        /// Picks a value from an enum allowing to exclude some.
        /// </summary>
        /// <typeparam name="T">Type of the enum to pick a value from.</typeparam>
        /// <param name="excluded">A list of values of the enum, which should not be returned.</param>
        public T PickEnum<T>(params T[] excluded) where T : struct
        {
            var enumType = typeof(T);
            if (!enumType.IsEnum)
                throw new ArgumentException("T must be an Enum.", nameof(excluded));

            var availableNames = Enum.GetNames(enumType);

            var excludedNames = excluded.Select(name => Enum.GetName(enumType, name));
            availableNames = availableNames.Except(excludedNames).ToArray();

            if (!availableNames.Any())
                throw new ArgumentException("AlphaNumeric values of the enum are excluded, and none are left to return.", nameof(excluded));

            var chosenName = PickFrom(availableNames);

            return (T) Enum.Parse(typeof(T), chosenName);
        }

        /// <summary>
        /// Shuffles an IEnumerable source.
        /// </summary>
        public IEnumerable<T> Shuffle<T>(IEnumerable<T> source)
        {
            return source.OrderBy(_ => Random.Next());
        }
    }
}