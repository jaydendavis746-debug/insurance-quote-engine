using QuoteEngine.Core.Enums;

namespace QuoteEngine.Core.Models;

public record QuoteRequest(
    string ReferenceId,
    PolicyType PolicyType,
    decimal SumInsured,
    int ApplicantAge,
    int ClaimsInPast3Years
);
