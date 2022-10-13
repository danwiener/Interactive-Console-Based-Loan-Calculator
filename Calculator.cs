using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Lab
{
    public class LoanCalculator
    {
        #region Data Inputs
        // data inputs
        private decimal _purchasePrice;
        private decimal _loanSize;
        private int _loanTerm;
        private decimal _downPaymentOffered;
        #endregion

        // data components
        private decimal _minDownPayment;

        #region Properties
        // Properties
        public decimal PurchasePrice { get => _purchasePrice; set => _purchasePrice = value; }

        public decimal LoanSize { get => _loanSize; set => _loanSize = value; }

        public int LoanTerm { get => _loanTerm; set => _loanTerm = value; }

        public decimal DownPaymentOffered { get => _downPaymentOffered; set => _downPaymentOffered = value; }

        public decimal MinDownPayment { get => _minDownPayment; set => _minDownPayment = value; }
        #endregion

        // Loan calculator constructor
        public LoanCalculator()
        {
            this._loanTerm = LoanTerm;
            this._purchasePrice = PurchasePrice;
            this._downPaymentOffered = DownPaymentOffered;
        } // end constructor

        public void StartCalculator()
        {
            InitialInput(); // Introduce program to user
            PurchasePricePrompt(); // Ask user for home price
            LoanAmountPrompt();
            LoanTermPrompt(); // Ask user for loan term
            DownPaymentOfferedPrompt(); // Ask user for amount they intend to put down initially

        } // end method StartCalculator

        // Introduce user to program
        public void InitialInput()
        {
            Console.WriteLine("Welcome and thank you for your interest in purchasing a home.\n" +
                            "We are here to assist you today, and will only need a few pieces of \ninformation from you " +
                            "before you can complete your loan.");
            Console.WriteLine("---------------------------------------------");
            Start:
            Console.WriteLine("Press \"C\" to continue, or \"Q\" to quit.");

            string inp = Console.ReadLine();

            try
            {
                char input = Convert.ToChar(inp.ToUpper());

                if (input != 'C' && input != 'Q')
                {
                    Console.Write("Invalid input. ");
                    goto Start;
                }

                if (input == 'Q')
                {
                    Console.WriteLine("Quitting... thanks for your time.");
                    Environment.Exit(0);
                }
            } 
            catch (Exception e)
            {
                Console.Write("Invalid input. ");
                goto Start;
            }
            
        } // end method initialInput

        // Ask user for home price
        public void PurchasePricePrompt()
        {
            Console.Write("\nGreat! Let's get started.\n" +
    "First, we'll ask you to enter the price of the home you would like to purchase.\n" +
    "Please enter a value now: $");
            StartTry:
            try
            {
                PurchasePrice = Decimal.Parse(Console.ReadLine()); // store home value in decimal
            }
            catch (Exception e)
            {
                Console.Write("\nInvalid input. Please enter a valid home price: $");
                goto StartTry;
            }
        } // end method PurchasePricePrompt

        // Ask user for amount they wish to loan
        public void LoanAmountPrompt()
        {
            Console.Write("\nExcellent. How much would you like to borrow to make this purchase?" +
                "\nPlease enter a valid value now: $");

            StartTry:
            try
            {
                LoanSize = Decimal.Parse(Console.ReadLine()); // store loan amount in decimal
            }
            catch (Exception e)
            {
                Console.Write("\nInvalid input. Please enter a valid value now: $");
                goto StartTry;
            }
        } // end method LoanAmountPrompt()

        // Ask user for loan term
        public void LoanTermPrompt()
        {
            Console.Write("\nThank you. You may apply for either a 15-year or a 30-year " +
                $"loan to cover the amount of {LoanSize:C2}. \nPlease enter the term of your loan in years: ");

            StartTry:
            try
            {
                LoanTerm = Int32.Parse(Console.ReadLine());
                while (LoanTerm != 15 && LoanTerm != 30)
                {
                    Console.Write("\nInvalid input. The term of your loan must be either 15 or 30 years." +
                        "\nPlease enter a valid input now: ");
                    LoanTerm = Int32.Parse(Console.ReadLine()); // store loan term in int
                }
            }
            catch
            {
                Console.Write("\nInvalid input. Please enter a valid numerical value for your loan term now: ");
                goto StartTry;
            }
        } // end method LoanTermPrompt

        public void DownPaymentOfferedPrompt()
        {
            Console.Write($"\nGreat! If you are approved for a {LoanTerm}-year loan," +
                $"\nhow much can you put down today?" +
                $"\nPlease enter a value now: $");

            StartTry:
            try
            {
                DownPaymentOffered = Decimal.Parse(Console.ReadLine()); // store downpayment offered in decimal
            }
            catch (Exception e)
            {
                Console.Write("\nInvalid input. Please enter a valid down payment amount now: $");
                goto StartTry;
            }
            finally
            {
                MinDownPayment = decimal.Multiply(0.1m, LoanSize);

                if (LoanSize > PurchasePrice)
                {
                    MinDownPayment += LoanSize - PurchasePrice;
                }

                if (DownPaymentOffered < MinDownPayment)
                {
                    Console.Write("\nAlert! \nYour down payment must equate to at least 10% of the size of the loan you are taking out, \nplus " +
                        "the difference between the size of your loan and the price of the house you would like to purchase," +
                        "\nin order for you to avoid having to take out a loan insurance policy." +
                        $"\nCan you afford to put down {MinDownPayment - DownPaymentOffered} more today, for a downpayment of {MinDownPayment}?"); 
                }
            }
        }

        static void Main(string[] args)
        {
            LoanCalculator c = new LoanCalculator();
            c.StartCalculator();
        }
    }
}
