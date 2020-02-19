using Final_Project_Code_First.Models;
using Final_Project_Code_First.Models.Search.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Final_Project_Code_First.Controllers
{
    //[RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        private BookExchangeModel db = new BookExchangeModel();
        GoogleSearchImp googleSearch = new GoogleSearchImp();

        [HttpGet]
        public IHttpActionResult GetSearchByName([FromUri]string name)
        {
            var book = db.Books.Where(BB => BB.Title.Contains(name)).ToList();
            if (book.Count == 0)
            {
                return Ok(googleSearch.SearchByName(name));
            }

            return Ok(book);
        }
    }
}
