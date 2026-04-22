using BankTransactionProcessor.Data;
using BankTransactionProcessor.Modles;

namespace BankTransactionProcessor.Services
{
    public class BankService
    {
        //this allows us to interact with the database through the AppDbContext
        private readonly AppDbContext _context;
        public BankService(AppDbContext context)
        {
            _context = context;
        }


        //get account balance by account id
        public async Task<decimal> GetBalace(int accountId)
        {
            var account = await _context.BankAccounts.FindAsync(accountId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            return account.Balance;
        }

        //deposite money into an account
        public async Task<decimal> Deposit(int accountId, decimal amount)
        {
            if(amount <= 0)
            {
                throw new Exception("Amount must be greater than zero");
            }

            var account = await _context.BankAccounts.FindAsync(accountId);

            if (account == null)
            {
                throw new Exception("Account not found");
            }

            account.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                Amount = amount,
                Type = TransactionType.Deposit,
                BankAccountId = accountId,
                Date = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return account.Balance;
        }

        //withdraw money from an account
        public async Task<decimal> Withdraw(int accountId, decimal amount)
        {
            var account = await _context.BankAccounts.FindAsync(accountId);

            if (account == null)
            {
                throw new Exception("Account not found");
            }

            if (amount <= 0 || amount > account.Balance)
            {
                throw new Exception("Amount must be greater than zero");
            }

            account.Balance -= amount;

            _context.Transactions.Add(new Transaction
            {
                Amount = amount,
                Type = TransactionType.Withdrawal,
                BankAccountId = accountId,
                Date = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return account.Balance;
        }

    }
}
