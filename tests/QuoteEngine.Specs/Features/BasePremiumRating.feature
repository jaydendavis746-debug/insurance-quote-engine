Feature: Base Premium Rating
  As an insurance underwriter
  I want a minimum premium floor applied to low-value policies
  So that writing small policies remains profitable

  Scenario: Property quote below threshold receives minimum premium floor
    Given a customer requests "Property" insurance with a sum insured of 50000
    When the base premium is calculated
    Then the base premium should be 150.00
