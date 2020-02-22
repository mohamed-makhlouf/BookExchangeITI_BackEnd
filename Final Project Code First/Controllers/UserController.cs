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
using Final_Project_Code_First.Controllers;

namespace Users.Controllers
{

    //[Authorize(Roles ="Admin")]
    public class UserController : ApiController
    {
        private BookExchangeModel db = new BookExchangeModel();

        //GetAll
        [ResponseType(typeof(User))]
        [HttpGet]
        [Route("api/User/page/{pageNumber:int}")]
        public IHttpActionResult GetAllByPageNo(int pageNumber, int pageSize)
        {     
            var user = db.Users.OrderBy(ww => ww.UserId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList().Select(ww => new { ww.FirstName, ww.LastName, ww.Address, ww.Rate });
            return Ok(user);
        }
        // GETById
        [ResponseType(typeof(User))]
        [HttpGet]
       
        public IHttpActionResult GetUser(int id, string type)
        {
            var query = db.Users.Where(ww => ww.UserId == id);
            if (query == null)
            {
                return NotFound();
            }
            if (type.Equals("mini"))
            {
                return Ok(query.Select(ww => new { ww.FirstName, ww.LastName, ww.Rate, ww.PhotoUrl }));
            }

            return Ok(query.Select(ww => new { ww.FirstName, ww.LastName, ww.Rate, ww.PhotoUrl, ww.Address, ww.Email, ww.PhoneNumber, ww.City }).First());

        }

        //GETByname
        [Route("api/User/{name:alpha}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUserByName(string name, string type)
        {
            var query = db.Users.Where(ww => (ww.FirstName + ww.LastName).Contains(name)).ToList();
            if (query == null)
            {
                return NotFound();
            }
            if (type.Equals("mini"))
            {
                return Ok(query.Select(ww => new { ww.FirstName, ww.LastName, ww.Rate, ww.PhotoUrl }));
            }

            return Ok(query.Select(ww => new { ww.FirstName, ww.LastName, ww.Rate, ww.PhotoUrl, ww.Address, ww.Email, ww.PhoneNumber, ww.City }).First());


        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        [HttpPost]
       // [Authorize]
       [Route("api/user/putUser")]


        public IHttpActionResult PutUser([FromBody] User user, int id)
        {
            // var currentId = UserUtilities.GetCurrentUserId(User);
            var currentId = id;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (currentId != user.UserId)
            {
                return Unauthorized();
            }

            db.Entry(user).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(currentId))
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

        // block and unblock
        [ResponseType(typeof(void))]
        [HttpDelete]
        [Route("api/user/block/{id:int}")]
        public IHttpActionResult DeleteUserBlocked(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Blocked = true ? false : true;
            return Ok(user.Blocked);
        }

        // POST: api/User
        [ResponseType(typeof(User))]
        [HttpPost]
        public IHttpActionResult PostUser([FromBody]User user)
        {
            if (user == null)
            {
                return NotFound();
            }
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return Ok(user);

        }
        [ResponseType(typeof(User))]
        [HttpGet]
        [Route("api/user/want/{id:int}")]
        public IHttpActionResult GetWantedBooks(int id)
        {
            //var user = db.Users.Where(ww => ww.UserId == id ).Select(ww => new { ww.FirstName, ww.LastName, ww.UserWantBooks}).ToList();
            var books = db.UserWantBooks.Where(ww => ww.UserId == id).OrderByDescending(ww => ww.DateBookAdded).Select(ww => ww.Book).ToList();
            //if (user.Count == 0)
            //{
            //    return NotFound();
            //}
            return Ok(books);
        }
        [ResponseType(typeof(User))]
        [HttpGet]
        [Route("api/user/having/{id:int}")]
        public IHttpActionResult GetHavingBooks(int id, int PageNumber, int pagSize)
        {
            //var user = db.User_Book.Where(ww => ww.User_Id == id && ww.Want == false).Select(ww => new { ww.User.First_Name, ww.User.Last_Name, ww.Book.Title }).ToList();
            //var user = db.Users.Where(ww => ww.UserId == id).Select(ww => new { ww.FirstName, ww.LastName, ww.UserHaveBooks }).ToList();
            //var books = db.Users.Include("Book").Where(user => user.UserId == id).Select(user => user.UserHaveBooks);
            
            var books = db.UserHaveBooks.Include("Book").Where(user => user.UserId == id).Select(uhb=> uhb.Book).ToList();
            //if (user.Count == 0)
            //{
            //    return NotFound();
            //}
            
            return Ok(books);
        }
        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}