using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LTransDeal_v1;

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
    }
}
