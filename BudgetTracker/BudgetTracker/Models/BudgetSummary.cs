namespace BudgetTracker.Models
{
    public class BudgetSummary
    {
        public decimal TotalIncome { get; }
        public decimal TotalExpenses { get; }
        public decimal NetBalance => TotalIncome - TotalExpenses;
        public DateTime PeriodStart { get; }
        public DateTime PeriodEnd { get; }

        public BudgetSummary(decimal totalIncome, decimal totalExpenses, DateTime periodStart, DateTime periodEnd)
        {
            if (totalIncome < 0)
                throw new ArgumentException("Total income cannot be negative");
            if (totalExpenses < 0)
                throw new ArgumentException("Total expense cannot be negative");
            TotalIncome = totalIncome;
            TotalExpenses = totalExpenses;
            PeriodStart = periodStart;
            PeriodEnd = periodEnd;
        }

        public bool IsInDeficit => NetBalance < 0;

        public decimal ExpenseRatio => TotalIncome == 0 ? 0 : Math.Round((TotalExpenses / TotalIncome) * 100, 2);
    }
}
