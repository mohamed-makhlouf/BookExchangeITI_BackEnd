using Final_Project_Code_First.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_Project_Code_First.Controllers
{
    
    public class ModifiedRequestController : ApiController
    {
        private BookExchangeModel db = new BookExchangeModel();

        [Authorize]
        public IHttpActionResult GetRecievedRequests(int pageNumber,int pageSize)
        {
            var currentUSerId =UserUtilities.GetCurrentUserId(User);
            var count = db.Requests
                .Include("Book")
                .Include("User")
                .Include("RequestStaus")
                .Where(req => req.RecieverId == currentUSerId || req.SenderId == currentUSerId).Count();
            var requests = db.Requests
                .Include("Book")
                .Include("User")
                .Include("RequestStaus")
                .Where(req => req.RecieverId == currentUSerId || req.SenderId == currentUSerId)
                .OrderByDescending(req=>req.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize )
                .Select(req=> new { req.Id,
                    req.DateOfMessage,
                    req.SendSwapUsertId,
                    //req.SenderId,
                    RequestedUser = new
                    {
                        req.RecieverUser.UserId,
                        req.RecieverUser.FirstName,
                        req.RecieverUser.LastName,
                        req.RecieverUser.PhotoUrl
                    },
                    SenderUser = new { 
                        req.SenderUser.UserId,
                        req.SenderUser.FirstName,
                        req.SenderUser.LastName,
                        req.SenderUser.PhotoUrl
                    },
                    RequestedBook = new {
                        req.RequestedBook.Book_Id,
                        req.RequestedBook.Title,
                        req.RequestedBook.Photo_Url,
                    },
                    SendedBook=new {
                         req.SendedBook.Book_Id,
                         req.SendedBook.Title,
                         req.SendedBook.Photo_Url,
                    },
                    RequestStatus = req.RequestStatusId


                })
                .ToList();
            return Ok(requests);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult PostNewRequest(Request request)
        {
            request.RequestStatusId = RequestStatusEnum.Requested;
            request.DateOfMessage = DateTime.Now;

            return Ok();
        }

        [HttpGet]
        [Route("api/home/have")]
        public IHttpActionResult GetWantedHaveBooks(int userId, int pageNumber, int pageSize)
        {

            if (userId == -1)
            {
                var count = db.UserHaveBooks
               .Count();
                var books = db.UserHaveBooks
                   .OrderByDescending(ww => ww.DateOfAdded)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .Select(ww => new
                   {
                       Book = new
                       {
                           ww.Book.Book_Id,
                           ww.Book.Title,
                           ww.Book.Photo_Url,
                           ww.Book.Author_Name,
                           ww.Book.Description,
                           ww.BookCondition.Name
                       }
                           ,
                       User = new
                       {
                           ww.User.FirstName,
                           ww.User.UserId,
                           ww.User.PhotoUrl,
                           ww.User.LastName
                       }
                   })
                   .ToList();
                return Ok(new { count, books });
            }
            else
            {
                var count = db.UserHaveBooks
              .Count();
                var books = db.UserHaveBooks
                    .Where(ww => ww.UserId == userId)
                   .OrderByDescending(ww => ww.DateOfAdded)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .Select(ww => new
                   {
                       Book = new
                       {
                           ww.Book.Book_Id,
                           ww.Book.Title,
                           ww.Book.Photo_Url,
                           ww.Book.Author_Name,
                           ww.Book.Description,
                           ww.BookCondition.Name
                       }
                           ,
                       User = new
                       {
                           ww.User.FirstName,
                           ww.User.UserId,
                           ww.User.PhotoUrl,
                           ww.User.LastName
                       }
                   })
                   .ToList();
                return Ok(new { count, books });
            }
        }


        [HttpGet]
        [Route("api/home/genres")]
        public IHttpActionResult GetBooksByGenresId(int pageNumber, int pageSize, string type,int genreId)
        {
            //var count = db.Genres.Count();
            if (type == "have")
            {
                var count = db.GenreBooks.Where(ww => ww.GenreId == genreId).Count();
                var books = db.GenreBooks
                   .Include("User")
                   .Include("Book")
                    .Where(ww=> ww.GenreId == genreId)
                   .Join(db.UserHaveBooks,
                        g=> g.BookId,
                        uhb => uhb.BookId,
                        (g,uhb) => uhb 
                   )
                   .OrderByDescending((ww) =>  ww.DateOfAdded)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .Select(ww => new
                   {
                       ww.DateOfAdded,
                       Book = new
                       {
                           ww.Book.Book_Id,
                           ww.Book.Title,
                           ww.Book.Photo_Url,
                           ww.Book.Author_Name,
                           ww.Book.Description,
                           ww.BookCondition.Name
                       }
                           ,
                       User = new
                       {
                           ww.User.FirstName,
                           ww.User.UserId,
                           ww.User.PhotoUrl,
                           ww.User.LastName
                       }
                   })
                   .ToList();
                var x = 10;
                return Ok(new { count, books });
            }
            else if (type == "want")
            {
                var count = db.GenreBooks.Where(ww => ww.GenreId == genreId).Count();
          
                var books = db.GenreBooks
                   .Where(ww => ww.GenreId == genreId)
                   .Join(db.UserWantBooks,
                        g => g.BookId,
                        uhb => uhb.BookId,
                        (g, uhb) => new { uhb }
                   )
                   .OrderByDescending(ww => ww.uhb.DateBookAdded)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .Select(ww => new
                   {
                       Book = new
                       {
                           ww.uhb.Book.Book_Id,
                           ww.uhb.Book.Title,
                           ww.uhb.Book.Photo_Url,
                           ww.uhb.Book.Author_Name,
                           ww.uhb.Book.Description
                       }
                           ,
                       User = new
                       {
                           ww.uhb.User.FirstName,
                           ww.uhb.User.UserId,
                           ww.uhb.User.PhotoUrl,
                           ww.uhb.User.LastName
                       }
                   })
                   .ToList();
                var x = 10;
                return Ok(new { count, books });
            }
            return NotFound();
        }
        [HttpGet]
        [Route("api/home/want")]
        public IHttpActionResult GetWantedHaveBooks2(int userId, int pageNumber, int pageSize)
        {

            if (userId == -1)
            {
                var count = db.UserWantBooks
               .Count();
                var books = db.UserWantBooks
                   .OrderByDescending(ww => ww.DateBookAdded)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .Select(ww => new
                   {
                       Book = new
                       {
                           ww.Book.Book_Id,
                           ww.Book.Title,
                           ww.Book.Photo_Url,
                           ww.Book.Author_Name,
                           ww.Book.Description
                                                  
                       }
                           ,
                       User = new
                       {
                           ww.User.FirstName,
                           ww.User.UserId,
                           ww.User.PhotoUrl,
                           ww.User.LastName
                       }
                   })
                   .ToList();
                return Ok(new { count, books });
            }
            else
            {
                var count = db.UserWantBooks
              .Count();
                var books = db.UserWantBooks
                    .Where(ww => ww.UserId == userId)
                   .OrderByDescending(ww => ww.DateBookAdded)
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .Select(ww => new
                   {
                       Book = new
                       {
                           ww.Book.Book_Id,
                           ww.Book.Title,
                           ww.Book.Photo_Url,
                           ww.Book.Author_Name,
                           ww.Book.Description
                         
                       }
                           ,
                       User = new
                       {
                           ww.User.FirstName,
                           ww.User.UserId,
                           ww.User.PhotoUrl,
                           ww.User.LastName
                       }
                   })
                   .ToList();
                return Ok(new { count, books });
            }
        }
    
        [HttpGet]
        [Route("api/home/book/have")]
        public IHttpActionResult GetUserHaveBook(int bookId)
        {
            
                var count = db.UserHaveBooks
                .Where(ww => ww.BookId == bookId)
                            .Count();
                var users = db.UserHaveBooks
                    .Where(ww => ww.BookId == bookId)
                   .OrderByDescending(ww => ww.DateOfAdded)
                   //.Skip((pageNumber - 1) * pageSize)
                   //.Take(pageSize)
                   .Select(ww => new
                   {
                       Book = new
                       {
                           ww.Book.Book_Id,
                           ww.Book.Title,
                           ww.Book.Photo_Url,
                           ww.Book.Author_Name,
                           ww.BookCondition.Name,
                           ww.Book.Description
                       }
                           ,
                       User = new
                       {
                           ww.User.FirstName,
                           ww.User.UserId,
                           ww.User.PhotoUrl,
                           ww.User.LastName
                       }
                   })
                   .ToList();
                return Ok(new { count, users });
           
        }
    }
}
