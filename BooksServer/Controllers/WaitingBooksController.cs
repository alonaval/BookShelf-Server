using BooksServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BooksServer.Controllers
{
    public class WaitingBooksController : ApiController
    {
        // GET api/<controller>
        public List<Book> Get(int userId)
        {
            WaitingBooks wb = new WaitingBooks();
            Book b = new Book();
            return b.getBooksArr(wb.GetBooksFromList(userId));
           // return wb.GetBooksFromList(userId);
        }


        // POST api/<controller>
        public void Post(int userId, long bookId)
        {
            WaitingBooks wb = new WaitingBooks();
            long[] books = wb.GetBooksFromList(userId);
            wb.AddToList(userId, bookId, books);
        }

        // DELETE api/<controller>/5
        public void Delete(int userId, long bookId)
        {
            WaitingBooks wb = new WaitingBooks();
            long[] books = wb.GetBooksFromList(userId);
            wb.DeleteFromList(userId, bookId, books);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

    }
}