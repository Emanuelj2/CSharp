using System;
using System.Collections.Generic;
using System.Text;

namespace MediaManagementSystem.Code
{
    internal class LibraryItem
    {
        public string Title { get; protected set; }
        public int UniqueId { get; protected set; }
        public bool Borrowed { get; protected set; }


        public LibraryItem( int id, string title)
        { 
            Title = title;
            UniqueId = id;
            Borrowed = false;
        }


        public bool isBorrowed()
        {
            return Borrowed; 
        }

        public virtual void Borrow()
        {
            if(Borrowed)
            {
                Console.WriteLine("Item is borrowed");
                return;
            }

            Borrowed = true;
            Console.WriteLine($"{Title} has been borrowed");
        }

        public virtual void Return()
        {
            if(!Borrowed)
            {
                Console.WriteLine("Item is not currently borrowed");
                return;
            }

            Borrowed = false;
            Console.WriteLine($"{Title} has been returned.");
        }

        public virtual string GetInfo()
        {
            return $"ID: {UniqueId}, Title: {Title}, Borrowed: {Borrowed}";
        }

        //public abstract double GetLateFee(int daysLate);
    }
}
