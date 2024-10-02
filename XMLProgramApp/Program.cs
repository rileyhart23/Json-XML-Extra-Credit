using System;
using System.Xml;

// dotnet run --project /Users/rileyhart/GitHub/WebsiteDevPortfolio/Json-XML-Extra-Credit/XMLProgramApp




namespace ExtraCredit
{
    class XMLProgram
    {

        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"/Users/rileyhart/GitHub/WebsiteDevPortfolio/Json-XML-Extra-Credit/XMLProgramApp/bookstore.xml");

            XmlNode root = doc.DocumentElement;
            XmlNodeList bookNodes = doc.GetElementsByTagName("book");

            Console.WriteLine("\t\tXML Program\n");
            printTitlesAndPublishYear(bookNodes);
            printTitlesAndAuthors(bookNodes);
            printTotalCostOfAllBooks(bookNodes);
            printBooksGreaterThan2003(bookNodes);
            printBookTitleAndCategory(bookNodes);
            checkAndPrintAuthorsBooks(bookNodes);




            pressKeyToExit();
        }




        public static void printTitlesAndPublishYear(XmlNodeList bookNodes)
        {
            Console.WriteLine("Titles of the books followed by there publish year: ");
            foreach (XmlNode bookNode in bookNodes)
            {
                // Get the title and year for each book
                XmlNode titleNode = bookNode["title"];
                XmlNode yearNode = bookNode["year"];

                // Print the result
                if (titleNode != null && yearNode != null)
                {
                    Console.WriteLine($"    Title: {titleNode.InnerText}, Year: {yearNode.InnerText}");
                }

            }
        }
        public static void printTitlesAndAuthors(XmlNodeList bookNodes)
        {
            Console.WriteLine("\n\nTitles of the books followed by there authors: ");
            foreach (XmlNode bookNode in bookNodes)
            {
                XmlNode titleNode = bookNode["title"];
                XmlNodeList authorNodes = bookNode.SelectNodes("author");
                if (titleNode != null && authorNodes.Count > 0)
                {
                    Console.Write($"    Title: {titleNode.InnerText}, Authors: ");

                    for (int i = 0; i < authorNodes.Count; i++)
                    {
                        XmlNode authorNode = authorNodes[i];
                        if (authorNode != null)
                        {
                            Console.Write(authorNode.InnerText);
                            if (i < authorNodes.Count - 1)
                            {
                                Console.Write("; ");
                            }
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
  
        public static void printTotalCostOfAllBooks(XmlNodeList bookNodes)
        {
            double totalCost = 0.0;

            foreach (XmlNode bookNode in bookNodes)
            {
                XmlNode priceNode = bookNode["price"];

                if (priceNode != null)
                {
                    // Parse the price value and add it to the total cost
                    double price = double.Parse(priceNode.InnerText);
                    totalCost += price;
                }
            }

            // Print the total cost
            Console.WriteLine("\n\nTotal cost of all books: $" + totalCost);
        }
        public static void printBooksGreaterThan2003(XmlNodeList bookNodes)
        {

            Console.WriteLine("\n\nList of books published after 2003:");

            foreach (XmlNode bookNode in bookNodes)
            {
                XmlNode titleNode = bookNode["title"];
                XmlNode yearNode = bookNode["year"];

                if (titleNode != null && yearNode != null)
                {
                    // Convert the year text to an integer for comparison
                    int year = int.Parse(yearNode.InnerText);

                    // Check if the year is greater than 2003
                    if (year > 2003)
                    {
                        Console.WriteLine($"    Title: {titleNode.InnerText}, Year: {year}");
                    }
                }
            }
        }
        public static void printBookTitleAndCategory(XmlNodeList bookNodes)
        {
            Console.WriteLine("\n\nTitles of the books followed by their category: ");
            foreach (XmlNode bookNode in bookNodes)
            {
                XmlNode titleNode = bookNode["title"];
                string category = bookNode.Attributes["category"]?.Value; // Get the category attribute

                if (titleNode != null)
                {
                    Console.WriteLine($"    Title: {titleNode.InnerText}, Category: {category}");
                }
            }
        }
        public static void checkAndPrintAuthorsBooks(XmlNodeList bookNodes)
        {
            Console.WriteLine("\nEnter an author name to check for available books:");
            string inputAuthor = Console.ReadLine();

            bool authorFound = false;

            foreach (XmlNode bookNode in bookNodes)
            {
                XmlNodeList authorNodes = bookNode.SelectNodes("author");
                XmlNode titleNode = bookNode["title"];
                foreach (XmlNode authorNode in authorNodes)
                {
                    if (authorNode.InnerText.Equals(inputAuthor, StringComparison.OrdinalIgnoreCase))
                    {
                        authorFound = true;
                        Console.WriteLine($"Title: {titleNode.InnerText} by Author: {authorNode.InnerText}");
                    }
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
