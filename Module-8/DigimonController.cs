using FluentAssertions;
using Moq;

namespace LearnTDD.Module_8
{
    public class DigimonControllerShould
    {
        private readonly DigimonController _digitalController;
        private readonly Mock<IDigimonAPI> _digimonAPI;
        private readonly Mock<IDigimonAuthenticationAPI> _digimonAuthenticationAPI;
        public DigimonControllerShould()
        {
            _digimonAPI = new Mock<IDigimonAPI>();
            _digimonAuthenticationAPI = new Mock<IDigimonAuthenticationAPI>();
            _digitalController = new DigimonController(_digimonAPI.Object, _digimonAuthenticationAPI.Object);
        }

        [Theory]
        [InlineData("dk", "dk", 1, "arkaldjfasdion", "Daanyaal")]
        public async Task Return_Name(string login, string password, int id, string token, string expectedName)
        {
            _digimonAuthenticationAPI.Setup(x => x.GetToken(login, password)).ReturnsAsync(token);
            _digimonAPI.Setup(x => x.GetNameById(token, id)).ReturnsAsync(expectedName);
            var result = await _digitalController.GetNameById(login, password, id);

            result.Should().Be(expectedName);
        }
        [Theory]
        [InlineData("dk", "sdfsddfs", 1, "", "Daanyaal")]
        public async Task Have_UnauthorizedAccess(string login, string password, int id, string token, string expectedName)
        {
            _digimonAuthenticationAPI.Setup(x => x.GetToken(login, password)).ThrowsAsync(new UnauthorizedAccessException());

            Func<Task<string>> act = async () => await _digitalController.GetNameById(login, password, id);

            await act.Should().ThrowAsync<UnauthorizedAccessException>();
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

        public async Task<string> GetNameById(string login, string password, int id)
        {
            try
            {
                var token = await _digimonAuthenticationAPI.GetToken(login, password);
                return await _digimonAPI.GetNameById(token, id);
            }
            catch(UnauthorizedAccessException ex)
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
