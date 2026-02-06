using System;
using System.Collections.Generic;

namespace StockMarket.Code
{
    internal class StockManager
    {
        private Dictionary<string, Stock> stocks =
            new Dictionary<string, Stock>();

        public void AddStock(Stock stock)
        {
            stocks[stock.Name] = stock;
        }

        public bool HasStock(string name)
        {
            return stocks.ContainsKey(name);
        }

        public void UpdateStock(string name, double newPrice)
        {
            if (!HasStock(name))
            {
                Console.WriteLine($"{name} Stock not found");
                return;
            }

            if (newPrice < 0)
            {
                Console.WriteLine("Invalid price");
                return;
            }

            double oldPrice = stocks[name].Price;

            if (Math.Abs(newPrice - oldPrice) / oldPrice > 0.5)
            {
                Console.WriteLine("Price change too large. Update rejected.");
                return;
            }

            stocks[name].UpdatePrice(newPrice);
            Console.WriteLine($"{name} updated from ${oldPrice} to ${newPrice}");
        }
    }
}
