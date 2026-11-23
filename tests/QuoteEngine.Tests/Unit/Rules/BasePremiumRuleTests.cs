using FluentAssertions;
using QuoteEngine.Core.Enums;
using QuoteEngine.Core.Models;
using QuoteEngine.Core.Rules;


namespace QuoteEngine.Tests.Unit.Rules;

public class BasePremiumRuleTests
{
    private readonly BasePremiumRule _sut = new(); 

    [Fact]
    public void Apply_ForPropertyInsurance_CalculatesCorrectBasePremium()
    {

        var request = new QuoteRequest(
            ReferenceId: "QUO-001",
            PolicyType: PolicyType.Property,
            SumInsured: 100000m,
            ApplicantAge: 35,
            ClaimsInPast3Years: 0
        );
        var calculation = new QuoteCalculation(request);

        _sut.Apply(calculation);

       
        calculation.BasePremium.Should().Be(200.00m);
        calculation.AppliedRules.Should().Contain("BasePremium: Property @ 0.2%");
    }
}
