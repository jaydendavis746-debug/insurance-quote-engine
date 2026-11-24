using FluentAssertions;
using QuoteEngine.Core.Enums;
using QuoteEngine.Core.Models;
using QuoteEngine.Core.Rules;


namespace QuoteEngine.Tests.Unit.Rules;

public class BasePremiumRuleTests
{
    private readonly BasePremiumRule _sut = new();

    [Theory]
    // PolicyType, SumInsured, ExpectedBasePremium
    [InlineData(PolicyType.Property, 100_000, 200.00)]                
    [InlineData(PolicyType.ProfessionalIndemnity, 100_000, 500.00)]   
    [InlineData(PolicyType.PublicLiability, 200_000, 200.00)]          
    public void Apply_ForDifferentPolicyTypes_CalculatesCorrectBasePremium(
        PolicyType policyType,
        decimal sumInsured,
        decimal expectedPremium)
    {
        // Arrange
        var request = new QuoteRequest("QUO-TEST", policyType, sumInsured, 30, 0);
        var calculation = new QuoteCalculation(request);

        // Act
        _sut.Apply(calculation);

        // Assert
        calculation.BasePremium.Should().Be(expectedPremium);
    }

    [Theory]
    // Boundary testing for Minimum Premium Floors:
    // Property Floor is £150 (threshold is £75k)
    [InlineData(PolicyType.Property, 50_000, 150.00)]   // Below boundary: 50k * 0.2% = £100 -> Floor £150
    [InlineData(PolicyType.Property, 75_000, 150.00)]   // Exactly on boundary: 75k * 0.2% = £150
    [InlineData(PolicyType.Property, 80_000, 160.00)]   // Above boundary: 80k * 0.2% = £160
    // Professional Indemnity Floor is £250 (threshold is £50k)
    [InlineData(PolicyType.ProfessionalIndemnity, 20_000, 250.00)]  // Below boundary -> Floor £250
    [InlineData(PolicyType.ProfessionalIndemnity, 50_000, 250.00)]  // Exactly on boundary -> £250
    // Public Liability Floor is £100 (threshold is £100k)
    [InlineData(PolicyType.PublicLiability, 50_000, 100.00)]       // Below boundary -> Floor £100
    public void Apply_WhenCalculatedPremiumIsBelowFloor_AppliesMinimumPremiumFloor(
        PolicyType policyType,
        decimal sumInsured,
        decimal expectedFloorPremium)
    {
        // Arrange
        var request = new QuoteRequest("QUO-FLOOR", policyType, sumInsured, 30, 0);
        var calculation = new QuoteCalculation(request);

        // Act
        _sut.Apply(calculation);

        // Assert
        calculation.BasePremium.Should().Be(expectedFloorPremium);
    }
}
