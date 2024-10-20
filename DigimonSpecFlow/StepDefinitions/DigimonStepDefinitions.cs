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

        [Then(@"the result should equal ""(.*)""")]
        public void ThenTheResultShouldEqual(string expectedResult)
        {
            _result.Should().Be(expectedResult);
        }
    }
}
