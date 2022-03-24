using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATMCLIApp.Services
{
    public class ATMService 
    {
        public static List<List<string>> GetTransactions(string[] inputLines)
        {
            var stringList = inputLines.Skip(2).ToList();
            var transactions = new List<List<string>>();
            var transaction = new List<string>();
            foreach (var line in stringList)
            {
                if (line != "\r")
                {
                    transaction.Add(line);
                }
                else
                {
                    transactions.Add(transaction);
                    transaction = new List<string>();
                }

                if (line == stringList.Last())
                {
                    transactions.Add(transaction);
                }
            }
            return transactions;
        }

        public static bool ValidPin(string credentials)
        {
            var customerActualPin = credentials.Substring(credentials.IndexOf(" ") + 1, 4);
            var enteredPin = credentials.Substring(credentials.LastIndexOf(" ") + 1, 4);
            return customerActualPin == enteredPin;
        }

    }
}
