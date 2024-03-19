using System.Text.Json.Serialization;

namespace ThesisCatalog.Core.Entities;

public record Weight
{
    private const double PoundsConversionFactor = 453.59237;
    public int Grams { get; set; }
    public WeightUnit DisplayUnit { get; set; }
    
    [JsonIgnore]
    public double DisplayValue =>  DisplayUnit switch
        {
            WeightUnit.Grams => Grams,
            WeightUnit.Kilograms => Grams / 1000d,
            WeightUnit.Pounds => Math.Round(Grams / PoundsConversionFactor),
            _ => throw new ArgumentOutOfRangeException()
        };

    public Weight(double amount, WeightUnit displayUnit)
    {
        var gramsAmount = ConvertToGrams(amount, displayUnit);
        Grams = gramsAmount;
        DisplayUnit = displayUnit;
    }

    [JsonConstructor]
    public Weight(int grams, WeightUnit displayUnit)
    {
        Grams = grams;
        DisplayUnit = displayUnit;
    }

    public override string ToString()
    {
        var amount = DisplayUnit switch
        {
            WeightUnit.Grams => Convert.ToDouble(Grams),
            WeightUnit.Kilograms => Convert.ToDouble(Grams) / 1000,
            WeightUnit.Pounds => Convert.ToDouble(Grams) / PoundsConversionFactor,
            _ => throw new ArgumentOutOfRangeException()
        };

        var suffix = DisplayUnit switch
        {
            WeightUnit.Grams => "g",
            WeightUnit.Kilograms => "kg",
            WeightUnit.Pounds => "lb",
            _ => string.Empty
        };

        return $"{amount:N1}{suffix}";
    }

    private static int ConvertToGrams(double amount, WeightUnit fromUnit)
    {
        Func<double, int> conversionFunction = fromUnit switch
        {
            WeightUnit.Pounds => PoundsToGrams,
            WeightUnit.Kilograms => d => Convert.ToInt16(Math.Round(d * 1000)),
            WeightUnit.Grams => d => Convert.ToInt16(Math.Round(d)),
        };

        return conversionFunction.Invoke(amount);
    }
    
    private static int PoundsToGrams(double amount)
    {
        var result = amount * PoundsConversionFactor;
        return Convert.ToInt16(Math.Round(result));
    }
}