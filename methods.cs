using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    /// <summary>
    /// LibraryCatalog represents a simple library management system allowing users
    /// to add, view, search, borrow, and return books.
    /// </summary>

    internal class LibraryCatalog
    {
        // Define a Book class to represent book details
        public class Book
        {
            public int Id;
            public string Title;
            public string Author;
            public int PublicationYear;
            public string Genre;
            public bool IsBorrowed;
        }

        // List to store books in the library catalog
        public List<Book> books = new List<Book>();

        // Method to add a new book to the catalog
        public void AddBook()
        {
            Book newBook = new Book();

            newBook.Id = books.Count + 1;

            Console.Write("Enter Book Title: ");
            newBook.Title = Console.ReadLine();

            Console.Write("Enter Author: ");
            newBook.Author = Console.ReadLine();

            Console.Write("Enter Publication Year: ");
            newBook.PublicationYear = GetInput<int>(" Please enter a valid year: ");

            Console.Write("Enter Genre: ");
            newBook.Genre = Console.ReadLine();

            // Set IsBorrowed to false for a new book
            newBook.IsBorrowed = false;

            // Add the new book to the catalog
            books.Add(newBook);
            Console.WriteLine("Book added successfully!");
        }

        // method to get valid user input with error handling
        public T GetInput<T>(string errorMessage)
        {
            while (true)
            {
                try
                {
                    string userInput = Console.ReadLine();
                    return (T)Convert.ChangeType(userInput, typeof(T));
                }
                catch (FormatException)
                {
                    Console.Write(errorMessage);
                }
            }
        }

        // Method to view books based on user's choice
        public void ViewBooks()
        {
            Console.Clear();
            Console.WriteLine("View Books:");
            Console.WriteLine("1. All Books");
            Console.WriteLine("2. By Genre");
            Console.Write(" Choose an Option: ");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                // Display all books
                case 1:
                    DisplayBooks(books);
                    break;

                // Display books by a specific genre
                case 2:
                    Console.Write("Enter Genre: ");
                    string genreInput = Console.ReadLine();
                    List<Book> genreBooks = books.FindAll(b => b.Genre.ToLower() == genreInput.ToLower());

                    DisplayBooks(genreBooks);
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        // Method to search and display books based on a specific condition
        public void SearchAndDisplayBooks(Func<Book, bool> searchCondition)
        {
            // Create a new list to store search results
            List<Book> searchResult = new List<Book>();

            // Iterate through each book in the 'books' list
            foreach (var book in books)
            {
                // Check if the current book satisfies the search condition
                if (searchCondition(book))
                {
                    // If yes, add it to the searchResult list
                    searchResult.Add(book);
                }
            }

            // Display the search results
            DisplayBooks(searchResult);
        }

        public bool SearchByTitle(Book book, string searchTitle)
        {
            return book.Title.ToLower().Contains(searchTitle.ToLower());
        }
         
        public bool SearchByAuthor(Book book, string searchAuthor)
        {
            return book.Author.ToLower().Contains(searchAuthor.ToLower());
        }

        // Method to search books by title and display the results
        public void SearchByTitleAndDisplayBooks(string searchTitle)
        {
            SearchAndDisplayBooks(book => SearchByTitle(book, searchTitle));
        }

        // Method to search books by author and display the results
        public void SearchByAuthorAndDisplayBooks(string searchAuthor)
        {
            SearchAndDisplayBooks(book => SearchByAuthor(book, searchAuthor));
        }

        // Method to search books by ID and display the results
        public void SearchByIdAndDisplayBooks(int searchId)
        {
            SearchAndDisplayBooks(book => book.Id == searchId);
        }

        // Method to search for books based on user's choice
        public void SearchBooks()
        {
            Console.Clear();
            Console.WriteLine("Search Books:");
            Console.WriteLine("1. By Title");
            Console.WriteLine("2. By Author Name");
            Console.WriteLine("3. By ID");
            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                // Search books by title
                case 1:
                    Console.Write("Enter Title: ");
                    string title = Console.ReadLine();
                    SearchByTitleAndDisplayBooks(title);
                    break;

                // Search books by Author
                case 2:
                    Console.Write("Enter Author Name: ");
                    string author  = Console.ReadLine();
                    SearchByAuthorAndDisplayBooks(author);
                    break;

                // Search books by ID
                case 3:
                    Console.Write("Enter ID: ");
                    int id = GetInput<int>("Please enter a valid ID: ");
                    SearchByIdAndDisplayBooks(id);
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        // Method to borrow a book based on its title
        public void BorrowBook()
        {
            Console.Clear();
            Console.Write("Enter Title of the book you want to borrow: ");
            string titleBorrow = Console.ReadLine();

            Book bookBorrow = FindBookByTitle(titleBorrow);

            if (bookBorrow != null && !bookBorrow.IsBorrowed)
            {
                bookBorrow.IsBorrowed = true;
                Console.WriteLine($"Book '{bookBorrow.Title}' borrowed successfully!");
            }
            else if (bookBorrow == null)
            {
                Console.WriteLine("Book not found.");
            }
            else
            {
                Console.WriteLine("Book is already borrowed.");
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        // Method to return a book based on its title
        public void ReturnBook()
        {
            Console.Clear();
            Console.Write("Enter Title of the book you want to return: ");
            string titleReturn = Console.ReadLine();

            Book  returnBook = FindBookByTitle(titleReturn);

            if (returnBook != null && returnBook.IsBorrowed)
            {
                returnBook.IsBorrowed = false;
                Console.WriteLine($"Book '{returnBook.Title}' returned successfully!");
            }
            else if (returnBook == null)
            {
                Console.WriteLine("Book not found.");
            }
            else
            {
                Console.WriteLine("Book is not borrowed.");
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        // Method to find a book in the catalog by its title
        private Book FindBookByTitle(string title)
        {
            return books.Find(book => book.Title.ToLower() == title.ToLower());
        }

        // Method to display a list of books with their details
        public void DisplayBooks(List<Book> bookList)
        {
            Console.Clear();

            if (bookList.Count == 0)
            {
                Console.WriteLine("No books to show.");
                return;
            }

            Console.WriteLine("|ID|\t\t|Title|\t\t|Author|\t\t|Year|\t|Genre|\t\t|Status|");
            Console.WriteLine("-*---*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*--*");

            foreach (var book in bookList)
            {
                Console.WriteLine($"{book.Id}\t{book.Title}\t\t{book.Author}\t\t{book.PublicationYear}\t{book.Genre}\t\t{(book.IsBorrowed ? "Borrowed" : "Available")}");
            }

            Console.WriteLine("-----------------------------------------------------------------------------");
        }
    }
}

