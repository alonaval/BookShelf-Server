using BooksServer.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksServer.Models
{
    public class BookStatus
    {
        int userID;
        long[] books;
        long bookID;

        public BookStatus(int userID, long[] books, long bookID)
        {
           
            this.userID = userID;
            this.books = books;
            this.bookID = bookID;
        }
        public BookStatus()
        {

        }

        public int UserID { get => userID; set => userID = value; }
        public long[] Books { get => books; set => books = value; }
        public long BookID { get => bookID; set => bookID = value; }


        public long[] AddLockedBook(long bookID, long[] books, int userID)
        {

            DBServices dbs = new DBServices();
           return dbs.AddLockedBookToLibrary(bookID, books, userID);

        }


        public long[] DeleteLockedBook(long bookID, long[] books, int userID)
        {

            DBServices dbs = new DBServices();
            return dbs.DeleteLockedBookFromLibrary(bookID, books, userID);

        }


        public long[] GetLockedBooksFromLibrary(int UserId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetLockedBooksFromLibrary(UserId);
        }
    }
}