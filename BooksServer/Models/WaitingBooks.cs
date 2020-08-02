using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BooksServer.Models.DAL;

namespace BooksServer.Models
{
    public class WaitingBooks
    {

        long bookId;
        int userId;
        public WaitingBooks() { }
        public WaitingBooks(long bookId, int userId)
        {
            this.BookId = bookId;
            this.UserId = userId;
        }

        public long BookId { get => bookId; set => bookId = value; }
        public int UserId { get => userId; set => userId = value; }

        public void AddToList(int userId, long bookId, long[] books)
        {
            DBServices dbs = new DBServices();
            dbs.AddBookToList(userId, bookId, books);

        }

        public void DeleteFromList(int userId, long bookId, long[] books)
        {

            DBServices dbs = new DBServices();
            dbs.DeleteBookFromList(userId, bookId, books);
        }

        public long[] GetBooksFromList(int userId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetBooksFromList(userId);
        }

        public int[] GetWaitingUsers(long bookId)
        {
            DBServices dbs = new DBServices();
            return dbs.GetWaitingUsers(bookId);
        }

    }
}