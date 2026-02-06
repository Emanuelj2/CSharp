using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace StockMarket.Code
{
    internal class Stock
    {
        public string? Name {  get; private set; }
        public double Price { get; private set; }


        //make the constructor
        public Stock(string name, double price)
        {
            Name = name;
            Price = price;
        }


        public void UpdatePrice(double newPrice)
        {
            Price = newPrice;
        }
    }
}
