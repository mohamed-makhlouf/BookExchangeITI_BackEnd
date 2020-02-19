using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Final_Project_Code_First.Models;

namespace Final_Project_Code_First.Controllers
{
    public class RatingsController : ApiController
    {
        private BookExchangeModel db = new BookExchangeModel();

       

        // GET: api/Ratings/5
        [ResponseType(typeof(Rating))]
        [Route("api/Ratings/")]
        public IHttpActionResult GetRating(int ratedUerId)
        {
            var rating = db.Ratings.Where(ww => ww.RatedUserId == ratedUerId).Select(ww => ww.Rate).Sum();
            var noOfUSers = db.Ratings.Where(ww => ww.RatedUserId == ratedUerId).Count();
            if(noOfUSers != 0)
            {
                var rate = rating / noOfUSers;
                return Ok(rate);
           
            }
            else
            {
                return Ok(0);
            }
            
        }

        // PUT: api/Ratings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRating(int id, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.Id)
            {
                return BadRequest();
            }

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Ratings
        [ResponseType(typeof(Rating))]
        public IHttpActionResult PostRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ratings.Add(rating);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rating.Id }, rating);
        }

        // DELETE: api/Ratings/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult DeleteRating(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            db.Ratings.Remove(rating);
            db.SaveChanges();

            return Ok(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(int id)
        {
            return db.Ratings.Count(e => e.Id == id) > 0;
        }
    }
}