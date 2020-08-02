using BooksServer.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksServer.Models
{
    public class Book
    {

        long bookid;
        string title;
        long isbn;
        string author;
        double average_rating;
        string description;
        string genre;
        string picture;
        int points;


        public Book() { }
        public Book(int Points, long Bookid, string Title, long Isbn, string Author, double Average_rat, string Description, string Genre, string Picture)
        {
            this.bookid = Bookid;
            this.title = Title;
            this.isbn = Isbn;
            this.author = Author;
            this.average_rating = Average_rat;
            this.genre = Genre;
            this.picture = Picture;
            this.description = Description;
            this.points = Points;

        }

       

        public long Bookid { get => bookid; set => bookid = value; }
        public string Title { get => title; set => title = value; }
        public long Isbn { get => isbn; set => isbn = value; }
        public string Author { get => author; set => author = value; }
        public double Average_rating { get => average_rating; set => average_rating = value; }
        public string Description { get => description; set => description = value; }
        public string Genre { get => genre; set => genre = value; }
        public string Picture { get => picture; set => picture = value; }
        public int Points { get => points; set => points = value; }

        public static void UpdateBookToDB(Book book)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateBook(book);
        }

        public List<Book> getBooks()
        {

            DBServices dbs = new DBServices();
            return dbs.getBooks();
        }

        public int addNewBook(Book book)
        {
            DBServices dbs = new DBServices();
            return dbs.AddBook(book);

        }

        public Book getBookById(long bookID)
        {
            DBServices dbs = new DBServices();
            return dbs.getBookById(bookID);
        }

        public long getBookByIsbn(long bookIsbn)
        {
            DBServices dbs = new DBServices();
            return dbs.getBookByIsbn(bookIsbn);
        }

        public List<Book> getBooksArr(long[] booksById)
        {
            List<Book> booksList = new List<Book>();
            DBServices dbs = new DBServices();

            return dbs.getBooksArr(booksById);


        }

        public List<string> GetGenres()
        {
            DBServices dbs = new DBServices();

            return dbs.GetGenres();

        }


        public long GetBookMaxId()
        {
            DBServices dbs = new DBServices();
            return dbs.GetBookMaxId();

        }

        public Book SearchByTitle(string title)
        {
            DBServices dbs = new DBServices();
            return dbs.SearchBookByTitle(title);

        }

        public long BookIDfromDB(long isbn)
        {
            DBServices dbs = new DBServices();
            return dbs.BookIDfromDB(isbn);
        }


    }
}