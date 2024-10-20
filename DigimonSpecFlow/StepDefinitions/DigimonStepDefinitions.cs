using LearnTDD.Module_8;
using Moq;

namespace DigimonSpecFlow.StepDefinitions
{
    [Binding]
    public class DigimonDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private Mock<IDigimonAuthenticationAPI> _digimonAuthenticationAPI;
        private Mock<IDigimonAPI> _digimonAPI;
        private DigimonController _digimonController;
        private string _login;
        private string _password;
        private int _id;
        private string _token;
        private string _expectedName;
        private string _result;
        private Func<Task> _resultAct;

        public DigimonDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void Setup()
        {
            _digimonAuthenticationAPI = new Mock<IDigimonAuthenticationAPI>();
            _digimonAPI = new Mock<IDigimonAPI>();
            _digimonController = new DigimonController(_digimonAPI.Object, _digimonAuthenticationAPI.Object);
        }

        [Given(@"the following valid credentials:")]
        public void GivenTheFollowingValidCredentials(Table table)
        {
            var row = table.Rows[0]; // Get the first row of the table

            _login = row["login"];
            _password = row["password"];
            _id = int.Parse(row["ID"]);
            _expectedName = row["expected name"];

            // Setup mocks based on the provided values
            _digimonAuthenticationAPI.Setup(x => x.GetToken(_login, _password)).ReturnsAsync("valid-token");
            _digimonAPI.Setup(x => x.GetNameById("valid-token", _id)).ReturnsAsync(_expectedName);
        }

        [When(@"the GetDigimon method is invoked with the login, password, and ID")]
        public async Task WhenTheGetDigimonMethodIsInvoked()
        {
            _result = await _digimonController.GetDigimon(_login, _password, _id);
        }
        [When(@"the GetDigimon method is invoked for exception with the login, password, and ID")]
        public async Task WhenTheGetDigimonMethodIsInvokedForException()
        {
            _resultAct = async () => await _digimonController.GetDigimon(_login, _password, _id);
        }
        [Then(@"the result should equal ""(.*)""")]
        public void ThenTheResultShouldEqual(string expectedResult)
        {
            _result.Should().Be(expectedResult);
        }

        [Given(@"the following credentials with invalid-password:")]
        public void GivenTheFollowingCredentialsWithInvalid_Password(Table table)
        {
            var row = table.Rows[0]; // Get the first row of the table

            _login = row["login"];
            _password = row["password"];
            _id = int.Parse(row["ID"]);
            _digimonAuthenticationAPI.Setup(x => x.GetToken(_login, _password)).ThrowsAsync(new UnauthorizedAccessException());
        }

        [Given(@"the following credentials for API failure:")]
        public void GivenTheFollowingCredentialsForAPIFailure(Table table)
        {
            var row = table.Rows[0]; // Get the first row of the table

            _login = row["login"];
            _password = row["password"];
            _id = int.Parse(row["ID"]);
            _digimonAuthenticationAPI.Setup(x => x.GetToken(_login, _password)).ReturnsAsync(_token);
            _digimonAPI.Setup(x => x.GetNameById(_token, _id)).ThrowsAsync(new Exception("API error"));
        }

        [Given(@"the following credentials with invalid id:")]
        public void GivenTheFollowingCredentialsWithInvalidId(Table table)
        {
            var row = table.Rows[0]; // Get the first row of the table

            _login = row["login"];
            _password = row["password"];
            _id = int.Parse(row["ID"]);

            _digimonAuthenticationAPI.Setup(x => x.GetToken(_login, _password)).ReturnsAsync(_token);
            _digimonAPI.Setup(x => x.GetNameById(_token, _id)).ThrowsAsync(new KeyNotFoundException("Digimon not found"));
        }
        [Then(@"an UnauthorizedAccessException should be thrown")]
        public async Task ThenAnUnauthorizedAccessExceptionShouldBeThrown()
        {
            await _resultAct.Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Then(@"an Exception should be thrown with message ""(.*)""")]
        public async Task ThenAnExceptionShouldBeThrownWithMessage(string message)
        {
            await _resultAct.Should().ThrowAsync<Exception>().WithMessage(message);
        }

        [Then(@"a KeyNotFoundException should be thrown with message ""(.*)""")]
        public async Task ThenAKeyNotFoundExceptionShouldBeThrownWithMessage(string message)
        {
            await _resultAct.Should().ThrowAsync<KeyNotFoundException>().WithMessage(message);
        }
    }
}
