using BooksServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BooksServer.Controllers
{
    public class BookController : ApiController
    {
        // GET api/<controller>
        public List<Book> Get()
        {
            Book book = new Book();
            return book.getBooks();

        }

        // POST api/<controller>
        [HttpPost]
        public long Post([FromBody]Book book)
        {
            long bookExistsInDB_id = book.BookIDfromDB(book.Isbn);
            if (bookExistsInDB_id!=0)
            {
                return bookExistsInDB_id;
            }
            long id = book.GetBookMaxId();
            book.Bookid = id;
            book.addNewBook(book);
            return id;
        }

        

        // DELETE api/<controller>/5
        public void Delete([FromBody]int id)
        {

        }

    }
}