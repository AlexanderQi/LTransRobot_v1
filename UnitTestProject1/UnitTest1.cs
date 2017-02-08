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

            string str = "123.123";
            string str2 = "123,123";
            string str3 = "+123,121,123.456";
            string str4 = "+55.88%";
            string str5 = "-66.11%";
            string str6 = "-123asdss";

            Regex reg = new Regex(@"^([+-]|\b)(\d*,)*\d+\.*\d*%?$");
            Match ma = reg.Match(str);
            if (ma.Success)
                Console.WriteLine(ma.Value);
            ma = reg.Match(str2);
            if (ma.Success)
                Console.WriteLine(ma.Value);
            ma = reg.Match(str3);
            if (ma.Success)
                Console.WriteLine(ma.Value);
            ma = reg.Match(str4);
            if (ma.Success)
                Console.WriteLine(ma.Value);
            ma = reg.Match(str5);
            if (ma.Success)
                Console.WriteLine(ma.Value);
            ma = reg.Match(str6);
            if (ma.Success)
                Console.WriteLine(ma.Value);

        }
    }
}
