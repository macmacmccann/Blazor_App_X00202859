namespace application.Components.Data
{
    public class WikiQueryReponse
    {
        public WikiQuery? query {  get; set; }

    }

    public class WikiQuery
    {
        // kv pair : key is page id and value is page data itself 
        public Dictionary<string,WikiPage>? pages { get; set; }
    }

    // one actual  wikipedia page 
    public class WikiPage
    {
        public string? title { get; set; }
        public WikiThumbnail? thumbnail { get; set; }
        // the first extracted paragraph 
        public string? extract {  get; set; }
    }


    public class WikiThumbnail
    {
        // the actual source url for the image 
        public string? source { get; set; }
    }

}
