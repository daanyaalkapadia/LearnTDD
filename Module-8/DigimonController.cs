using FluentAssertions;
using Moq;

namespace LearnTDD.Module_8
{
    public class DigimonControllerShould
    {
        private readonly DigimonController _digimonController;
        private readonly Mock<IDigimonAPI> _digimonAPI;
        private readonly Mock<IDigimonAuthenticationAPI> _digimonAuthenticationAPI;

        public DigimonControllerShould()
        {
            _digimonAPI = new Mock<IDigimonAPI>();
            _digimonAuthenticationAPI = new Mock<IDigimonAuthenticationAPI>();
            _digimonController = new DigimonController(_digimonAPI.Object, _digimonAuthenticationAPI.Object);
        }

        [Theory]
        [InlineData("dk", "dk", 1, "valid-token", "Daanyaal")]
        public async Task Return_Name_When_Valid_Credentials(string login, string password, int id, string token, string expectedName)
        {
            _digimonAuthenticationAPI.Setup(x => x.GetToken(login, password)).ReturnsAsync(token);
            _digimonAPI.Setup(x => x.GetNameById(token, id)).ReturnsAsync(expectedName);

            var result = await _digimonController.GetDigimon(login, password, id);

            result.Should().Be(expectedName);
        }

        [Theory]
        [InlineData("dk", "invalid-password", 1)]
        public async Task Throw_UnauthorizedAccessException_When_Invalid_Credentials(string login, string password, int id)
        {
            _digimonAuthenticationAPI.Setup(x => x.GetToken(login, password)).ThrowsAsync(new UnauthorizedAccessException());

            Func<Task> act = async () => await _digimonController.GetDigimon(login, password, id);

            await act.Should().ThrowAsync<UnauthorizedAccessException>();
        }

        [Theory]
        [InlineData("dk", "dk", 1, "valid-token")]
        public async Task Throw_ApiException_When_Api_Fails(string login, string password, int id, string token)
        {
            _digimonAuthenticationAPI.Setup(x => x.GetToken(login, password)).ReturnsAsync(token);
            _digimonAPI.Setup(x => x.GetNameById(token, id)).ThrowsAsync(new Exception("API error"));

            Func<Task> act = async () => await _digimonController.GetDigimon(login, password, id);

            await act.Should().ThrowAsync<Exception>().WithMessage("API Failure : API error");
        }

        [Theory]
        [InlineData("dk", "dk", 45, "valid-token")]
        public async Task Throw_KeyNotFoundException_When_Digimon_Not_Found(string login, string password, int id, string token)
        {
            _digimonAuthenticationAPI.Setup(x => x.GetToken(login, password)).ReturnsAsync(token);
            _digimonAPI.Setup(x => x.GetNameById(token, id)).ThrowsAsync(new KeyNotFoundException("Digimon not found"));

            Func<Task> act = async () => await _digimonController.GetDigimon(login, password, id);

            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Mission Key : Digimon not found");
        }
    }

    public class DigimonController
    {
        private readonly IDigimonAPI _digimonAPI;
        private readonly IDigimonAuthenticationAPI _digimonAuthenticationAPI;

        public DigimonController(IDigimonAPI digimonAPI, IDigimonAuthenticationAPI digimonAuthenticationAPI)
        {
            _digimonAPI = digimonAPI;
            _digimonAuthenticationAPI = digimonAuthenticationAPI;
        }

        public async Task<string> GetDigimon(string login, string password, int id)
        {
            try
            {
                var token = await _digimonAuthenticationAPI.GetToken(login, password);
                return await _digimonAPI.GetNameById(token, id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Mission Key : " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException("Unauthorized : " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("API Failure : " + ex.Message);
            }
        }
    }
}
