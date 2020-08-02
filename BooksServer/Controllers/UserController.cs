using BooksServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BooksServer.Controllers
{
    public class UserController : ApiController
    {
        // GET api/<controller>
        public List<User> Get()
        {
            User user = new User();
            return user.getUsers();

        }

        //Get User by UserID
        //NEW
        public User Get(int UserID)
        { 
           User u = new User();
           return u.GetUserById(UserID);
        }


        public List <User> Get(int id1, int id2)
        {
            User u = new User();
            return u.GetUsersById(id1, id2);
        }

        // POST api/<controller>
        [HttpPost]
        public User Post([FromBody]User user)
        {
            if (user.CheckIfExists(user.Email)==0)
            {
                return user;
            }
           
            user.addNewUser(user);  
            Library l = new Library();
            int newUserId = user.NewUserID();
            l.CreateLibrary(newUserId);

            return user.GetUserById(newUserId);
        }


        // DELETE api/<controller>/5
        public void Delete([FromBody]int id)
        {
            User user = new User();
            user.deleteUser(id);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public int Put( [FromBody]User user)
        {
            return user.UpdateUser(user);
        }

        public void Put(int userId, string token)
        {
            User u = new User();
            u.PutToken(userId, token);
        }

    }
}