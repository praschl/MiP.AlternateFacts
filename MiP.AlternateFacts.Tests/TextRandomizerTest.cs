using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MiP.AlternateFacts.Tests
{
    [TestClass]
    public class TextRandomizerTest : RandomTest
    {
        private TextRandomizer _randomizer;

        [TestInitialize]
        public void Initialize()
        {
            _randomizer = new TextRandomizer();
        }

        [TestMethod]
        public void Char_returns_random_elements()
        {
            CanReturn(() => _randomizer.Char('a', 'd'), 'a');
            CanReturn(() => _randomizer.Char('a', 'd'), 'b');
            CanReturn(() => _randomizer.Char('a', 'd'), 'c');
            CanReturn(() => _randomizer.Char('a', 'd'), 'd');
        }

        [TestMethod]
        public void AlphaNumeric_Numeric()
        {
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.Numeric), '0');
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.Numeric), '9');

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
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.AlphaNumeric), '0');
            CanReturn(() => _randomizer.AlphaNumeric(AlphaNums.AlphaNumeric), 'z');
        }

        [TestMethod]
        public void StringFromTemplate_returns_suitable_Numeric()
        {
            var fromTemplate = _randomizer.StringFromTemplate(new string('#', 100));
            ContainsNothingBut(fromTemplate, "0123456789");
        }

        [TestMethod]
        public void StringFromTemplate_returns_suitable_Alpha()
        {
            var fromTemplate = _randomizer.StringFromTemplate(new string('?', 100));
            ContainsNothingBut(fromTemplate, "abcdefghijklmnopqrstuvwzyxABCDEFGHIJKLMNOPQRSTUVWZYX");
        }

        [TestMethod]
        public void StringFromTemplate_returns_suitable_AlphaNumeric()
        {
            var fromTemplate = _randomizer.StringFromTemplate(new string('*', 100));
            ContainsNothingBut(fromTemplate, "0123456789abcdefghijklmnopqrstuvwzyxABCDEFGHIJKLMNOPQRSTUVWZYX");
        }

        [TestMethod]
        public void StringFromTemplate_returns_suitable_AlphaNumericUpper()
        {
            var fromTemplate = _randomizer.StringFromTemplate(new string('N', 100));
            ContainsNothingBut(fromTemplate, "0123456789ABCDEFGHIJKLMNOPQRSTUVWZYX");
        }

        [TestMethod]
        public void StringFromTemplate_returns_suitable_AlphaNumericLower()
        {
            var fromTemplate = _randomizer.StringFromTemplate(new string('n', 100));
            ContainsNothingBut(fromTemplate, "0123456789abcdefghijklmnopqrstuvwzyx");
        }

        [TestMethod]
        public void StringFromTemplate_returns_suitable_AlphaUpper()
        {
            var fromTemplate = _randomizer.StringFromTemplate(new string('A', 100));
            ContainsNothingBut(fromTemplate, "ABCDEFGHIJKLMNOPQRSTUVWZYX");
        }

        [TestMethod]
        public void StringFromTemplate_returns_suitable_AlphaLower()
        {
            var fromTemplate = _randomizer.StringFromTemplate(new string('a', 100));
            ContainsNothingBut(fromTemplate, "abcdefghijklmnopqrstuvwzyx");
        }

        [TestMethod]
        public void StringFromTemplate_does_not_replace_removed_chars()
        {
            var settings = new StringTemplateSettings()
                .DoNotReplace('#')
                .DoNotReplace('?')
                .DoNotReplace('*')
                .DoNotReplace('a')
                .DoNotReplace('A')
                .DoNotReplace('n')
                .DoNotReplace('N');

            const string template = "#?*aAnN";
            var fromTemplate = _randomizer.StringFromTemplate(template, settings);

            Assert.AreEqual(template, fromTemplate);
        }

        [TestMethod]
        public void StringFromTemplate_replaces_other_chars()
        {
            var settings = new StringTemplateSettings(false)
                .Replace('.', AlphaNums.Alpha)
                .Replace('0', AlphaNums.Numeric);

            var fromTemplate = _randomizer.StringFromTemplate("...,000,Hello", settings);

            var parts = fromTemplate.Split(',');
            ContainsNothingBut(parts[0], "abcdefghijklmnopqrstuvwzyxABCDEFGHIJKLMNOPQRSTUVWZYX");
            ContainsNothingBut(parts[1], "0123456789");
            Assert.AreEqual("Hello", parts[2]);
        }

        [TestMethod]
        public void StringFromTemplates_calls_defined_functions()
        {
            var settings = new StringTemplateSettings(false)
                .Replace('0', () => "Aaa")
                .Replace('1', () => "Bbb");

            var fromTemplate = _randomizer.StringFromTemplate("00,11", settings);

            Assert.AreEqual("AaaAaa,BbbBbb", fromTemplate);
        }

        private static void ContainsNothingBut(string fromTemplate, string allowedChars)
        {
            var wrongChars = fromTemplate.Where(ch => !allowedChars.Contains(ch)).ToArray();

            if (wrongChars.Length > 0)
                throw new AssertFailedException($"Unexpected characters returned: [{string.Join(",", wrongChars)}].");
        }
    }
}