using ThesisCatalog.Core.Entities;

namespace ThesisCatalog.API.Data.Entities;

public record WeightSpecification
{
    public long WeightGrams { get; set; }
    public WeightUnit WeightDisplayUnit { get; set; }
}