using FluentAssertions;
using QuoteEngine.Core.Enums;
using QuoteEngine.Core.Models;
using QuoteEngine.Core.Rules;
using Reqnroll;

namespace QuoteEngine.Specs.StepDefinitions;

[Binding]
public class BasePremiumRatingStepDefinitions
{
    private readonly BasePremiumRule _rule = new();
    private QuoteRequest? _request;
    private QuoteCalculation? _calculation;

    [Given("a customer requests {string} insurance with a sum insured of {decimal}")]
    public void GivenACustomerRequestsInsuranceWithASumInsuredOf(string policyTypeStr, decimal sumInsured)
    {
        // Parse the Gherkin string directly into our existing C# enum
        var policyType = Enum.Parse<PolicyType>(policyTypeStr, ignoreCase: true);

        _request = new QuoteRequest(
            ReferenceId: "BDD-001",
            PolicyType: policyType,
            SumInsured: sumInsured,
            ApplicantAge: 35,
            ClaimsInPast3Years: 0
        );
    }

    [When("the base premium is calculated")]
    public void WhenTheBasePremiumIsCalculated()
    {
        _request.Should().NotBeNull("Request must be initialized before calculation");
        _calculation = new QuoteCalculation(_request!);

        // Reuse our existing production business logic
        _rule.Apply(_calculation);
    }

    [Then("the base premium should be {decimal}")]
    public void ThenTheBasePremiumShouldBe(decimal expectedPremium)
    {
        _calculation.Should().NotBeNull("Calculation must be run before asserting");
        _calculation.BasePremium.Should().Be(expectedPremium);
    }
}
