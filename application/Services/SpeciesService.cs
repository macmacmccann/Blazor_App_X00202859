using static System.Net.WebRequestMethods;


namespace application.Services
{
    public class SpeciesService
    {
   
        public List<Species> species = new();

        public async Task LoadSpeciesAsync(HttpClient http)
        {
           var response = await http.GetFromJsonAsync<SpeciesSearchResponse>("https://api.gbif.org/v1/species?rank=SPECIES&limit=100");
           species = response?.results ?? new List<Species>();
        }




        public async Task PopulateWithWikipedia(WikipediaService wiki)
        // this makes it a global list 
        {

            foreach (var s in species)
            {
                var (image, extract) = await wiki.GetInfoAsync(s);

                s.wikiImage = image;
                s.wikiExtract = extract;

            }
        }



        public async Task PopulateWithWikipediaGlobally(WikipediaService wiki ,List<Species> list )
            // this makes it a global list 
            // i can call await Service.Populatewithwiki(wikiservicelocal,specieslist
        {
            
            foreach(var s in list)
            {
                var (image, extract) = await wiki.GetInfoAsync(s);
                
                s.wikiImage = image;
                s.wikiExtract = extract;

            }
        }





    }




}
