

public class Species
{
    public required int key { get; set; }
    public required string scientificName { get; set; }
    public required string canonicalName { get; set; }

}

public class SpeciesSearchResponse
{
    public required List<Species> results { get; set; }
}


