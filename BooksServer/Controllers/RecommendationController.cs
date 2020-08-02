using BooksServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace BooksServer.Controllers
{
    public class RecommendationController : ApiController
    {
        // GET api/<controller> get user's recs by UserID
        public List<Recommendation> Get(int userid)
        {
            Recommendation rec = new Recommendation();
            return rec.getRecommendations(userid);
        }

        // GET - get user's rec to specific book
        public Recommendation Get(int userid, string bookTitle)
        {
            Recommendation Rec = new Recommendation();
            return Rec.getRecs(userid,bookTitle);

        }

        //GET- get all recs to specific book
        public List<Recommendation> Get(string bookTitle)
        {
            Recommendation rec = new Recommendation();
            return rec.getRecsByTitle(bookTitle);
        }

        // POST api/<controller>
        [HttpPost]
        public int Post([FromBody]Recommendation Rec)
        {
            return Rec.AddNewRecommendation(Rec);
        }

        [HttpPut]
        public void Put([FromBody]Recommendation Rec)
        {
            Rec.UpdateRec(Rec); 
        }

        // DELETE api/<controller>/5
        public void Delete(int userId, int recId)
        {
            Recommendation r = new Recommendation();
            r.DeleteRec(userId, recId);
        }
    }
}