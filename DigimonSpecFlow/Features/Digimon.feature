Feature: Feature1

The DigimonController manages Digimon data retrieval by authenticating users and fetching information based on provided credentials, while handling errors gracefully.

Scenario: Return Name When Valid Credentials
    Given the following valid credentials:
        | login | password | ID | expected name |
        | dk    | dk       | 1  | Daanyaal      |
    When the GetDigimon method is invoked with the login, password, and ID
    Then the result should equal "Daanyaal"
