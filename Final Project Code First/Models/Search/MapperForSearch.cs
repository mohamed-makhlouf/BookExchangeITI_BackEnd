using Final_Project_Code_First.Models.Search.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_Code_First.Models.Search
{
    public static class MapperForSearch
    {
        public static List<Book> MapGoogleResponse(GoogleResponse response)
        {
            return response.Items.OrderBy(ww => ww.VolumeInfo.Title)
                .Take(10)
                .Select(ww => new Book()
                {
                    Title = ww?.VolumeInfo?.Title,
                    Photo_Url = ww?.VolumeInfo?.ImageLinks == null ? "NotFound" : ww.VolumeInfo.ImageLinks.Thumbnail,
                    Author_Name = ww?.VolumeInfo?.Authors != null ? ww.VolumeInfo.Authors[0] : "NotFound",
                    Description = ww?.VolumeInfo?.Description != null ? ww?.VolumeInfo?.Description : "NotFound",
                    
                    
                }
                ).ToList();
        }
    }
}