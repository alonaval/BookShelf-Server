using BooksServer.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksServer.Models
{

    public class Library
    {
        int libraryID;
        int userID;
        long[] books;
        long bookID;

        public Library(int libraryID, int userID, long[] books, long bookID)
        {
            this.libraryID = libraryID;
            this.userID = userID;
            this.books = books;
            this.bookID = bookID;
        }
        public Library(int libraryID, int userID, long[] books)
        {
            this.libraryID = libraryID;
            this.userID = userID;
            this.books = books;
        }
        public Library(int libraryID,long bookID)
        {
            this.libraryID = libraryID;
            this.bookID = bookID;
        }

        public Library(int userID)
        {
            this.libraryID = userID;
            this.userID = userID;
        }
        public Library()
        {

        }


        public int LibraryID { get => libraryID; set => libraryID = value; }
        public int UserID { get => userID; set => userID = value; }
        public long[] Books { get => books; set => books = value; }
        public long BookID { get => bookID; set => bookID = value; }


        public void AddBook(long bookID, long[] books, int userID)
        {

            DBServices dbs = new DBServices();
            dbs.AddBookToLibrary(bookID, books, userID);

        }


        public void DeleteBook(long bookID, long[] books, int userID)
        {

            DBServices dbs = new DBServices();
            dbs.DeleteBookFromLibrary(bookID, books, userID);

        }


        public long[] GetBooksFromLibrary(int libraryId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetBooksFromLibrary(libraryId);
        }


        public int CreateLibrary(int userID)
        {
            DBServices dbs = new DBServices();
            return dbs.CreateLibrary(userID);

        }

        //NEW
        public int[] bookSearch(long bookID)
        {
            DBServices dbs = new DBServices();
            return dbs.bookSearch(bookID);
        }

    }

    
}