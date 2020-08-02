using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BooksServer.Models.DAL;
namespace BooksServer.Models
{
    public class Recommendation
    {

        int recid;
        string headline;
        string content;
        int userid;
        DateTime recdate;
        string bookname;
        int rating;

        public Recommendation() { }
       

        private Recommendation(int recid, string headline, string content, DateTime recdate, int userid, string bookname, int rating)
        {
            this.recid = recid;
            this.headline = headline;
            this.content = content;
            this.recdate = recdate;
            this.userid = userid;
            this.bookname = bookname;
            this.rating = rating;
        }

        public Recommendation( string headline, string content, int userid, string bookname, int recid)
        {
           
            this.headline = headline;
            this.content = content;
            this.userid = userid;
            this.bookname = bookname;
            this.recid = recid; 
        }


        public int RecID { get => recid; set => recid = value; }
        public string Headline { get => headline; set => headline = value; }
        public string Content { get => content; set => content = value; }
        public DateTime RecDate { get => recdate; set => recdate = value; }
        public int UserID { get => userid; set => userid = value; }
        public string BookName { get => bookname; set => bookname = value; }
        public int Rating { get => rating; set => rating = value; }

        public List<Recommendation> getRecommendations(int RecID)
        {
            DBServices dbs = new DBServices();
            return dbs.GetRecsById(RecID);
        }


        public int AddNewRecommendation(Recommendation rec)
        {
            DBServices dbs = new DBServices();
            return dbs.AddRec(rec);

        }

        public Recommendation getRecs(int userid, string bookTitle)
        {
            DBServices dbs = new DBServices();
            return dbs.GetRecs(userid, bookTitle);
        }

        public List<Recommendation> getRecsByTitle(string bookTitle)
        {
            DBServices dbs = new DBServices();
            return dbs.getRecsByTitle(bookTitle);
        }

        

        public void UpdateRec (Recommendation Rec)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateRec(Rec);
        }

        public void DeleteRec(int userId, int recId)
        {
            DBServices dbs = new DBServices();
            dbs.DeleteRec(userId, recId);
        }
    }
}