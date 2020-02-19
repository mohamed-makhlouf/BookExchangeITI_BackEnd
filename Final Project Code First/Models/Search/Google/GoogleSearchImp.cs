using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Final_Project_Code_First.Models.Search.Google
{
    public class GoogleSearchImp
    {
        private string apiKey = "AIzaSyCMOutpopp4OfWcDz-S8dcLxNWRf2sxayU";
        HttpClient client;
        NameValueCollection query;
        public GoogleSearchImp()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://www.googleapis.com/");
            query = HttpUtility.ParseQueryString(string.Empty);
            query["key"] = apiKey;

        }

        public List<Book> QuickSearchByName(string name)
        {
            var message = client.GetAsync("books/v1/volumes?" + query.ToString()).Result;
            if (message.IsSuccessStatusCode)
            {
                GoogleResponse response = message.Content.ReadAsAsync<GoogleResponse>().Result;
                return MapperForSearch.MapGoogleResponse(response);
            }
            throw new NotImplementedException();
        }

        public List<Book> SearchByName(string name)
        {
            query["q"] = name;
            var message = client.GetAsync("books/v1/volumes?" + query.ToString()).Result;
            if (message.IsSuccessStatusCode)
            {
                GoogleResponse response = message.Content.ReadAsAsync<GoogleResponse>().Result;
                return MapperForSearch.MapGoogleResponse(response);
            }
            return null;
        }

    }
}