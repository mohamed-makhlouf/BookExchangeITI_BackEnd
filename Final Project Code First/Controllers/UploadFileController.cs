using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using Final_Project_Code_First.Models;

namespace FileUpload.Controllers
{
    public class DocFileController : ApiController
    {
        private BookExchangeModel db = new BookExchangeModel();
        

        [HttpPost]
        [Route("api/UploadFile/Book")]
        public HttpResponseMessage PostBookFile()
        {
            HttpResponseMessage result = null;
            //var bookResult= _BookDb.Books.Where(ww=>ww.Book_Id==id)

            var httpRequest = HttpContext.Current.Request;
            var name = HttpContext.Current.Request.Form.Get("name");

            int bookId = int.Parse(HttpContext.Current.Request.Form.Get("bookId"));
            string title = HttpContext.Current.Request.Form.Get("title");
            int rate= int.Parse(HttpContext.Current.Request.Form.Get("rate"));
            string authName= HttpContext.Current.Request.Form.Get("authName");
            if (httpRequest.Files.Count > 0)
            {
                var filePath = "";
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var userId = db.Users.Select(ww => ww.UserId);
                     filePath = HttpContext.Current.Server.MapPath("~/Upload/BookImages/" + userId+postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                db.Books.Add(new Book { Book_Id = bookId, Title = title, Rate = rate, Photo_Url = filePath,Author_Name=authName }) ;
                db.SaveChanges();
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        [HttpPost]
        [Route("api/UploadFile/User")]
        public HttpResponseMessage PostUserFile()
        {
            HttpResponseMessage result = null;
            //var bookResult= _BookDb.Books.Where(ww=>ww.Book_Id==id)

            var httpRequest = HttpContext.Current.Request;
            var name = HttpContext.Current.Request.Form.Get("name");

            int userId = int.Parse(HttpContext.Current.Request.Form.Get("userId"));
            string firstName = HttpContext.Current.Request.Form.Get("firstName");
            string lastName = HttpContext.Current.Request.Form.Get("lastName");
            string email = HttpContext.Current.Request.Form.Get("email");
            string password = HttpContext.Current.Request.Form.Get("password");
            int rate = int.Parse(HttpContext.Current.Request.Form.Get("rate"));
            string city = HttpContext.Current.Request.Form.Get("city");
            string address = HttpContext.Current.Request.Form.Get("address");
            double phoneNumber = double.Parse(HttpContext.Current.Request.Form.Get("phoneNumber"));

            if (httpRequest.Files.Count > 0)
            {
                var filePath = "";
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    //var userId = db.Users.Select(ww => ww.UserId);
                    filePath = HttpContext.Current.Server.MapPath("~/Upload/UserImages/" + userId + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
             
                db.Users.Add(new User { UserId = userId, FirstName = firstName, LastName = lastName, Email=email, Password=password,Address=address,City=city, Rate=rate,PhoneNumber=phoneNumber,PhotoUrl = filePath });
                db.SaveChanges();
                
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }



    }
}