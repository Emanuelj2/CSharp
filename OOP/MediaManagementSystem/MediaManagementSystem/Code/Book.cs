using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace MediaManagementSystem.Code
{
    internal class Book : LibraryItem
    {
        public string Author { get; private set; }
        public int Pages { get; private set; }

        //double LateFee = 0.25;

        public Book(int id, string title, string author, int pages) : base(id, title)
        {
            Author = author;
            Pages = pages;
        }

        public override string GetInfo()
        {
            return $"Book\n" +
                $"ID: {UniqueId}" +
                $"Title {Title}" +
                $"Author: {Author}" +
                $"Pages : {Pages}" +
                $"Borrowed {Borrowed}";
        }
    }
}
