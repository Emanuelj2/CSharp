// See https://aka.ms/new-console-template for more information

using StockMarket.Code;

public class Prorgam
{

    static void Main(string[] args)
    {
        StockManager market = new StockManager();

        market.AddStock(new Stock("Microsoft", 200));
        market.AddStock(new Stock("Google", 330));
        market.AddStock(new Stock("Apple", 200));


        // Try valid update
        market.UpdateStock("Microsoft", 230.0);

        // Try invalid big jump
        market.UpdateStock("Microsoft", 400.0);

        // Try negative
        market.UpdateStock("Apple", -50);

        // Try missing stock
        market.UpdateStock("Google", 100);

        //is not in the market
        market.UpdateStock("Nvidia", 200);

        Console.WriteLine("\nDone. Press any key...");
        Console.ReadKey();


    }
}