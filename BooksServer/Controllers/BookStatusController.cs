using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BooksServer.Models;

namespace BooksServer.Controllers
{
    public class BookStatusController : ApiController
    {

        public long[] Get(int userID)
        {
            //מחזיר את כל פרטי הספרים בספרייה
            BookStatus bs = new BookStatus();
            return bs.GetLockedBooksFromLibrary(userID);
        }

        public bool Get(int userID, long bookIsbn)
        {
            //מחזיר את כל פרטי הספרים בספרייה
            Book b = new Book();
            long bookID = b.getBookByIsbn(bookIsbn);
            BookStatus bs = new BookStatus();
            bool val = false;
            long[] arr=bs.GetLockedBooksFromLibrary(userID);
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i]==bookID)
                {
                    val = true;
                }
            }
            return val;
        }


        public long[] Put(int userID, long bookID)
        {
          BookStatus bs = new BookStatus();
            //פונקציה מביאה את כל הספרים מהספריה
            long[] arr = bs.GetLockedBooksFromLibrary(userID);
            // פונקציה מוסיפה ספר למערך ספרים בספרייה
            return bs.AddLockedBook(bookID, arr, userID);
        }



        ////libraryID, bookID
        // DELETE api/<controller>/5
        public long[] Delete(int userID, long bookID)
        {
            BookStatus bs = new BookStatus();
            //פונקציה מביאה את כל הספרים מהספריה
            long[] arr = bs.GetLockedBooksFromLibrary(userID);
            //פונקציה מוחקת ספר ממערך ספרים בספרייה
            return bs.DeleteLockedBook(bookID, arr, userID);
        }
    }
}