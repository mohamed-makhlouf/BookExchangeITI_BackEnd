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
        private BookExchangeModel _BookDb = new BookExchangeModel();
        
        public HttpResponseMessage Post(int bookId,string title,int rate,string authName)
        {
            HttpResponseMessage result = null;
            //var bookResult= _BookDb.Books.Where(ww=>ww.Book_Id==id)
            var httpRequest = HttpContext.Current.Request;
            var name = HttpContext.Current.Request.Form.Get("name");
            if (httpRequest.Files.Count > 0)
            {
                var filePath = "";
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var userId = _BookDb.Users.Select(ww => ww.UserId);
                     filePath = HttpContext.Current.Server.MapPath("~/Upload/Image/"+ userId+postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }

                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                
                _BookDb.Books.Add(new Book { Book_Id = bookId, Title = title, Rate = rate, Photo_Url = filePath,Author_Name=authName }) ;
                    _BookDb.SaveChanges();
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }
    }
}