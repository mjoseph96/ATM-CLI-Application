using ATMCLIApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATMCLIApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = System.IO.File.ReadAllText(@"..\..\..\InputData.txt");
            var inputLines = text.Split("\n");
            var ATMBalance = int.Parse(inputLines[0]);
            var transactions = ATMService.GetTransactions(inputLines);

            foreach (var transaction in transactions)
            {
                var credentials = transaction[0].Replace("\r", "");
                if (ATMService.ValidPin(credentials))
                {
                    var balanceLine = transaction[1].Replace("\r", "");
                    var currentBalance = int.Parse(balanceLine.Substring(0, balanceLine.IndexOf(" ")));
                    var overdraft = int.Parse(balanceLine[(balanceLine.IndexOf(" ") + 1)..]);
                    for (int i = 2; i < transaction.Count(); i++)
                    {
                        var operation = transaction[i][0];
                        switch (operation)
                        {
                            case 'B':
                                Console.WriteLine(currentBalance);
                                break;
                            case 'W':
                                var withdrawAmount = int.Parse(transaction[i][(transaction[i].IndexOf(" ") + 1)..]);
                                if (withdrawAmount > (currentBalance + overdraft))
                                {
                                    Console.WriteLine("FUNDS_ERR");
                                }
                                else if (withdrawAmount > ATMBalance)
                                {
                                    Console.WriteLine("ATM_ERR");
                                }
                                else
                                {
                                    Console.WriteLine((withdrawAmount <= currentBalance ? (currentBalance - withdrawAmount) : withdrawAmount - (currentBalance + overdraft)));
                                    ATMBalance -= withdrawAmount;
                                }
                                break;
                        }

                    }
                }
                else
                {
                    Console.WriteLine("ACCOUNT_ERR");
                }

            }
        }
    }
}
