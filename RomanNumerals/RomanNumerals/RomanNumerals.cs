using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    public class Roman
    {
        public static int CheckDecimal(int dec, int lastNumber, int lastDecimal)
        {
            if (lastNumber > dec)
            {
                return lastDecimal - dec;
            }
            else
            {
                return lastDecimal + dec;
            }
        }
        public static int FromRoman(string RomanNumeral)
        {
            int notationValue = 0;
            int secondLetterValue = 0;
            int tooMany = 0;
            char thisChar, pastChar;

            SortedDictionary<char, int> RomanDictionary = new SortedDictionary<char, int>
            {
                { 'M', 1000 },
                { 'D', 500 },
                { 'C', 100 },
                { 'L', 50 },
                { 'X', 10 },
                { 'V', 5 },
                { 'I', 1 },
                { ' ', 0 }
            };

            try
            {

                RomanNumeral = RomanNumeral.ToUpper().TrimEnd(' ').TrimStart(' ');
                if (RomanNumeral.Contains(" ") || string.IsNullOrWhiteSpace(RomanNumeral))
                    throw new ArgumentException();

                for (int x = 0; x < RomanNumeral.Length; x++)
                {
                    thisChar = RomanNumeral[x];
                    pastChar = x > 0 ? RomanNumeral[x - 1] : ' ';
                    tooMany = (thisChar == pastChar) ? tooMany + 1 : 1;
                    if (RomanDictionary.TryGetValue(thisChar, out notationValue) && RomanDictionary.TryGetValue(pastChar, out secondLetterValue))
                    {
                        int subNumber = (notationValue.ToString()[0] == '1') ? (notationValue / 10) : (notationValue / 5);
                        if (tooMany > 1 && notationValue.ToString()[0] == '5')
                            throw new ArgumentException();
                        if (tooMany > 3)
                            throw new ArgumentException();
                        if (subNumber > 0 && secondLetterValue > 0 && secondLetterValue != subNumber && secondLetterValue < notationValue)
                            throw new ArgumentException();
                    }
                    else
                    {
                        throw new ArgumentException();
                    }

                }
                int dec = 0;
                int Number = 0;
                for (int x = RomanNumeral.Length - 1; x >= 0; x--)
                {
                    switch (RomanNumeral[x])
                    {
                        case 'M':
                            dec = CheckDecimal(1000, Number, dec);
                            Number = 1000;
                            break;

                        case 'D':
                            dec = CheckDecimal(500, Number, dec);
                            Number = 500;
                            break;

                        case 'C':
                            dec = CheckDecimal(100, Number, dec);
                            Number = 100;
                            break;

                        case 'L':
                            dec = CheckDecimal(50, Number, dec);
                            Number = 50;
                            break;

                        case 'X':
                            dec = CheckDecimal(10, Number, dec);
                            Number = 10;
                            break;

                        case 'V':
                            dec = CheckDecimal(5, Number, dec);
                            Number = 5;
                            break;

                        case 'I':
                            dec = CheckDecimal(1, Number, dec);
                            Number = 1;
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
                return dec;
            }
            catch { throw new ArgumentException(); }
        }

        public static string ToRoman(int num)
        {
            if (num > 3999 || num <= 0)
                throw new OutOfRangeError();
            if (num == null)
                throw new ArgumentNullException();

            string Roman = "";
            while (num != 0)
            {
                if (num >= 1000)
                {
                    Roman += "M";
                    num -= 1000;
                }
                else if (num >= 500)
                {
                    if (num < 900)
                    {
                        Roman += "D";
                        num -= 500;
                    }
                    else
                    {
                        Roman += "CM";
                        num %= 100;
                    }

                }
                else if (num >= 100)
                {
                    if (num < 400)
                    {
                        Roman += "C";
                        num -= 100;
                    }
                    else
                    {
                        Roman += "CD";
                        num %= 100;
                    }
                }
                else if (num >= 50)
                {
                    if (num < 90)
                    {
                        Roman += "L";
                        num -= 50;
                    }
                    else
                    {
                        Roman += "XC";
                        num %= 10;
                    }
                }
                else if (num >= 10)
                {
                    if (num < 40)
                    {
                        Roman += "X";
                        num -= 10;
                    }
                    else
                    {
                        Roman += "XL";
                        num %= 10;
                    }
                }
                else if (num >= 5)
                {
                    if (num < 9)
                    {
                        Roman += "V";
                        num -= 5;
                    }
                    else
                    {
                        Roman += "IX";
                        num = 0;
                    }
                }
                else if (num >= 1)
                {
                    if (num < 4)
                    {
                        Roman += "I";
                        num -= 1;
                    }
                    else
                    {
                        Roman += "IV";
                        num = 0;
                    }
                }
            }

            return Roman;
        }

        public static void ToRoman(Action negativeNumbers)
        {
            throw new NotImplementedException();
        }
    }

}
