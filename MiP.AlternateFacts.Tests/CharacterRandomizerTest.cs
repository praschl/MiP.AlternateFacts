using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MiP.AlternateFacts.Tests
{
    [TestClass]
    public class CharacterRandomizerTest : RandomTest
    {
        private CharacterRandomizer _randomizer;

        [TestInitialize]
        public void Initialize()
        {
            _randomizer = new CharacterRandomizer();
        }

        [TestMethod]
        public void Char_returns_random_elements()
        {
            CanReturn(()=>_randomizer.Char('a','d'),'a');
            CanReturn(()=>_randomizer.Char('a','d'),'b');
            CanReturn(()=>_randomizer.Char('a','d'),'c');
            CanReturn(()=>_randomizer.Char('a','d'),'d');
        }

        [TestMethod]
        public void AlphaNumeric_Numeric()
        {
            CanReturn(()=>_randomizer.AlphaNumeric(AlphaNums.Numeric), '0');
            CanReturn(()=>_randomizer.AlphaNumeric(AlphaNums.Numeric), '9');

            NeverReturns(() => _randomizer.AlphaNumeric(AlphaNums.Numeric), 'a');
            NeverReturns(() => _randomizer.AlphaNumeric(AlphaNums.Numeric), 'A');
        }

        [TestMethod]
        public void AlphaNumeric_NumericUpper()
        {
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.AlphaNumericUpper), '0');
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.AlphaNumericUpper), 'Z');

            NeverReturns(() => _randomizer.AlphaNumeric(AlphaNums.AlphaNumericUpper), 'a');
        }

        [TestMethod]
        public void AlphaNumeric_AlphaLower()
        {
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.AlphaLower), 'a');
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.AlphaLower), 'z');

            NeverReturns(() => _randomizer.AlphaNumeric(AlphaNums.AlphaLower), 'Z');
            NeverReturns(() => _randomizer.AlphaNumeric(AlphaNums.AlphaLower), '9');
        }

        [TestMethod]
        public void AlphaNumeric_AlphaUpper()
        {
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.AlphaUpper), 'A');
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.AlphaUpper), 'Z');

            NeverReturns(() => _randomizer.AlphaNumeric(AlphaNums.AlphaUpper), 'a');
            NeverReturns(() => _randomizer.AlphaNumeric(AlphaNums.AlphaUpper), '9');
        }

        [TestMethod]
        public void AlphaNumeric_Alpha()
        {
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.Alpha), 'A');
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.Alpha), 'z');

            NeverReturns(() => _randomizer.AlphaNumeric(AlphaNums.Alpha), '9');
        }

        [TestMethod]
        public void AlphaNumeric_All()
        {
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.All), '0');
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.All), 'z');
        }
    }
}