namespace QuoteEngine.Core.Models;

public class QuoteCalculation
{
    public QuoteRequest Request { get; }
    public decimal BasePremium { get; set; }
    public decimal FinalPremium { get; set; }
    public List<string> AppliedRules { get; } = new();

    public QuoteCalculation(QuoteRequest request)
    {
        Request = request;
    }
}
