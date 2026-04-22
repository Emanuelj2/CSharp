namespace BankTransactionProcessor.Modles
{
    public enum TransactionType
    {
        Deposit,
        Withdrawal,

    }
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
