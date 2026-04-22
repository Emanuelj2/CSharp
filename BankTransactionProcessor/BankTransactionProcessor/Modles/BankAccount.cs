namespace BankTransactionProcessor.Modles
{
    public enum Type
    {
        Checking,
        Savings
    }

    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public Type Type { get; set; } = Type.Checking;

        //navigation property
        public List<Transaction> Transactions { get; set; }
    }
}
