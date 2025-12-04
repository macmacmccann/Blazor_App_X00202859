namespace application.Services
{
    using application.Components.Data;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class WikipediaService
    {

        private readonly HttpClient httpClient;

        public WikipediaService(HttpClient httpClient) // inject http client 
        {
            this.httpClient = httpClient; //store the injected http client 
            this.httpClient.DefaultRequestHeaders.UserAgent
        .ParseAdd("MyBlazorApp/1.0 (https://example.com)");
        }

        //return image url and extracted text for given name 
        public async Task<(string imageUrl,string extract)> GetInfoAsync(string scientificName)
        {

            // Safety check for empty input 
            if (string.IsNullOrEmpty(scientificName))
                return ("", "");

            string url =
                "https://en.wikipedia.org/w/api.php" + // the api 
                "?format=json" +                       // return json
                "&action=query" +                      // the action is to query - retreive data  
                "&prop=pageimages|extracts" +          // request images and text extract 
                "&exintro=1" +                         // only the intro paragraph  
                "&explaintext=1" +                     // no html just text 
                "&piprop=thumbnail" +                  //include thumbnauil info  
                "&pithumbsize=600" +                   //thumbnail size 
                "&origin=*" +                            // wikpedia cors requirement 
                $"&titles={Uri.EscapeDataString(scientificName)}"; //page name eg., chordata 

            // Now actually request the data and use 

            var response = await httpClient.GetFromJsonAsync<Dictionary<string, JsonElement>>(url);


            if(response == null || !response.TryGetValue("query", out var queryElement))
            {
                return ("", "");
            }


            // WikiQuery Model is dicton of id and page (object w/ title thumbnail and extract ) 
            var query = JsonSerializer.Deserialize<WikiQuery>(queryElement.GetRawText());
            
            // Safety check : ensure pages exists
            if(query?.pages == null)
            {
                return ("", "");

            }
            //Api returns pages indexed by page id i want the first page 
            var page = query.pages.Values.FirstOrDefault();

            if(page == null) return ("", "");

            // null safe 
            string image = page.thumbnail?.source ?? "" ;

            string extract = page.extract ?? "";

            //return as a tuple 
            return ( image, extract);


        }






    }

}
