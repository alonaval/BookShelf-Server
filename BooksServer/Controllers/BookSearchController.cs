using BooksServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BooksServer.Controllers
{
    public class BookSearchController : ApiController
    {


        //GET api/<controller>/5
        //NEW
        //public int[] Get(long BookID)
        //{
        //    //הולכת לספריות של כל האנשים ומחפשת למי יש את הספר
        //    Library l = new Library();
        //    return l.bookSearch(BookID);
        //}

        public List<User> Get(long BookIsbn)
        {
            //הולכת לספריות של כל האנשים ומחפשת למי יש את הספר
            Book b = new Book();
            User u = new User();
            List <User> lu= new List<User>();
            // b.getBookByIsbn(BookIsbn);
            Library l = new Library();
            var temp = l.bookSearch(b.getBookByIsbn(BookIsbn));
            if (temp[0]==0)
            {
                return lu;
            }
            return u.GetUSersById(temp);

        }
        //NEW
        public Book Get(string title)
        {
            //הולכת לספריות של כל האנשים ומחפשת למי יש את הספר
            Book b = new Book();
            return b.SearchByTitle(title);
        }
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}