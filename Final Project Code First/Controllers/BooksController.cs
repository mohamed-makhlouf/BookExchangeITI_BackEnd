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
using System.Security.Claims;
using Final_Project_Code_First.Models;
using System.Web.Http.Cors;

namespace Final_Project_Code_First.Controllers
{
    //[EnableCors(origins:"http://")]
    public class BooksController : ApiController
    {
        private BookExchangeModel db = new BookExchangeModel();

        // GET: api/Books
        //[Authorize(Roles = "Admin")]
        public IHttpActionResult GetBooks()
        {
            //var userIdentity = User.Identity as ClaimsIdentity;
            //var loggenInId = userIdentity.Claims.Where(claim => claim.Type.Equals("LoggenInUserId")).FirstOrDefault().Value;
            var books = db.Books.Take(10).ToList();
            if(books==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(books);
            }
        }

        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        //GetAll
        [ResponseType(typeof(Book))]
        [HttpGet]
        [Route("api/Books/page/{pageNumber:int}")]
        public IHttpActionResult GetAllByPageNo(int pageNumber)
        {
            int pageSize = 10;
            var book = db.Books.OrderBy(ww => ww.Author_Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList().Select(ww => new { ww.Author_Name, ww.Title, ww.Rate});
            if(book==null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [ResponseType(typeof(Book))]
        [Route("api/Books/GetBookByAuthor/{auth_name:alpha}")]
        public IHttpActionResult GetBookByAuthor(string auth_name)
        {
            var book = db.Books.Where(BB => BB.Author_Name.Contains(auth_name)).ToList();
            if (book.Count == 0)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [ResponseType(typeof(Book))]
        [Route("api/Books/GetBookByTitle/{BookTitle:alpha}")]
        public IHttpActionResult GetBookByTitle(string BookTitle)
        {
            var book = db.Books.Where(BB => BB.Title.Contains(BookTitle)).ToList();
            if (book.Count == 0)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        [Authorize]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Book_Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        [ResponseType(typeof(Book))]
        //[Authorize(Roles = "Admin")]
        public IHttpActionResult PostBook(Book book)
        {
            book.Rate = 0;
           var LoggedInUserId = UserUtilities.GetCurrentUserId(User);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Books.Add(book);
            if (book.Want.Equals("have"))
            {
                db.UserHaveBooks.Add(new UserHaveBook() { UserId = LoggedInUserId, BookId = book.Book_Id,BookConditionId=BookConditionEnum.New });
            }else if (book.Want.Equals("want"))
            {

                var user = db.Users.Where(user2 => user2.UserId == LoggedInUserId).FirstOrDefault();
                //book.UserWantBooks.Add(user);
            }

            try
            {
                db.SaveChanges();

            }
            catch (Exception e)
            {
                return Ok(e.Message);   
            }

            return CreatedAtRoute("DefaultApi", new { id = book.Book_Id }, book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        [Authorize]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Book_Id == id) > 0;
        }
    }
}