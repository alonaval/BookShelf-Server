using BooksServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BooksServer.Controllers
{

    public class LibraryController : ApiController
    {
       

        // get by userID
        // GET api/<controller>/5
        public List<Book> Get(int userID)
        {
            //מחזיר את כל פרטי הספרים בספרייה
            Library l = new Library();
            Book b = new Book();
            return b.getBooksArr(l.GetBooksFromLibrary(userID));
        }


        public Book Get(long bookID)
        {//מחזיר פרטי הספר לפי איידי
            Book book = new Book();
            return book.getBookById(bookID);
        }


        //create library by userID
        // POST api/<controller>
        public void Post(int userID)
        {//מייצר ספרייה ריקה ליוזר
            Library l = new Library();
            l.CreateLibrary(userID);
        }

        //libraryID, bookID
        // PUT api/<controller>/5
        public List<User> Put(int userID, long bookID, long isbn)
        {
          Library l = new Library();
            //פונקציה מביאה את כל הספרים מהספריה
            long[] arr = l.GetBooksFromLibrary(userID);
            // פונקציה מוסיפה ספר למערך ספרים בספרייה
            l.AddBook(bookID, arr, userID);

            WaitingBooks wb = new WaitingBooks();
           int[]waitingUsers= wb.GetWaitingUsers(bookID);
            User u = new User();
            return u.GetUSersById(waitingUsers);
        }



        ////libraryID, bookID
        // DELETE api/<controller>/5
        public void Delete(int userID, long bookID)
        {
            Library l = new Library();
            //פונקציה מביאה את כל הספרים מהספריה
            long[] arr = l.GetBooksFromLibrary(userID);
            //פונקציה מוחקת ספר ממערך ספרים בספרייה
            l.DeleteBook(bookID, arr, userID);


            BookStatus bs = new BookStatus();
            //פונקציה מביאה את כל הספרים מהספריה
            long[] lockedArr = bs.GetLockedBooksFromLibrary(userID);
            //פונקציה מוחקת ספר ממערך ספרים בספרייה
             bs.DeleteLockedBook(bookID, lockedArr, userID);
        }
    }



}