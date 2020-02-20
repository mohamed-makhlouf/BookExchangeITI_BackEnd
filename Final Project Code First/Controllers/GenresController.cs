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

namespace CatagoryAPI.Controllers
{
    public class GenresController : ApiController
    {
        private BookExchangeModel db = new BookExchangeModel();
        // GET: api/Genres
        public IHttpActionResult GetGenres()
        {
            var geners = db.Genres.ToList().Select(ww => new { ww.Genre_Id, ww.Genre_Name });
            return Ok(geners);
        }


        // GET: api/Genres/5
        [ResponseType(typeof(Genre))]
        [HttpGet]
        public IHttpActionResult GetGenreById(int id)
        {

            var genre = db.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }
            var books = db.Genres.First(ww => ww.Genre_Id == id).Books.Select(ww => new { ww.Title });
            return Ok(books);
        }

        [HttpGet]
        public IHttpActionResult GetGenreByName(string name)
        {
            var genre = db.Genres.Where(ww => ww.Genre_Name.Contains(name)).Select(ww => new { ww.Genre_Id, ww.Genre_Name }).ToList();
            if (genre.Count == 0)
            {
                return NotFound();
            }

            return Ok(genre);
        }


        [ResponseType(typeof(void))]
        [HttpGet]
        [Route("api/Genres/Pages")]
        public IHttpActionResult GetGenresPages(int number)
        {

            int pagSize = 10;
            //var genrenumbers=db.Genres.Select(ww => new { ww.Genre_Id, ww.Genre_Name }).ToList();
            var genre = db.Genres.OrderBy(ww => ww.Genre_Id).Skip((number - 1) * pagSize).Take(pagSize).ToList().Select(ww => new { ww.Genre_Name, ww.Genre_Id });
            return Ok(genre);

        }
        // PUT: api/Genres/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGenre(int id, Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genre.Genre_Id)
            {
                return BadRequest();
            }

            db.Entry(genre).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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

        // POST: api/Genres
        [ResponseType(typeof(Genre))]
        public IHttpActionResult PostGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Genres.Add(genre);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = genre.Genre_Id }, genre);
        }

        // DELETE: api/Genres/5
        [ResponseType(typeof(Genre))]
        public IHttpActionResult DeleteGenre(int id)
        {
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }

            db.Genres.Remove(genre);
            db.SaveChanges();

            return Ok(genre);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GenreExists(int id)
        {
            return db.Genres.Count(e => e.Genre_Id == id) > 0;
        }
    }
}