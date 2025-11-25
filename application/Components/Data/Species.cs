

public class Species
{
    public required int key { get; set; }
    public required string scientificName { get; set; }
    public required string canonicalName { get; set; }
    //JsonPropertyName("vernacularName")] uncomment if its dif 
    public string? vernacularName { get; set; }  // laymans name used for search 



}

public class SpeciesSearchResponse
{
    public required List<Species> results { get; set; }
}


