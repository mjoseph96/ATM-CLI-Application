using ATMCLIApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ATMCLIUnitTests
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InValidPin_Should_ReturnFalse()
        {
            var invalidCredentials = "12345678 1234 1233";

            var valid = ATMService.ValidPin(invalidCredentials);

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void ValidPin_Should_ReturnTrue()
        {
            var invalidCredentials = "12345678 1234 1234";

            var valid = ATMService.ValidPin(invalidCredentials);

            Assert.IsTrue(valid);            
        }

        [TestMethod]
        public void GetTransactions_Should_ReturnTrue()
        {
            string[] testLines ={"8000",
                            "\r",
                            "12345678 1234 1234",  
                            "500 100", 
                            "B", 
                            "W 100",
                            "\r",
                            "87654321 4321 4321",
                            "100 0",
                            "W 10"};
            var transactions = ATMService.GetTransactions(testLines);

            Assert.AreEqual(transactions.Count(), 2);
        }

        [TestMethod]
        public void GetTransactions_Should_ReturnFalse()
        {
            string[] testLines ={"8000",
                            "\r",
                            "12345678 1234 1234",
                            "500 100",
                            "B",
                            "W 100"};
            var transactions = ATMService.GetTransactions(testLines);

            Assert.IsFalse(transactions.Count() == 2);
        }
    }
}
