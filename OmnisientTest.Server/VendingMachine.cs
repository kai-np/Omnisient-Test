using System;
using System.Collections.Generic;
using System.Linq;

namespace OmnisientTest.Server
{
    // Vending machine class to find the optimal ammount of change as per problem statement
    public class VendingMachine
    {
        private List<int> coinDenominations;

        // Order/sort coin denominations in descending order
        public VendingMachine(List<int> coinDenominations)
        {
            this.coinDenominations = coinDenominations.OrderByDescending(d => d).ToList(); 
        }

        // Core function to calculate optimal change
        public List<int> CalculateChange(double purchaseAmount, double tenderAmount)
        {

            // Calculates the total change amount in cents
            int changeAmount = (int)Math.Round((tenderAmount - purchaseAmount) * 100);

            List<int> change = new List<int>();

            // Loops through the sorted denominations to find the optimal change
            foreach (int denomination in coinDenominations)
            {
                // Adds the highest possible denomination to the change until not possible
                while (changeAmount >= denomination)
                {
                    change.Add(denomination);
                    changeAmount -= denomination;
                }
            }

            return change;
        }
    }
}
