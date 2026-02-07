using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace MediaManagementSystem.Code
{
    internal class LibraryManager
    {
        Dictionary<int, LibraryItem> items = new Dictionary<int, LibraryItem>();

        /*
            AddItem(item)

            RemoveItem(id)

            BorrowItem(id)

            ReturnItem(id)

            GetItemInfo(id)

            PrintAllItems()
         */

        public void AddItem(LibraryItem item)
        {
            if(!items.ContainsKey(item.UniqueId))
            {
                items.Add(item.UniqueId, item);
                Console.WriteLine("Item added");
            }
            else
            {
                Console.WriteLine("item already exist");
            }
        }

        public void RemoveItem(int id)
        {
            if (items.ContainsKey(id))
            {
                items.Remove(id);
                Console.WriteLine("Item removed");
            }
            else
            {
                Console.WriteLine("Item does not exist");
            }
        }

        public void BorrowItem(int id)
        {
            if(items.ContainsKey(id))
            {
                items[id].Borrow();
            }
            else
            {
                Console.WriteLine("item not found");
            }
        }

        public void ReturnItem(int id)
        {
            if(items.ContainsKey(id))
            {
                items[id].Return();
            }
            else
            {
                Console.WriteLine("Item not found");
            }
        }

        public void GetItemInfo(int id)
        {
            if (items.ContainsKey(id))
            {
                Console.WriteLine(items[id].GetInfo());
            }
            else
            {
                Console.WriteLine("Item not found");
            }
        }

        public void PrintAllItems()
        {
            foreach (var item in items.Values)
            {
                Console.WriteLine(item.GetInfo());
                Console.WriteLine("---------------------");
            }
        }
    }
}
