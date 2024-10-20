Feature: Feature1

The DigimonController manages Digimon data retrieval by authenticating users and fetching information based on provided credentials, while handling errors gracefully.

Scenario: Return Name When Valid Credentials
    Given the following valid credentials:
        | login | password | ID | expected name |
        | dk    | dk       | 1  | Daanyaal      |
    When the GetDigimon method is invoked with the login, password, and ID
    Then the result should equal "Daanyaal"


Scenario: Throw UnauthorizedAccessException When Invalid Credentials
    Given the following credentials with invalid-password:
        | login | password          | ID |
        | dk    | invalid-password   | 1  |
    When the GetDigimon method is invoked for exception with the login, password, and ID
    Then an UnauthorizedAccessException should be thrown

Scenario: Throw ApiException When Api Fails
    Given the following credentials for API failure:
        | login | password | ID |
        | dk    | dk       | 1  |
    When the GetDigimon method is invoked for exception with the login, password, and ID
    Then an Exception should be thrown with message "API Failure : API error"

Scenario: Throw KeyNotFoundException When Digimon Not Found
    Given the following credentials with invalid id:
        | login | password | ID |
        | dk    | dk       | 45 |
    When the GetDigimon method is invoked for exception with the login, password, and ID
    Then a KeyNotFoundException should be thrown with message "Mission Key : Digimon not found"
