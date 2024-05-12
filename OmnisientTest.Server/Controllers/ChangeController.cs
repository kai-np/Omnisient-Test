using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace OmnisientTest.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChangeController : ControllerBase
    {
        private readonly VendingMachine vendingMachine;

        public ChangeController()
        {
            // Initialize VendingMachine with default coin denominations
            List<int> defaultCoinDenominations = new List<int> { 1, 5, 10, 25 }; // US Dollar
            vendingMachine = new VendingMachine(defaultCoinDenominations);
        }

        [HttpGet("calculate-change")]
        public IActionResult CalculateChange(double purchaseAmount, double tenderAmount)
        {
            List<int> change = vendingMachine.CalculateChange(purchaseAmount, tenderAmount);
            return Ok(change);
        }

        [HttpGet("calculate-change-with-currency")]
        public IActionResult CalculateChangeWithCurrency(double purchaseAmount, double tenderAmount, string currencyCode)
        {
            // Map currencyCode to corresponding coin denominations
            List<int> coinDenominations = GetCoinDenominationsForCurrency(currencyCode);
            VendingMachine currencyVendingMachine = new VendingMachine(coinDenominations);

            List<int> change = currencyVendingMachine.CalculateChange(purchaseAmount, tenderAmount);
            return Ok(change);
        }


        // Function to map currency code to corresponding coin denominations
        private List<int> GetCoinDenominationsForCurrency(string currencyCode)
        {
            switch (currencyCode.ToUpper())
            {
                case "USD":
                    return new List<int> { 1, 5, 10, 25 }; // US Dollar
                case "GBP":
                    return new List<int> { 1, 2, 5, 10, 20, 50 }; // British Pound
                default:
                    throw new ArgumentException("Invalid currency code");
            }
        }
    }
}
