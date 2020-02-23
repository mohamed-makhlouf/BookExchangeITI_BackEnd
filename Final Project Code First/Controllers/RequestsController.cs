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
    public class RequestsController : ApiController
    {
        private BookExchangeModel db = new BookExchangeModel();

        // GET: api/Requests
        public IHttpActionResult GetRequests()
        {
            var requests = db.Requests.ToList();
            return Ok(requests);
        }

        // GET: api/Requests/5
        [ResponseType(typeof(Request))]
        public IHttpActionResult GetRequest(int id)
        {
            var request = db.Requests.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }


        [ResponseType(typeof(Request))]
        [Route("api/Requests/GetRequestById/{Req_id:int}")]
        public IHttpActionResult GetRequestById(int Req_id)
        {
            var request = db.Requests.Where(ww => ww.RecieverId == Req_id).Select(BB => new
            {
                BB.SenderId,
                BB.Id,
                BB.RecieverId,
                BB.RequestStaus,
                BB.Swap,
                BB.BookId,
                BB.DateOfMessage,
                senderUser = new { BB.SenderId, BB.SenderUser.FirstName },
                recieverUser = new { BB.RecieverId, BB.RecieverUser.FirstName }
            }).FirstOrDefault();
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }


        [ResponseType(typeof(Request))]
        [Route("api/Requests/GetBySender_Id/{send_id:int}")]
        public IHttpActionResult GetBySenderId(int send_id)
        {
            var request = db.Requests.Where(BB => BB.SenderId == send_id).Select(BB => new
            {
                BB.SenderId,
                BB.RequestStatusId,
                BB.RequestStaus,
                BB.Swap,
                BB.BookId,
                BB.DateOfMessage,
                senderUser = new { BB.SenderId, BB.SenderUser.FirstName },
                recieverUser = new { BB.RecieverId, BB.RecieverUser.FirstName },
            }).ToList();
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        [ResponseType(typeof(Request))]
        [Route("api/Requests/GetByStatus")]
        public IHttpActionResult GetByStatIdAndSendId(RequestStatusEnum status_num, int sender_id)
        {
            var request = db.Requests.Where(ss => ss.RequestStatusId == status_num && ss.SenderId == sender_id).Select(BB => new {
                BB.SenderId,
                BB.Id,
                BB.RequestStaus,
                BB.Swap,
                BB.BookId,
                BB.DateOfMessage,
                BB.RecieverId,
                senderUser = new { BB.SenderId, BB.SenderUser.FirstName },
                recieverUser = new { BB.RecieverId, BB.RecieverUser.FirstName },

            }).ToList();

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }


        // PUT: api/Requests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRequest(int id, Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            db.Entry(request).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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
        //Add Some Validation
        // POST: api/Requests
        [Route("api/request/add")]
        [ResponseType(typeof(Request))]
        public IHttpActionResult PostRequest(Request request)
        {

            request.DateOfMessage = DateTime.UtcNow;
            request.RequestStatusId = RequestStatusEnum.Requested;
            var checkHaveBook = db.UserHaveBooks.Where(ww=>ww.BookId==request.BookId&&ww.UserId == request.SenderId).Select(ww => ww.BookId).ToList();
            var checkWantBook = db.UserWantBooks.Where(ww => ww.BookId==request.BookId&&ww.UserId==request.RecieverId).Select(ww=>ww.BookId).ToList();
            if(checkHaveBook.Count>=0&&checkWantBook.Count>=0)
            {
                db.Requests.Add(request);
                db.SaveChanges();
            }
            else if(request.RequestStaus.Id!= RequestStatusEnum.Requested)
            {
                db.Requests.Add(request);
                db.SaveChanges();
            }

            return StatusCode(HttpStatusCode.Created);
        }

        [HttpPost]
        [Route("api/Requests/Create")]
        public IHttpActionResult CreateRequest(int sentId,int receiveId,int bookId,DateTime date,RequestStaus requestStaus)
        {
            var resultRequests= db.Requests.Add(new Request { SenderId = sentId, RecieverId = receiveId, BookId = bookId, DateOfMessage = date });
            db.SaveChanges();
            if(resultRequests==null)
            {
                return NotFound();
            }
            return Ok(resultRequests);

        }


        [HttpPost]
        [Route("api/Requests")]
        public IHttpActionResult CreateRequest2(int sentId, int receiveId, int bookId)
        {
            
            var resultRequests = db.Requests.Add(new Request { SenderId = sentId, RecieverId = receiveId, BookId = bookId, DateOfMessage = DateTime.Now,RequestStatusId = RequestStatusEnum.Requested });
            db.SaveChanges();
            if (resultRequests == null)
            {
                return NotFound();
            }
            return Ok(resultRequests);
        }

        [HttpPut]
        [Route("api/Request/Accept")]
        public IHttpActionResult CreateAcceptReq(int id)
        {
            var resultRequests = db.Requests.Include("RequestStaus").Where(ww => ww.Id == id).FirstOrDefault();
            if (resultRequests == null)
            {
                return NotFound();
            }
            else
            {
                resultRequests.RequestStatusId = RequestStatusEnum.Accepted;
                db.Entry(resultRequests).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(new
                {
                    resultRequests.Id,
                    resultRequests.DateOfMessage,
                    resultRequests.SenderId,
                    RequestedUser = new
                    {
                        resultRequests.RecieverUser.UserId,
                        resultRequests.RecieverUser.FirstName,
                        resultRequests.RecieverUser.LastName,
                        resultRequests.RecieverUser.PhotoUrl
                    },
                    SenderUser = new
                    {
                        resultRequests.SenderUser.UserId,
                        resultRequests.SenderUser.FirstName,
                        resultRequests.SenderUser.LastName,
                        resultRequests.SenderUser.PhotoUrl
                    },
                    RequestedBook = new
                    {
                        resultRequests.RequestedBook.Book_Id,
                        resultRequests.RequestedBook.Title,
                        resultRequests.RequestedBook.Photo_Url,
                    },
                    SendedBook = new
                    {
                        resultRequests.SendedBook.Book_Id,
                        resultRequests.SendedBook.Title,
                        resultRequests.SendedBook.Photo_Url,
                    },
                    RequestStatus = resultRequests.RequestStatusId

                });
            }
        }

        [HttpPut]
        [Route("api/Request/Refuse")]
        public IHttpActionResult CreateRefuseReq(int id)
        {
            var resultrequest = db.Requests.Where(ww => ww.Id == id).FirstOrDefault();
            if(resultrequest==null)
            {
                return NotFound();
            }
            else
            {
                resultrequest.RequestStatusId = RequestStatusEnum.Refused;
                db.Entry(resultrequest).State = EntityState.Modified;
                db.SaveChanges();

                return Ok(new
                {
                    resultrequest.Id,
                    resultrequest.DateOfMessage,
                    resultrequest.SenderId,
                    RequestedUser = new
                    {
                        resultrequest.RecieverUser.UserId,
                        resultrequest.RecieverUser.FirstName,
                        resultrequest.RecieverUser.LastName,
                        resultrequest.RecieverUser.PhotoUrl
                    },
                    SenderUser = new
                    {
                        resultrequest.SenderUser.UserId,
                        resultrequest.SenderUser.FirstName,
                        resultrequest.SenderUser.LastName,
                        resultrequest.SenderUser.PhotoUrl
                    },
                    RequestedBook = new
                    {
                        resultrequest.RequestedBook.Book_Id,
                        resultrequest.RequestedBook.Title,
                        resultrequest.RequestedBook.Photo_Url,
                    },
                    SendedBook = new
                    {
                        resultrequest.SendedBook.Book_Id,
                        resultrequest.SendedBook.Title,
                        resultrequest.SendedBook.Photo_Url,
                    },
                    RequestStatus = resultrequest.RequestStatusId

                });
            }
        }

        [HttpPut]
        [Route("api/Requests/AcceptSwap")]
        public IHttpActionResult CreateAcceptSwapReq(int id)
        {
            var resultRequest = db.Requests.Where(ww => ww.Id == id).FirstOrDefault();
            if(resultRequest==null)
            {
                return NotFound();
            }
            else
            {
                resultRequest.RequestStatusId = RequestStatusEnum.AcceptSwap;
                db.Entry(resultRequest).State = EntityState.Modified;
                var userHaveBookCheck = db.UserHaveBooks.Where(uhb => uhb.UserId == resultRequest.SenderId && uhb.BookId == resultRequest.BookId).FirstOrDefault();
                if(userHaveBookCheck == null)
                {
                    return StatusCode(HttpStatusCode.NotAcceptable);
                }
                var userWantBookCheck = db.UserWantBooks.Where(uwb => uwb.UserId == resultRequest.RecieverId && uwb.BookId == resultRequest.RequestedBookId).FirstOrDefault();
                if(userWantBookCheck == null)
                {
                    return StatusCode(HttpStatusCode.NotAcceptable);

                }
                var temp = new UserHaveBook() { BookId = userWantBookCheck.BookId, UserId = userWantBookCheck.UserId , BookConditionId = userHaveBookCheck.BookConditionId};
                var temp2 = new UserWantBook() { BookId = userHaveBookCheck.BookId, UserId = userHaveBookCheck.UserId };
                db.Entry(temp).State = EntityState.Added;
                db.Entry(temp2).State = EntityState.Added;
                db.Entry(userHaveBookCheck).State = EntityState.Deleted;
                db.Entry(userWantBookCheck).State = EntityState.Deleted;
                db.SaveChanges();

                return Ok(new
                {
                    resultRequest.Id,
                    resultRequest.DateOfMessage,
                    resultRequest.SenderId,
                    resultRequest = new
                    {
                        resultRequest.RecieverUser.UserId,
                        resultRequest.RecieverUser.FirstName,
                        resultRequest.RecieverUser.LastName,
                        resultRequest.RecieverUser.PhotoUrl
                    },
                    SenderUser = new
                    {
                        resultRequest.SenderUser.UserId,
                        resultRequest.SenderUser.FirstName,
                        resultRequest.SenderUser.LastName,
                        resultRequest.SenderUser.PhotoUrl
                    },
                    RequestedBook = new
                    {
                        resultRequest.RequestedBook.Book_Id,
                        resultRequest.RequestedBook.Title,
                        resultRequest.RequestedBook.Photo_Url,
                    },
                    SendedBook = new
                    {
                        resultRequest.SendedBook.Book_Id,
                        resultRequest.SendedBook.Title,
                        resultRequest.SendedBook.Photo_Url,
                    },
                    RequestStatus = resultRequest.RequestStatusId

                });
            }

        }

        [HttpPut]
        [Route("api/Requests/RequestSwap")]
        public IHttpActionResult CreateRequest6(int id)
        {
            var resultRequest = db.Requests.Where(ww => ww.Id == id).FirstOrDefault();
            if(resultRequest==null)
            {
                return NotFound();
            }
            else
            {
                resultRequest.RequestStatusId = RequestStatusEnum.RequestSwap;
                
                db.Entry(resultRequest).State = EntityState.Modified;
                db.SaveChanges();

                return Ok(new
                {
                    resultRequest.Id,
                    resultRequest.DateOfMessage,
                    resultRequest.SenderId,
                    RequestedUser = new
                    {
                        resultRequest.RecieverUser.UserId,
                        resultRequest.RecieverUser.FirstName,
                        resultRequest.RecieverUser.LastName,
                        resultRequest.RecieverUser.PhotoUrl
                    },
                    SenderUser = new
                    {
                        resultRequest.SenderUser.UserId,
                        resultRequest.SenderUser.FirstName,
                        resultRequest.SenderUser.LastName,
                        resultRequest.SenderUser.PhotoUrl
                    },
                    RequestedBook = new
                    {
                        resultRequest.RequestedBook.Book_Id,
                        resultRequest.RequestedBook.Title,
                        resultRequest.RequestedBook.Photo_Url,
                    },
                    SendedBook = new
                    {
                        resultRequest.SendedBook.Book_Id,
                        resultRequest.SendedBook.Title,
                        resultRequest.SendedBook.Photo_Url,
                    },
                    RequestStatus = resultRequest.RequestStatusId

                });
            }
        }


        // DELETE: api/Requests/5
        [ResponseType(typeof(Request))]
        public IHttpActionResult DeleteRequest(int id)
        {
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            db.Requests.Remove(request);
            db.SaveChanges();

            return Ok(request);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestExists(int id)
        {
            return db.Requests.Count(e => e.Id == id) > 0;
        }
    }
}


