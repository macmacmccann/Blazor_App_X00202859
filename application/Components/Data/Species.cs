

using Microsoft.Extensions.Diagnostics.HealthChecks;

using System.Text.Json.Serialization; // for keyword class 
public class Species
{
    public required int key { get; set; }
    public required string scientificName { get; set; }
    public string? canonicalName { get; set; }
    //JsonPropertyName("vernacularName")] uncomment if its dif 
    public string? vernacularName { get; set; }  // laymans name used for search
    public string? kingdom { get; set; } // must contain null value when exiting construct
    public string? phylum { get; set; }
    [JsonPropertyName("class")]
    public string? classOfSpecies { get; set; }
    public string? order { get; set; }
    public string? family { get; set; }

    public string? genus { get; set; }
    public string? species { get; set; }
    public string? status { get; set; }
    public int numDescendants { get; set; }
    public string? wikiImage { get; set; }
    public string? wikiExtract { get; set; }

    //Api keys 

    public int? familykey { get; set; }
    public int? genuskey { get; set; }
    public int? orderkey { get; set; }
    [JsonPropertyName("Specieskey")]
    public int? subspecieskey { get; set; }
    public int? nubkey { get; set; }

    public List<VernacularNames>? vernacularNames { get; set; }

    public class VernacularNames
    {
        public string? vernacularName { get; set; }
        public string? language { get; set; }
    }

}

public class SpeciesSearchResponse
{
    public required List<Species> results { get; set; }
}

