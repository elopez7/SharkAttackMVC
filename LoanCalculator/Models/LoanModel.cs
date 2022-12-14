namespace LoanCalculator.Models
{
    public class LoanModel
    {
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public int Term { get; set; }
        public decimal Payment { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalCost { get; set; }
        public List<LoanPaymentModel> Payments { get; set; } = new();
    }
}
