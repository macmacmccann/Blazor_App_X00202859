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

    }
}
