using System;

namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryCatalog libraryCatalog = new LibraryCatalog();

            while (true)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("          Library Menu");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("1. Add a Book");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. Search Books");
                Console.WriteLine("4. Borrow Book");
                Console.WriteLine("5. Return Book");
                Console.WriteLine("6. Exit");
                Console.WriteLine("----------------------------------------------");

                Console.Write("Choose an option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        libraryCatalog.AddBook();
                        break;

                    case 2:
                        libraryCatalog.ViewBooks();
                        break;

                    case 3:
                        libraryCatalog.SearchBooks();
                        break;

                    case 4:
                        libraryCatalog.BorrowBook();
                        break;

                    case 5:
                        libraryCatalog.ReturnBook();
                        break;

                    case 6:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
