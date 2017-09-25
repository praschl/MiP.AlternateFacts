using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MiP.AlternateFacts.Tests
{
    [TestClass]
    public class RandomizerTest : RandomTest
    {
        private readonly Randomizer _randomizer = new Randomizer();
        
        [TestMethod]
        public void Even_expected_values()
        {
            CanReturn(() => _randomizer.Even(-5, 5), -4, -2, 0, 2, 4);
        }

        [TestMethod]
        public void Even_unexpected_values()
        {
            NeverReturns(() => _randomizer.Even(-6, 6), -7, -5, -3, -1, 1, 3, 5, 7);
        }

        [TestMethod]
        public void Odd_expected_values()
        {
            CanReturn(() => _randomizer.Odd(-5, 5), -5, -3, -1, 1, 3, 5);
        }

        [TestMethod]
        public void Odd_unexpected_values()
        {
            NeverReturns(() => _randomizer.Odd(-5, 5), -6, -4, -2, 0, 2, 4, 6);
        }

        [TestMethod]
        public void Digit_expected_values()
        {
            CanReturn(() => _randomizer.Digit(0, 9), 0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
        }

        [TestMethod]
        public void Int32_range_is_inclusive()
        {
            CanReturn(() => _randomizer.Int32(0, 10), 0, 10);
        }

        [TestMethod]
        public void Int32_can_return_int_MaxValue_and_MinValue()
        {
            CanReturn(() => _randomizer.Int32(int.MaxValue - 10, int.MaxValue), int.MaxValue);
            CanReturn(() => _randomizer.Int32(int.MinValue, int.MinValue + 10), int.MinValue);
        }

        [TestMethod]
        public void UInt32_can_return_uint_MaxValue_and_MinValue()
        {
            CanReturn(() => _randomizer.UInt32(uint.MaxValue - 10, uint.MaxValue), uint.MaxValue);
            CanReturn(() => _randomizer.UInt32(uint.MinValue, uint.MinValue + 10), uint.MinValue);
        }

        [TestMethod]
        public void Int64_can_return_long_near_MaxValue_and_MinValue()
        {
            ReturnsChecked(() => _randomizer.Int64(long.MaxValue - 10, long.MaxValue), x => x >= long.MaxValue - 10);
            ReturnsChecked(() => _randomizer.Int64(long.MinValue, long.MinValue + 10), x => x <= long.MinValue + 10);
        }

        [TestMethod]
        public void UInt64_can_return_ulong_near_MaxValue_and_MinValue()
        {
            ReturnsChecked(() => _randomizer.UInt64(ulong.MaxValue - 10, ulong.MaxValue), x => x >= ulong.MaxValue - 10);
            ReturnsChecked(() => _randomizer.UInt64(ulong.MinValue, ulong.MinValue + 10), x => x <= ulong.MinValue + 10);
        }

        [TestMethod]
        public void Int16_can_return_short_MaxValue_and_MinValue()
        {
            CanReturn(() => _randomizer.Int16(short.MaxValue - 10, short.MaxValue), short.MaxValue);
            CanReturn(() => _randomizer.Int16(short.MinValue, short.MinValue + 10), short.MinValue);
        }

        [TestMethod]
        public void UInt16_can_return_ushort_MaxValue_and_MinValue()
        {
            CanReturn(() => _randomizer.UInt16(ushort.MaxValue - 10, ushort.MaxValue), ushort.MaxValue);
            CanReturn(() => _randomizer.UInt16(ushort.MinValue, ushort.MinValue + 10), ushort.MinValue);
        }

        [TestMethod]
        public void Double_range_is_inclusive()
        {
            const double min = 0.999999999999999d;
            const double max = 1d;

            CanReturn(() => _randomizer.Double(min, max), min);
            CanReturn(() => _randomizer.Double(min, max), max);
        }

        [TestMethod]
        public void Double_expected_values()
        {
            ReturnsChecked(() => _randomizer.Double(-100, 100), x => x >= -100 && x <= 100);
        }

        [TestMethod]
        public void Single_range_is_inclusive()
        {
            const float min = 0.9999999f;
            const float max = 1f;

            CanReturn(() => _randomizer.Single(min, max), min);
            CanReturn(() => _randomizer.Single(min, max), max);
        }

        [TestMethod]
        public void Single_expected_values()
        {
            ReturnsChecked(() => _randomizer.Single(-100, 100), x => x >= -100 && x <= 100);
        }

        [TestMethod]
        public void Decimal_range_is_inclusive()
        {
            const decimal min = 0.9999999999999999999999999999m;
            const decimal max = 1m;

            CanReturn(() => _randomizer.Decimal(min, max), min);
            CanReturn(() => _randomizer.Decimal(min, max), max);
        }
        
        [TestMethod]
        public void Decimal_expected_values()
        {
            ReturnsChecked(() => _randomizer.Decimal(-100, 100), x => x >= -100 && x <= 100);
        }

        [TestMethod]
        public void Byte_range_is_inclusive()
        {
            CanReturn(() => _randomizer.Byte(byte.MaxValue - 10, byte.MaxValue), byte.MaxValue);
            CanReturn(() => _randomizer.Byte(byte.MinValue, byte.MinValue + 10), byte.MinValue);
        }

        [TestMethod]
        public void Bytes_returns_correct_array_length()
        {
            var bytes = _randomizer.Bytes(7);
            Assert.AreEqual(7, bytes.Length);
        }

        [TestMethod]
        public void SByte_range_is_inclusive()
        {
            CanReturn(() => _randomizer.SByte(sbyte.MaxValue - 10, sbyte.MaxValue), sbyte.MaxValue);
            CanReturn(() => _randomizer.SByte(sbyte.MinValue, sbyte.MinValue + 10), sbyte.MinValue);
        }

        [TestMethod]
        public void Boolean_returns_true_and_false()
        {
            CanReturn(() => _randomizer.Boolean(), true);
            CanReturn(() => _randomizer.Boolean(), false);
        }

        [TestMethod]
        public void Guid_returns_Guid_different_from_Guid_Empty()
        {
            NeverReturns(() => _randomizer.Guid(), Guid.Empty);
        }

        [TestMethod]
        public void PickFrom_expected_values()
        {
            int[] list = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            CanReturn(() => _randomizer.PickFrom(list, 0, 9), 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        }

        [TestMethod]
        public void PickFrom_returns_first_and_last_character()
        {
            CanReturn(() => _randomizer.PickFrom(Chars.Numeric, 0, 10), Chars.Numeric.ToArray());
        }

        [TestMethod]
        public void PickEnum_returns_first_and_last_value()
        {
            CanReturn(() => _randomizer.PickEnum<PickEnum>(), 
                PickEnum.One, PickEnum.Two, PickEnum.Three, PickEnum.Four, PickEnum.Five, 
                PickEnum.Six, PickEnum.Seven, PickEnum.Eight, PickEnum.Nine, PickEnum.Ten);
        }

        [TestMethod]
        public void Shuffle_randomizes_list()
        {
            int[] list = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            var shuffled = _randomizer.Collections.Shuffle(list).ToArray();
            CollectionAssert.AreEquivalent(list, shuffled);
            CollectionAssert.AreNotEqual(list, shuffled);
        }

        private enum PickEnum
        {
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten
        }
    }
}