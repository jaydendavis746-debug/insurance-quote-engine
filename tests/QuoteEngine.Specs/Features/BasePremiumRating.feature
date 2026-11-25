Feature: Base Premium Rating
As an underwriter
I want to rate base premiums according to policy type rules
So that each policy has an accurate starting premium

Scenario Outline: Quotes below floor threshold receive minimum premium
    Given a customer requests "<PolicyType>" insurance with a sum insured of <SumInsured>
    When the base premium is calculated
    Then the base premium should be <ExpectedPremium>

  Examples:
    | PolicyType            | SumInsured | ExpectedPremium |
    | Property              | 50000      | 150.00          |
    | ProfessionalIndemnity | 40000      | 250.00          |
    | PublicLiability       | 50000      | 100.00          |
