using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumerals.Test
{


    [TestFixture]
    public class BaseTests
    {
        private const string Path = @"C:\Users\Czarek\Source\Repos\MPR1\RomanNumerals\RomanNumerals.Test\RomanNumerals.csv";
        public class TestData
        {
            public int Numbers { get; set; }
            public string Romans { get; set; }

            public static IEnumerable TestCases
            {
                get
                {
                    string inputLine;
                    using (FileStream inputStream = new FileStream(Path, FileMode.Open,FileAccess.Read))
                    {
                        StreamReader streamReader = new StreamReader(inputStream);

                        while ((inputLine = streamReader.ReadLine()) != null)
                        {
                            var data = inputLine.Split(',');
                            yield return new TestData
                            {
                                Numbers = Convert.ToInt32(data[0]),Romans = data[1]
                            };
                        }

                        streamReader.Close();
                        inputStream.Close();
                    }
                }
            }
        }
       

        [Test,TestCaseSource(typeof(TestData), "TestCases")]
        public void ShouldConvertNumberFromRoman(TestData data)
        {
            var result = Roman.FromRoman(data.Romans);
            Assert.AreEqual(data.Numbers, result);
        }

        [Test, TestCaseSource(typeof(TestData), "TestCases")]
        public void ShouldConvertNumberToRoman(TestData data)
        {
            var result = Roman.ToRoman(data.Numbers);
            Assert.AreEqual(data.Romans, result);
        }

        [Test, TestCaseSource(typeof(TestData), "TestCases")]
        public void SanityTestFromRoman(TestData data)
        {
            var result1 = Roman.ToRoman(data.Numbers);
            var result2 = Roman.FromRoman(result1);
            Assert.AreEqual(result2, data.Numbers);
        }

        [Test, TestCaseSource(typeof(TestData), "TestCases")]
        public void SanityTestToRoman(TestData data)
        {
            var result1 = Roman.FromRoman(data.Romans);
            var result2 = Roman.ToRoman(result1);
            Assert.AreEqual(result2, data.Romans);
        }

        [Test]
        public void OutOfRangeNumbersGreaterThanFourThousand([Range(4000,4500,50)] int number)
        {
            Assert.Throws<OutOfRangeError>(() => Roman.ToRoman(number));
        }

        [Test]
        public void OutOfRangeNumbersLessThanOne([Range(-1000, -500, 50)] int number)
        {
            Assert.Throws<OutOfRangeError>(() => Roman.ToRoman(number));
        }

        [Test]
        public void WrongFromRomanFormat()
        {
            Assert.Throws<ArgumentException>(() => Roman.FromRoman("HHFDH"));
        }

        [Test]
        public void NullFromRomanFormat()
        {
            Assert.Throws<ArgumentException>(() => Roman.FromRoman(""));
        }


        [TestCase("XXXX")]
        [TestCase("VV")]
        [TestCase("IIII")]
        [TestCase("LL")]
        [TestCase("CCCC")]
        public void TestTooManyRepeatedNumerals(string roman)
        {
            Assert.Throws<ArgumentException>(() => Roman.FromRoman(roman));
        }


        [TestCase("xiii", "XIII")]
        [TestCase("xxxiii", "XXXIII")]
        [TestCase("xlix", "XLIX")]
        [TestCase("l", "L")]
        [TestCase("mmmcccxxxiii", "MMMCCCXXXIII")]
        [TestCase("dlv", "DLV")]
        [TestCase("cmxcix", "CMXCIX")]
        public void ShouldBeUpperCaseAndParityTest(string lower, string upper)
        {
            var result1 = Roman.FromRoman(lower);
            var result2 = Roman.ToRoman(result1);
            Assert.AreEqual(result2, upper);
        }

       

    }
}
