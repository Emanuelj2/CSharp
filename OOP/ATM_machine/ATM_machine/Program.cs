using System.Transactions;

class Program
{ 
    static void Main(String[] args)
    {
        int balance, depositeAmt, withdrawAmt;
        int choice = 0, pin = 0;

        Console.WriteLine("Enter your ledger balance");
        balance = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter tour pin number");
        pin = int.Parse(Console.ReadLine());

        if(pin != 1234)
        {
            Console.WriteLine("invalid pin");
            Console.ReadKey(false);
            return;
        }

        while(choice != 4)
        {
            Console.WriteLine("1. check balance");
            Console.WriteLine("2. withdraw");
            Console.WriteLine("3. deposite");
            Console.WriteLine("4. exit");

            Console.WriteLine("Enter your choice.");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("\n Your balance $ : {0}", balance);
                    break;
                case 2:
                    Console.WriteLine("\n Enter the amount that you want to withdraw: ");
                    withdrawAmt = int.Parse(Console.ReadLine());
                    if (withdrawAmt % 100 != 0)
                    {
                        Console.WriteLine("\n Denominations present are 100,500 and 2000. Your amount cannot be processed");
                    }
                    else if(withdrawAmt > balance)
                    {
                        Console.WriteLine("\n low funds");
                    }
                    else
                    {
                        balance = balance - withdrawAmt;
                        Console.WriteLine("\n transaction is processed");
                        Console.WriteLine("\n current balance $: {0}", balance);
                    }
                    break;
                case 3:
                    Console.WriteLine("\nEnter the amount to deposite: ");
                    depositeAmt = int.Parse(Console.ReadLine());
                    if (depositeAmt < 0)
                    {
                        Console.WriteLine("Invalid.");
                    }
                    balance += depositeAmt;

                    Console.WriteLine("Your balance is {0}", balance);
                    break;
                case 4:
                    Console.WriteLine("\n thankyou for using the atm");
                    break;
            }
        }
        Console.ReadLine();
    }
}
