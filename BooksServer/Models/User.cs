using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BooksServer.Models.DAL;

namespace BooksServer.Models
{
    public class User
    {
        int userID;
        string fullname;
        string password;
        string gender;
        string city;
        int points;
        string email;
        string preferedGenres;
        string token;


        public User() { }

        public User(int userID, string fullname, string password, string gender,  string city, int points, string email, string preferedGenres, string token )
        {
            this.userID = userID;
            this.fullname = fullname;
            this.password = password;
            this.gender = gender;
            this.city = city;
            this.points = points;
            this.email = email;
            this.preferedGenres = preferedGenres;
            this.token = token;
        }


        public int UserID { get => userID; set => userID = value; }
        public string Fullname { get => fullname; set => fullname = value; }
        public string Password { get => password; set => password = value; }
        public string Gender { get => gender; set => gender = value; }
        public string City { get => city; set => city = value; }
        public int Points { get => points; set => points = value; }
        public string Email { get => email; set => email = value; }
        public string  PreferedGenres { get => preferedGenres; set => preferedGenres = value; }
        public string Token { get => token; set => token = value; }

        public List<User> getUsers()
        {
            DBServices dbs = new DBServices();
            return dbs.getUsers();
        }

        //
        public int CheckIfExists(string email){
            DBServices dbs = new DBServices();
            return dbs.CheckIfExists(email);
        }
        public User GetUserById(int userID)
        {
            DBServices dbs = new DBServices();
            return dbs.getUserById(userID);
        }

        public List<User> GetUSersById(int[] userID)
        {
            DBServices dbs = new DBServices();
            return dbs.getUsersById(userID);
        }

        public List<User> GetUsersById(int id1, int id2)
        {
            DBServices dbs = new DBServices();
            return dbs.getChatUsersById(id1, id2);
        }

        public int addNewUser(User user)
        {
            DBServices dbs = new DBServices();
            return dbs.addUser(user);

        }

        public User CheckLoginData(string email, string password)
        {
            DBServices dbs = new DBServices();
            return dbs.CheckLoginData(email, password);
           
        }

        public int deleteUser(int userID)
        {
            DBServices dbs = new DBServices();
            return dbs.deleteUser(userID);
        }

        public int UpdateUser(User user)
        {
            DBServices dbs = new DBServices();
            return dbs.UpdateUser(user);
        }

        public int NewUserID()
        {

            DBServices dbs = new DBServices();
            return dbs.NewUserID();

        }

        public void PutToken(int userId, string token)
        {

            DBServices dbs = new DBServices();
            dbs.PutToken(userId, token);
        }

    }
}