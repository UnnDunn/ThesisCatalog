namespace ThesisCatalog.Core.Entities;

public record Weight
{
    public int WeightAmount { get; set; }
    public WeightUnit Unit { get; set; }
}