// See https://aka.ms/new-console-template for more information


using MediaManagementSystem.Code;

public class Program
{
    static void Main()
    {
        LibraryManager manager = new LibraryManager();

        // Create items
        Book book1 = new Book(1, "1984", "George Orwell", 328);
        DvD dvd1 = new DvD(2, "Inception", 148, 13);


        // Add items
        manager.AddItem(book1);
        manager.AddItem(dvd1);

        // Test borrowing
        manager.BorrowItem(1);
        manager.BorrowItem(2);

        // Print info
        manager.PrintAllItems();

        // Return item
        manager.ReturnItem(1);

        // Print again
        manager.PrintAllItems();

        
    }
}