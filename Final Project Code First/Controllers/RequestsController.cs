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
   
        // POST: api/Requests
        [ResponseType(typeof(Request))]
        public IHttpActionResult PostRequest(Request request)
        {
            db.Requests.Add(request);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = request.Id }, request);
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
        [Route("api/Requests/Accept")]
        public IHttpActionResult CreateAcceptReq(int id)
        {

            var resultRequests = db.Requests.Where(ww => ww.Id == id).FirstOrDefault();
            if (resultRequests == null)
            {
                return NotFound();
            }
            else
            {
                resultRequests.RequestStatusId = RequestStatusEnum.Accepted;
                db.Entry(resultRequests).State = EntityState.Modified;
                return Ok(resultRequests);
            }
        }

        [HttpPut]
        [Route("api/Requests/Refuse")]
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
                return Ok(resultrequest);
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
                return Ok(resultRequest);
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
                return Ok(resultRequest);
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


