using QuoteEngine.Core.Enums;
using QuoteEngine.Core.Models;

namespace QuoteEngine.Core.Rules;

public class BasePremiumRule
{
    private const decimal PropertyRate = 0.002m;

    public void Apply(QuoteCalculation calculation)
    {
        if (calculation.Request.PolicyType == PolicyType.Property)
        {
            var basePremium = calculation.Request.SumInsured * PropertyRate;
            calculation.BasePremium = basePremium;
            calculation.AppliedRules.Add("BasePremium: Property @ 0.2%");
        }
    }
}
