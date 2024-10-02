using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq; // Make sure you have this for LINQ operations

namespace ExtraCredit
{
    public class JsonProgram
    {
        public class Book
        {
            public string Category { get; set; }
            public Title Title { get; set; }
            public List<string> Authors { get; set; }
            public int Year { get; set; }
            public double Price { get; set; }
        }

        public class Title
        {
            public string Lang { get; set; }
            public string Value { get; set; }
        }

        public class Bookstore
        {
            public List<Book> Books { get; set; }
        }

        static void Main(string[] args)
        {
            string jsonFilePath = @"C:\Users\Riley Wasdyke\source\repos\ExtraCredit\ExtraCredit\bookstore.json";
            Bookstore bookstore = LoadBooksFromJson(jsonFilePath);

            printTitlesAndPublishYear(bookstore.Books);
            printTitlesAndAuthors(bookstore.Books);
            printTotalCostOfAllBooks(bookstore.Books);
            printBooksGreaterThan2003(bookstore.Books);
            printBookTitleAndCategory(bookstore.Books);
            checkAndPrintAuthorsBooks(bookstore.Books);

            pressKeyToExit();
        }

        public static Bookstore LoadBooksFromJson(string jsonFilePath)
        {
            string json = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<Bookstore>(json);
        }
        public static void printTitlesAndPublishYear(List<Book> books)
        {
            Console.WriteLine("Titles of the books followed by their publish year: ");
            foreach (Book book in books)
            {
                if (book.Title != null)
                {
                    Console.WriteLine($"    Title: {book.Title.Value}, Year: {book.Year}");
                }
            }
        }
        public static void printTitlesAndAuthors(List<Book> books)
        {
            Console.WriteLine("\n\nTitles of the books followed by their authors: ");
            foreach (Book book in books)
            {
                if (book.Title != null && book.Authors != null && book.Authors.Count > 0)
                {
                    Console.Write($"    Title: {book.Title.Value}, Authors: ");
                    Console.WriteLine(string.Join("; ", book.Authors));
                }
            }
        }
        public static void printTotalCostOfAllBooks(List<Book> books)
        {
            double totalCost = 0.0;

            foreach (Book book in books)
            {
                totalCost += book.Price;
            }

            Console.WriteLine("\n\nTotal cost of all books: $" + totalCost);
        }
        public static void printBooksGreaterThan2003(List<Book> books)
        {
            Console.WriteLine("\n\nList of books published after 2003:");

            foreach (Book book in books)
            {
                if (book.Year > 2003)
                {
                    Console.WriteLine($"    Title: {book.Title.Value}, Year: {book.Year}");
                }
            }
        }
        public static void printBookTitleAndCategory(List<Book> books)
        {
            Console.WriteLine("\n\nTitles of the books followed by their category: ");
            foreach (Book book in books)
            {
                if (book.Title != null)
                {
                    Console.WriteLine($"    Title: {book.Title.Value}, Category: {book.Category}");
                }
            }
        }
        public static void checkAndPrintAuthorsBooks(List<Book> books)
        {
            Console.WriteLine("\nEnter an author name to check for available books:");
            string inputAuthor = Console.ReadLine();

            bool authorFound = false;

            foreach (Book book in books)
            {
                // Check if the Authors list is not null and contains the input author
                if (book.Authors != null && book.Authors.Contains(inputAuthor, StringComparer.OrdinalIgnoreCase))
                {
                    authorFound = true;
                    Console.WriteLine($"Title: {book.Title.Value} by Author: {inputAuthor}");
                }
            }

            if (!authorFound)
            {
                Console.WriteLine($"No books found by author: {inputAuthor}");
            }
        }
        public static void pressKeyToExit()
        {
            Console.WriteLine("\n\n\n\n\n\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
