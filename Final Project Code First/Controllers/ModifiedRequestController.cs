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
            pageNumber = pageNumber == 0 ? 1 : 1;
            var requests = db.Requests
                //.Include("Book")
                .Where(req => req.RecieverId == currentUSerId)
                .OrderByDescending(req=>req.DateOfMessage)
                .Skip((pageNumber - 1)* pageSize)
                .Take(pageSize)

                .Select(req=> new { req.Id,
                    req.DateOfMessage,
                    req.SenderId,
                    SenderUser = new { 
                        req.SenderUser.UserId,
                        req.SenderUser.FirstName,
                        req.SenderUser.LastName,
                        req.SenderUser.PhotoUrl,},
                    RequestedBook = new {
                        req.SendedBook.Book_Id,
                        req.SendedBook.Title,
                        req.SendedBook.Photo_Url,
                    },
                    RequestStatus=req.RequestStaus.Name

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
                           ww.Book.Author_Name
                         
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
    }
}
