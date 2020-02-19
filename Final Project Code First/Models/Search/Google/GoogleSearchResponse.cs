using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project_Code_First.Models.Search.Google
{
    public class ReadingModes
    {

        [JsonProperty("text")]
        public bool Text { get; set; }

        [JsonProperty("image")]
        public bool Image { get; set; }
    }

    public class PanelizationSummary
    {

        [JsonProperty("containsEpubBubbles")]
        public bool ContainsEpubBubbles { get; set; }

        [JsonProperty("containsImageBubbles")]
        public bool ContainsImageBubbles { get; set; }
    }

    public class ImageLinks
    {

        [JsonProperty("smallThumbnail")]
        public string SmallThumbnail { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }

    public class IndustryIdentifier
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }
    }

    public class VolumeInfo
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }

        [JsonProperty("authors")]
        public IList<string> Authors { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("publishedDate")]
        public string PublishedDate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("readingModes")]
        public ReadingModes ReadingModes { get; set; }

        [JsonProperty("pageCount")]
        public int PageCount { get; set; }

        [JsonProperty("printType")]
        public string PrintType { get; set; }

        [JsonProperty("maturityRating")]
        public string MaturityRating { get; set; }

        [JsonProperty("allowAnonLogging")]
        public bool AllowAnonLogging { get; set; }

        [JsonProperty("contentVersion")]
        public string ContentVersion { get; set; }

        [JsonProperty("panelizationSummary")]
        public PanelizationSummary PanelizationSummary { get; set; }

        [JsonProperty("imageLinks")]
        public ImageLinks ImageLinks { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("previewLink")]
        public string PreviewLink { get; set; }

        [JsonProperty("infoLink")]
        public string InfoLink { get; set; }

        [JsonProperty("canonicalVolumeLink")]
        public string CanonicalVolumeLink { get; set; }

        [JsonProperty("industryIdentifiers")]
        public IList<IndustryIdentifier> IndustryIdentifiers { get; set; }

        [JsonProperty("categories")]
        public IList<string> Categories { get; set; }
    }

    public class ListPrice
    {

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }
    }

    public class RetailPrice
    {

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }
    }

    public class Offer
    {

        [JsonProperty("finskyOfferType")]
        public int FinskyOfferType { get; set; }

        [JsonProperty("listPrice")]
        public ListPrice ListPrice { get; set; }

        [JsonProperty("retailPrice")]
        public RetailPrice RetailPrice { get; set; }
    }

    public class SaleInfo
    {

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("saleability")]
        public string Saleability { get; set; }

        [JsonProperty("isEbook")]
        public bool IsEbook { get; set; }

        [JsonProperty("listPrice")]
        public ListPrice ListPrice { get; set; }

        [JsonProperty("retailPrice")]
        public RetailPrice RetailPrice { get; set; }

        [JsonProperty("buyLink")]
        public string BuyLink { get; set; }

        [JsonProperty("offers")]
        public IList<Offer> Offers { get; set; }
    }

    public class Epub
    {

        [JsonProperty("isAvailable")]
        public bool IsAvailable { get; set; }

        [JsonProperty("acsTokenLink")]
        public string AcsTokenLink { get; set; }
    }

    public class Pdf
    {

        [JsonProperty("isAvailable")]
        public bool IsAvailable { get; set; }

        [JsonProperty("acsTokenLink")]
        public string AcsTokenLink { get; set; }
    }

    public class AccessInfo
    {

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("viewability")]
        public string Viewability { get; set; }

        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }

        [JsonProperty("publicDomain")]
        public bool PublicDomain { get; set; }

        [JsonProperty("textToSpeechPermission")]
        public string TextToSpeechPermission { get; set; }

        [JsonProperty("epub")]
        public Epub Epub { get; set; }

        [JsonProperty("pdf")]
        public Pdf Pdf { get; set; }

        [JsonProperty("webReaderLink")]
        public string WebReaderLink { get; set; }

        [JsonProperty("accessViewStatus")]
        public string AccessViewStatus { get; set; }

        [JsonProperty("quoteSharingAllowed")]
        public bool QuoteSharingAllowed { get; set; }
    }

    public class SearchInfo
    {

        [JsonProperty("textSnippet")]
        public string TextSnippet { get; set; }
    }

    public class Item
    {

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("selfLink")]
        public string SelfLink { get; set; }

        [JsonProperty("volumeInfo")]
        public VolumeInfo VolumeInfo { get; set; }

        [JsonProperty("saleInfo")]
        public SaleInfo SaleInfo { get; set; }

        [JsonProperty("accessInfo")]
        public AccessInfo AccessInfo { get; set; }

        [JsonProperty("searchInfo")]
        public SearchInfo SearchInfo { get; set; }
    }

    public class GoogleResponse
    {
        public GoogleResponse()
        {
            Items = new List<Item>();
        }
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }

        [JsonProperty("items")]
        public IList<Item> Items { get; set; }

    }
}