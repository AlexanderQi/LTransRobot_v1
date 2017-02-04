using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LTransDeal_v1;
using System.Text.RegularExpressions;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            LTransDeal.appid = "2015063000000001";
            LTransDeal.key = "12345678";
            LTransDeal.salt = "1435660288";
            string test_sign = "f89f9594663708c1605f3d736d01d2d4";
            string str = LTransDeal.getSign("apple");
            Assert.AreEqual(test_sign, str, true);
            Console.WriteLine(test_sign);
            Console.WriteLine(str);
        }

        [TestMethod]
        public void TestMethod2()
        {

            string str = LTransDeal.getTransResult("even the most well regarded writing since then has sought to capture spoken English on the page.");
            Console.WriteLine(LTransDeal.url);
            Console.WriteLine(str);
        }

        [TestMethod]
        public void TestMethod3()
        {

            //string str = "123.123";
            //string str2 = "123,123";
            //string str3 = "123,123.456";
            //string str4 = "123.123.456.123";
            //string str5 = "fffaa";

            //Regex reg = new Regex(@"\d*\.,");
            //Match ma = reg.Match(str);
            //if(ma.Success)
            //    Console.WriteLine(ma.)

            // Define a regular expression for repeated words.
            Regex rx = new Regex(@"\b(?<word>\w+)\s+(\k<word>)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Define a test string.        
            string text = "The the quick brown fox  fox jumped over the lazy dog dog.";

            // Find matches.
            MatchCollection matches = rx.Matches(text);

            // Report the number of matches found.
            Console.WriteLine("{0} matches found.", matches.Count);

            // Report on each match.
            foreach (Match match in matches)
            {
                string word = match.Groups["word"].Value;
                int index = match.Index;
                Console.WriteLine("{0} repeated at position {1}", word, index);
            }
        }
    }
}
