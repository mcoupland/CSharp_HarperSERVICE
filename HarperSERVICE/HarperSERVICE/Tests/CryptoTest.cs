using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CryptoTest
    {
        public CryptoTest() { }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestHash2()
        {
            string cleartext = "~all the stuff and more!";

            string hashedvalue = HarperCRYPTO.Cryptography.Hash(cleartext);
            string dehashedvalue = HarperCRYPTO.Cryptography.DeHash(hashedvalue);

            string hashedvalue2 = HarperCRYPTO.Cryptography.Hash2(cleartext);
            string dehashedvalue2 = HarperCRYPTO.Cryptography.DeHash2(hashedvalue2);


        }
    }
}
