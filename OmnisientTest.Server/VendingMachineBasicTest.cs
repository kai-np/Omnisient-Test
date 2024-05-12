using System.Collections.Generic;
using Xunit;

namespace OmnisientTest.Server.Tests
{
    public class VendingMachineTests
    {
        [Fact]
        public void CalculateOptimalChangeReturnedWithGivenInput()
        {
      
            List<int> coinDenominations = new List<int> { 1, 5, 10, 25 };
            VendingMachine vendingMachine = new VendingMachine(coinDenominations);
            double purchaseAmount = 1.35;
            double tenderAmount = 2.00;
            List<int> expectedChange = new List<int> { 25, 25, 10, 5 };
            List<int> actualChange = vendingMachine.CalculateChange(purchaseAmount, tenderAmount);
            Assert.Equal(expectedChange, actualChange);
        }
    }
}
