using QuoteEngine.Core.Enums;
using QuoteEngine.Core.Models;

namespace QuoteEngine.Core.Rules;

public class BasePremiumRule
{
    public void Apply(QuoteCalculation calculation)
    {
        var request = calculation.Request;

        var (rate, floor) = request.PolicyType switch
        {
            PolicyType.Property => (0.002m, 150.00m),
            PolicyType.ProfessionalIndemnity => (0.005m, 250.00m),
            PolicyType.PublicLiability => (0.001m, 100.00m),
            _ => throw new ArgumentOutOfRangeException(nameof(request.PolicyType), $"Unsupported policy type: {request.PolicyType}")
        };

        var calculatedPremium = request.SumInsured * rate;
        var finalBase = Math.Max(calculatedPremium, floor);

        calculation.BasePremium = finalBase;
        calculation.AppliedRules.Add($"BasePremium: {request.PolicyType} @ {rate:P1} (Floor: £{floor:F2})");
    }
}
