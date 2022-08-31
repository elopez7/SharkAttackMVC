using LoanCalculator.Models;

namespace LoanCalculator.Helpers
{
    public class LoanHelper
    {
        public LoanModel GetPayments(LoanModel loan)
        {
            loan.Payment = CalculatePayment(loan.Amount, loan.Rate, loan.Term);

            var balance = loan.Amount;
            var totalInterest = 0.0m;
            var monthlyInterest = 0.0m;
            var monthlyPrincipal = 0.0m;
            var monthlyRate = CalculateMonthlyRate(loan.Rate);

            for(int month = 0; month <= loan.Term; month++)
            {
                monthlyInterest = CalculateMonthlyInterest(balance, monthlyRate);
                totalInterest += monthlyInterest;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                LoanPaymentModel loanPayment = new();

                loanPayment.Month = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyPrincipal = monthlyPrincipal;
                loanPayment.MonthlyInterest = monthlyInterest;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance;

                loan.Payments.Add(loanPayment);
            }

            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.Amount + totalInterest;

            return loan;
        }

        private decimal CalculatePayment(decimal amount, decimal rate, int term)
        {
            decimal monthlyRate = CalculateMonthlyRate(rate);

            var rateDouble = Convert.ToDouble(monthlyRate);
            var amountDouble = Convert.ToDouble(amount);

            var paymentDouble = (amountDouble * rateDouble) / (1 - Math.Pow(1 + rateDouble, -term));

            return Convert.ToDecimal(paymentDouble);
            
        }

        private decimal CalculateMonthlyRate(decimal rate)
        {
            return rate / 1200;
        }

        private decimal CalculateMonthlyInterest(decimal balance, decimal monthlyRate)
        {
            return balance * monthlyRate;
        }
    }
}
