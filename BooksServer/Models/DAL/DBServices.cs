using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace BooksServer.Models.DAL
{
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBServices()
        {

        }

        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        public SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandType = CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }


        //USER
        public String BuildInsertCommand(User u)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            string tempPreferedGenres = string.Join(",", u.PreferedGenres);

            sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}','{4}','{5}', '{6}' )", u.Fullname, u.Password, u.Gender, u.City, u.Points, u.Email, tempPreferedGenres);
            String prefix = "INSERT INTO Users2020 " + "(fullname, password, gender, city, points, email, preferedGenres)";
            command = prefix + sb.ToString();

            return command;
        }
        public List<User> getUsers()
        {
            List<User> userList = new List<User>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Users2020";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    User user = new User();
                    user.UserID = Convert.ToInt32(dr["UserID"]);
                    user.Fullname = (string)dr["fullname"];
                    user.Password = (string)dr["password"];
                    user.Gender = (string)dr["gender"];
                    user.City = (string)dr["city"];
                    user.Points = Convert.ToInt32(dr["points"]);
                    user.Email = (string)dr["email"];
                    user.PreferedGenres = ((string)dr["preferedGenres"]);

                    userList.Add(user);
                }

                return userList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public User getUserById(int userID)
        {
            User user = new User();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select * from Users2020 where UserID='{userID}'";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row 
                    string Token;
                    try
                    {
                        Token = (string)dr["Token"];
                    }
                    catch (Exception)
                    {

                        Token = "0";
                    }
                   
                   
                    user.UserID = Convert.ToInt32(dr["UserID"]);
                    user.Fullname = (string)dr["fullname"];
                    user.Password = (string)dr["password"];
                    user.Gender = (string)dr["gender"];
                    user.City = (string)dr["city"];
                    user.Points = Convert.ToInt32(dr["points"]);
                    user.Email = (string)dr["email"];
                    user.PreferedGenres = ((string)dr["preferedGenres"]);
                    user.Token = Token;

                }
                return user;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        public List<User> getUsersById(int[] UsersIdArr)
        {
            List<User> users = new List<User>();
            SqlConnection con = null;
            List<int> str = new List<int>();
            for (int i = 0; i < UsersIdArr.Length; i++)
            {
                str.Add(UsersIdArr[i]);
            }
            //var nums = new List<int> { 1, 2, 3 };
            string result = string.Join(", ", str);

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select *,isnull(token, 0) as Token from Users2020 where UserID in ({result})";




                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row  
                    User user = new User();
                    user.UserID = Convert.ToInt32(dr["UserID"]);
                    user.Fullname = (string)dr["fullname"];
                    user.Password = (string)dr["password"];
                    user.Gender = (string)dr["gender"];
                    user.City = (string)dr["city"];
                    user.Points = Convert.ToInt32(dr["points"]);
                    user.Email = (string)dr["email"];
                    user.PreferedGenres = ((string)dr["preferedGenres"]);
                    user.Token = (string)dr["Token"];
                    users.Add(user);
                }
                return users;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        public int deleteUser(int id)
        {

            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"DELETE FROM Users2020 WHERE UserID='{id}'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);


            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

            return id;
        }

        public int addUser(User user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(user);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public int UpdateUser(User u)
        {

            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String selectSTR = $"UPDATE Users2020 SET fullname='{u.Fullname}',password='{u.Password}',gender='{u.Gender}',city='{u.City}',points='{u.Points}',email='{u.Email}',preferedGenres='{u.PreferedGenres}' WHERE UserID={u.UserID}";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public int NewUserID()
        {
            int id = 0;

            SqlConnection con = null;

            try
            {
                Book book = new Book();
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select max(userID) as maxID from Users2020";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    id = Convert.ToInt32(dr["maxID"]);
                }

            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

            return id;


        }

        public List<User> getChatUsersById(int id1, int id2)
        {
            List<User> users = new List<User>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                //String selectSTR = $"select * from Users2020 where UserID='{userID}'";
                String selectSTR = $"select *,isnull(token, 0) as Token from Users2020 where UserID in ({id1}, {id2})";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row  
                    User user = new User();
                    user.UserID = Convert.ToInt32(dr["UserID"]);
                    user.Fullname = (string)dr["fullname"];
                    user.Password = (string)dr["password"];
                    user.Gender = (string)dr["gender"];
                    // user.Age = Convert.ToInt32(dr["age"]);
                    user.City = (string)dr["city"];
                    user.Points = Convert.ToInt32(dr["points"]);
                    user.Email = (string)dr["email"];
                    user.PreferedGenres = ((string)dr["preferedGenres"]);
                    user.Token = ((string)dr["Token"]);
                    users.Add(user);
                }
                return users;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        public void PutToken(int userId, string token)
        {
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            DateTime date = DateTime.Now;
            String selectSTR = $"UPDATE Users2020 SET token='{token}'  WHERE UserID={userId}";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }


        }

        public int CheckIfExists(string email)
        {
            SqlConnection con = null;
            try
            {
                int result = 0;
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"IF EXISTS (select * from Users2020 where email='{email}') SELECT 0 as result ELSE SELECT 1 as result";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                while (dr.Read())
                {   // Read till the end of the data into a row
                    result = Convert.ToInt32(dr["result"]);
                }
                return result;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }


        }
        //EndUSER

        //LOGIN
        public User CheckLoginData(string email, string password)
        {
            //int Userid = 0;
            User user = new User();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); 

               // String selectSTR = "Select UserID from Users2020 WHERE email =" + email + " and password=" + password;
                String selectSTR = "Select * from Users2020 WHERE email =" + email + " and password=" + password;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); 

                while (dr.Read())
                {
                    // Userid = Convert.ToInt32(dr["UserID"]);

                    user.UserID = Convert.ToInt32(dr["UserID"]);
                    user.Fullname = (string)dr["fullname"];
                    user.Password = (string)dr["password"];
                    user.Gender = (string)dr["gender"];
                    user.City = (string)dr["city"];
                    user.Points = Convert.ToInt32(dr["points"]);
                    user.Email = (string)dr["email"];
                    user.PreferedGenres = ((string)dr["preferedGenres"]);
                    //user.Token= (string)dr["token"];
                }

                //return Userid;
                return user;

            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }
        //EndLOGIN

        //LIBRARY
        public String InsertCreateLibraryCommand(int userID)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            int libraryID = userID;
            sb.AppendFormat("Values('{0}', '{1}')", libraryID, userID);
            String prefix = "INSERT INTO Library2020 " + "(LibraryID, UserID)";
            command = prefix + sb.ToString();

            return command;

        }

        public int CreateLibrary(int userID)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = InsertCreateLibraryCommand(userID);
            //cStr += InsertCreateWaitingBooksList(userID);

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public void DeleteBookFromLibrary(long bookID, long[] books, int libraryID)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            long[] arr1 = new long[books.Length - 1];

            var numbersList = books.ToList();
            numbersList.Remove(bookID);
            arr1 = numbersList.ToArray();
            string booksList = string.Join(",", arr1);
            string cStr = $"UPDATE Library2020 SET [NumOfBooks]='{booksList}' WHERE LibraryID = {libraryID};";
            cStr += $"UPDATE Library2020 SET NumOfBooks = NULLIF(NumOfBooks, '') WHERE LibraryID = {libraryID}"; //NULL הופך מקום ריק ל 
            cmd = CreateCommand(cStr, con); // create the command

            try
            {
                cmd.ExecuteNonQuery(); // execute the command
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public void AddBookToLibrary(long bookID, long[] books, int libraryID)
        {
            int pointsToAdd = 20;

            SqlConnection con;
            SqlCommand cmd;
            try
            {

                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            long[] books_arr;

            if (books[0] != 0)
            {
                books_arr = new long[books.Length + 1];
                for (int i = 0; i < books.Length; i++)
                {
                    books_arr[i] = books[i];
                }
                books_arr[books_arr.Length - 1] = bookID;
            }

            else
            {
                books_arr = new long[books.Length];
                books_arr[0] = bookID;
            }

            string booksList = string.Join(",", books_arr);

            string cStr = $"UPDATE Library2020 SET [NumOfBooks]='{booksList}' WHERE LibraryID = {libraryID}";      // helper method to build the insert string
            cStr += $"UPDATE users2020 SET points = points + {pointsToAdd} WHERE UserID = {libraryID}"; cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                cmd.ExecuteNonQuery(); // execute the command
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public long[] GetBooksFromLibrary(int libraryId)
        {

            long[] arr = { };
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select isnull (NumOfBooks,0) AS 'NumOfBooks' from Library2020 where LibraryID='{libraryId}'";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row  

                    if (((string)dr["NumOfBooks"]) == "")
                    {
                        return arr;
                    }
                    else
                    {
                        arr = Array.ConvertAll(((string)dr["NumOfBooks"]).Split(','), long.Parse);
                    }


                }
                return arr;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        public int[] bookSearch(long bookID)
        {
            int[] usersWithBook = new int[0];
            long[] books;
            List<int> users = new List<int>();


            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $" select UserID,isnull (NumOfBooks,0) AS 'NumOfBooks' from Library2020 ";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row  
                    int userId = Convert.ToInt32(dr["UserID"]);
                    books = Array.ConvertAll(((string)dr["NumOfBooks"]).Split(','), long.Parse);
                    for (int i = 0; i < books.Length; i++)
                    {
                        if (books[i] == bookID && bookID != 0)
                        {
                            users.Add(userId);
                        }
                    }

                }
                users.Add(0);
                usersWithBook = users.ToArray();


                return usersWithBook;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        //EndLIBRARY

        //LockedBook

        public long[] DeleteLockedBookFromLibrary(long bookID, long[] books, int userID)
        {
            long[] arr = { };
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            long[] arr1 = new long[books.Length - 1];

            var numbersList = books.ToList();
            numbersList.Remove(bookID);
            arr1 = numbersList.ToArray();
            string booksList = string.Join(",", arr1);
            string cStr = $"UPDATE Library2020 SET [LockedBooks]='{booksList}' WHERE UserID = {userID};";
            cStr += $"UPDATE Library2020 SET LockedBooks = NULLIF(LockedBooks, '') WHERE UserID = {userID}";
            cStr += $"select isnull (LockedBooks,0) AS 'LockedBooks' from Library2020 where LibraryID={userID}";//NULL הופך מקום ריק ל 
            cmd = CreateCommand(cStr, con); // create the command

            try
            {
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached t
                while (dr.Read())
                {   // Read till the end of the data into a row  

                    if (((string)dr["LockedBooks"]) == "")
                    {
                        return arr;
                    }
                    else
                    {
                        arr = Array.ConvertAll(((string)dr["LockedBooks"]).Split(','), long.Parse);
                    }


                }
                return arr;// execute the command
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public long[] AddLockedBookToLibrary(long bookID, long[] books, int userID)
        {
            long[] arr = { };


            SqlConnection con;
            SqlCommand cmd;
            try
            {

                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            long[] books_arr;

            if (books[0] != 0)
            {
                books_arr = new long[books.Length + 1];
                for (int i = 0; i < books.Length; i++)
                {
                    books_arr[i] = books[i];
                }
                books_arr[books_arr.Length - 1] = bookID;
            }

            else
            {
                books_arr = new long[books.Length];
                books_arr[0] = bookID;
            }

            string booksList = string.Join(",", books_arr);

            string cStr = $"UPDATE Library2020 SET [LockedBooks]='{booksList}' WHERE UserID = {userID}";
            cStr += $"select isnull (LockedBooks,0) AS 'LockedBooks' from Library2020 where LibraryID={userID}";// helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                cmd.ExecuteNonQuery(); // execute the command
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached t
                while (dr.Read())
                {   // Read till the end of the data into a row  

                    if (((string)dr["LockedBooks"]) == "")
                    {
                        return arr;
                    }
                    else
                    {
                        arr = Array.ConvertAll(((string)dr["LockedBooks"]).Split(','), long.Parse);
                    }


                }
                return arr;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public long[] GetLockedBooksFromLibrary(int userID)
        {
            long[] arr = { };
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select isnull (LockedBooks,0) AS 'LockedBooks' from Library2020 where UserID='{userID}'";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row  
                    if (((string)dr["LockedBooks"]) == "")
                    {
                        return arr;
                    }
                    else
                    {
                        arr = Array.ConvertAll(((string)dr["LockedBooks"]).Split(','), long.Parse);
                    }


                }
                return arr;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        //EndLockedBook

        //Book
        public String BuildInsertCommandBook(Book b)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            sb.AppendFormat("Values('{0}', '{1}' ,'{2}', '{3}','{4}','{5}', '{6}' ,'{7}','{8}')", b.Bookid, b.Title, b.Isbn, b.Author, b.Average_rating, b.Description, b.Genre, b.Picture, b.Points);
            String prefix = "INSERT INTO books2020 " + "(BookID,title,isbn,author,average_rating,description,genre,picture,points)";
            command = prefix + sb.ToString();

            return command;
        }

        public int UpdateBook(Book b)
        {
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            String selectSTR = $"UPDATE books2020 SET title='{b.Title}',author='{b.Author}',average_rating='{b.Average_rating}',description='{b.Description}',genre='{b.Genre}',picture='{b.Picture}' WHERE title={b.Title}";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }



        }

        public int AddBook(Book book)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            string cstr = BuildInsertCommandBook(book); // helper method to build the insert string

            cmd = CreateCommand(cstr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public List<Book> getBooks()
        {
            List<Book> booksList = new List<Book>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM books2020";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Book book = new Book();
                    book.Bookid = Convert.ToInt64(dr["bookID"]);
                    book.Isbn = Convert.ToInt64(dr["isbn"]);
                    book.Author = Convert.ToString(dr["Author"]);
                    book.Title = Convert.ToString(dr["Title"]);
                    book.Description = Convert.ToString(dr["Description"]);
                    //book.Genre = ((string)dr["Genre"]).Split(',');
                    book.Genre = Convert.ToString(dr["Genre"]);
                    book.Picture = Convert.ToString(dr["Picture"]);
                    book.Average_rating = (float)Convert.ToDouble(dr["Average_rating"]);
                    book.Points = Convert.ToInt32(dr["points"]);

                    Console.WriteLine(book);

                    booksList.Add(book);

                }

                return booksList;
            }
            catch (Exception ex)
            {

                // write to log
                Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public Book getBookById(long bookID)
        {

            SqlConnection con = null;

            try
            {
                Book book = new Book();
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select * from books2020 where bookID='{bookID}'";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    book.Bookid = Convert.ToInt64(dr["bookID"]);
                    book.Isbn = Convert.ToInt64(dr["isbn"]);
                    book.Author = Convert.ToString(dr["Author"]);
                    book.Title = Convert.ToString(dr["Title"]);
                    book.Description = Convert.ToString(dr["Description"]);
                    book.Genre = Convert.ToString(dr["Genre"]);
                    //book.Genre = ((string)dr["Genre"]).Split(',');
                    book.Picture = Convert.ToString(dr["Picture"]);
                    book.Average_rating = (float)Convert.ToDouble(dr["Average_rating"]);
                    book.Points = Convert.ToInt32(dr["points"]);

                    Console.WriteLine(book);



                }
                return book;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }


        }

        public List<Book> getBooksArr(long[] booksIdArr)
        {
            List<Book> books = new List<Book>();
            SqlConnection con = null;

            String[] string_list = new String[booksIdArr.Length];

            for (int i = 0; i < booksIdArr.Length; i++)
            {

                try
                {
                    //List<Book> books = new List<Book>();
                    con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                    String selectSTR = $"SELECT* FROM books2020 WHERE bookID IN('{ booksIdArr[i]}')";
                    SqlCommand cmd = new SqlCommand(selectSTR, con);

                    // get a reader
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                    while (dr.Read())
                    {   // Read till the end of the data into a row
                        Book book = new Book();
                        book.Bookid = Convert.ToInt64(dr["bookID"]);
                        book.Isbn = Convert.ToInt64(dr["isbn"]);
                        book.Author = Convert.ToString(dr["Author"]);
                        book.Title = Convert.ToString(dr["Title"]);
                        book.Description = Convert.ToString(dr["Description"]);
                        //book.Genre = ((string)dr["Genre"]).Split(',');
                        book.Genre = Convert.ToString(dr["Genre"]);
                        book.Picture = Convert.ToString(dr["Picture"]);
                        book.Average_rating = (float)Convert.ToDouble(dr["Average_rating"]);
                        book.Points = Convert.ToInt32(dr["points"]);



                        books.Add(book);

                    }
                    //  return books;
                }

                catch (Exception ex)
                {
                    // write to log
                    throw (ex);
                }


                finally
                {
                    if (con != null)
                    {
                        con.Close();
                    }
                }
            }
            return books;

        }

        public long GetBookMaxId()
        {
            long bookId = 0;

            SqlConnection con = null;

            try
            {
                Book book = new Book();
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT Max(bookID) as maxBookID FROM books2020 ";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    bookId = Convert.ToInt64(dr["maxBookID"]);
                }

            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }





            return bookId + 1;

        }

        public Book SearchBookByTitle(string title)
        {
            SqlConnection con = null;

            try
            {
                Book book = new Book();
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select * from books2020 where title='{title}'";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    book.Bookid = Convert.ToInt64(dr["bookID"]);
                    book.Isbn = Convert.ToInt64(dr["isbn"]);
                    book.Author = Convert.ToString(dr["Author"]);
                    book.Title = Convert.ToString(dr["Title"]);
                    book.Description = Convert.ToString(dr["Description"]);
                    book.Genre = Convert.ToString(dr["Genre"]);
                    //book.Genre = ((string)dr["Genre"]).Split(',');
                    book.Picture = Convert.ToString(dr["Picture"]);
                    book.Average_rating = (float)Convert.ToDouble(dr["Average_rating"]);
                    book.Points = Convert.ToInt32(dr["points"]);

                    Console.WriteLine(book);



                }

                return book;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        public long BookIDfromDB(long isbn)
        {
            SqlConnection con = null;
            try
            {
                long id = 0;
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
                String selectSTR = $"select bookID from books2020 where isbn = '{isbn}'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);
                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
                while (dr.Read())
                {   // Read till the end of the data into a row
                    id = Convert.ToInt64(dr["bookID"]);
                }
                return id;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        public long getBookByIsbn(long isbn)
        {
            SqlConnection con = null;
            long bookID = 0;

            try
            {
                Book book = new Book();
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select bookID from books2020 where isbn='{isbn}'";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row

                    bookID = Convert.ToInt64(dr["bookID"]);
                }
                return bookID;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }
        //EndBook



        //Genres
        public List<string> GetGenres()
        {
            List<String> genresList = new List<string>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT genre, COUNT (*) FROM books2020 GROUP BY genre HAVING COUNT(*)>5";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row  
                    if ((string)dr["genre"] != "")
                    {
                        genresList.Add((string)dr["genre"]);
                    }


                }
                return genresList;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        //EndGenres

        //MyRecommendations
        public List<Recommendation> GetRecsById(int UserID)
        {
            List<Recommendation> RecsList = new List<Recommendation>();
            SqlConnection con = null;

            try
            {
                // Recommendation Rec = new Recommendation();

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select * from UserRecommendation2020 where UserID='{UserID}'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row 
                    Recommendation Rec = new Recommendation();
                    Rec.RecID = Convert.ToInt32(dr["RecID"]);
                    Rec.Headline = Convert.ToString(dr["Headline"]);
                    Rec.Content = Convert.ToString(dr["Content"]);
                    Rec.RecDate = Convert.ToDateTime(dr["RecDate"]);
                    Rec.UserID = (int)Convert.ToInt32(dr["UserID"]);
                    Rec.BookName = Convert.ToString(dr["BookName"]);
                    Rec.Rating = Convert.ToInt32(dr["rating"]);
                    RecsList.Add(Rec);
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return RecsList;
        }


        public int AddRec(Recommendation Rec)
        {
            int pointsToAdd = 25;
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = InsertCreateRecCommand(Rec);
            cStr += $"UPDATE users2020 SET points = points + {pointsToAdd} WHERE UserID = {Rec.UserID}";// helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public List<Recommendation> getRecsByTitle(string bookTitle)
        {
            List<Recommendation> RecsList = new List<Recommendation>();
            SqlConnection con = null;

            try
            {
                // Recommendation Rec = new Recommendation();

                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select * from UserRecommendation2020 where BookName='{bookTitle}'";

                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row 
                    Recommendation Rec = new Recommendation();
                    Rec.RecID = Convert.ToInt32(dr["RecID"]);
                    Rec.Headline = Convert.ToString(dr["Headline"]);
                    Rec.Content = Convert.ToString(dr["Content"]);
                    Rec.RecDate = Convert.ToDateTime(dr["RecDate"]);
                    Rec.UserID = (int)Convert.ToInt32(dr["UserID"]);
                    Rec.BookName = Convert.ToString(dr["BookName"]);
                    Rec.Rating = Convert.ToInt32(dr["rating"]);
                    RecsList.Add(Rec);
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return RecsList;
        }


        public Recommendation GetRecs(int userid, string bookTitle)
        {
            Recommendation Rec = new Recommendation();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"SELECT * FROM UserRecommendation2020 where UserID='{userid}' AND BookName='{bookTitle}'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row 

                    Rec.RecID = Convert.ToInt32(dr["RecID"]);
                    Rec.Headline = Convert.ToString(dr["Headline"]);
                    Rec.Content = Convert.ToString(dr["Content"]);
                    Rec.RecDate = Convert.ToDateTime(dr["RecDate"]);
                    Rec.UserID = (int)Convert.ToInt32(dr["UserID"]);
                    Rec.BookName = Convert.ToString(dr["BookName"]);
                    Rec.Rating = Convert.ToInt32(dr["rating"]);


                }
                return Rec;

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public int UpdateRec(Recommendation R)
        {

            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            DateTime date = DateTime.Now;
            // String selectSTR = $"UPDATE UserRecommendation2020 SET RecDate={date} WHERE RecID=1";
            String selectSTR = $"UPDATE UserRecommendation2020 SET BookName='{R.BookName}', Headline='{R.Headline}', Content='{R.Content}', rating='{R.Rating}' WHERE RecID={R.RecID}";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            try
            {
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public String InsertCreateRecCommand(Recommendation R)
        {

            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string

            DateTime date = DateTime.Now;
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", R.UserID, R.BookName, R.Headline, R.Content, date, R.Rating);
            String prefix = "INSERT INTO UserRecommendation2020" + "( UserID, BookName, Headline, Content, RecDate, rating)";
            command = prefix + sb.ToString();

            return command;

        }

        public void DeleteRec(int userId, int recId)
        {
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"DELETE FROM UserRecommendation2020 where UserID={userId} AND RecID={recId}";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end



            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        //EndRecommendation

        public long[] GetBooksFromList(int userId)
        {

            long[] arr = { };
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"select isnull (WaitingBooks,0) AS 'Books' from Library2020 where userId='{userId}'";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row  

                    if (((string)dr["Books"]) == "")
                    {
                        return arr;
                    }
                    else
                    {
                        arr = Array.ConvertAll(((string)dr["Books"]).Split(','), long.Parse);
                    }


                }
                return arr;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

        }

        public void DeleteBookFromList(int userId, long isbn, long[] books)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            long[] arr1 = new long[books.Length - 1];

            var numbersList = books.ToList();
            numbersList.Remove(isbn);
            arr1 = numbersList.ToArray();
            string booksList = string.Join(",", arr1);
            string cStr = $"UPDATE Library2020 SET [WaitingBooks]='{booksList}' WHERE userId = {userId};";
            cStr += $"UPDATE Library2020 SET WaitingBooks = NULLIF(WaitingBooks, 0) WHERE userId = {userId}"; //NULL הופך מקום ריק ל 
            cmd = CreateCommand(cStr, con); // create the command

            try
            {
                cmd.ExecuteNonQuery(); // execute the command
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        public int[] GetWaitingUsers(long bookId)
        {

            int[] arr = { };
            List<int> users = new List<int>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = $"SELECT UserId FROM Library2020 WHERE WaitingBooks LIKE '%{bookId}%'";


                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row  

                    int userId = Convert.ToInt32(dr["UserId"]);
                    users.Add(userId);

                }
                arr = users.ToArray();
                return arr;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public void AddBookToList(int userId, long isbn, long[] books)
        {

            SqlConnection con;
            SqlCommand cmd;
            try
            {

                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            long[] books_arr;

            if (books[0] != 0)
            {
                books_arr = new long[books.Length + 1];
                for (int i = 0; i < books.Length; i++)
                {
                    books_arr[i] = books[i];
                }
                books_arr[books_arr.Length - 1] = isbn;
            }

            else
            {
                books_arr = new long[books.Length];
                books_arr[0] = isbn;
            }

            string booksList = string.Join(",", books_arr);

            string cStr = $"UPDATE Library2020 SET [WaitingBooks]='{booksList}' WHERE userId = {userId}";      // helper method to build the insert string
            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                cmd.ExecuteNonQuery(); // execute the command
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        //EndWaitingBooks


    }
}





