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
            var result = await _digitalController.GetDigimon(login, password, id);

            result.Should().Be(expectedName);
        }
        [Theory]
        [InlineData("dk", "sdfsddfs", 1)]
        public async Task Have_UnauthorizedAccess(string login, string password, int id)
        {
            _digimonAuthenticationAPI.Setup(x => x.GetToken(login, password)).ThrowsAsync(new UnauthorizedAccessException());

            Func<Task<string>> act = async () => await _digitalController.GetDigimon(login, password, id);

            await act.Should().ThrowAsync<UnauthorizedAccessException>();
        }
        [Theory]
        [InlineData("dk", "dk", 1, "arkaldjfasdion")]
        public async Task Have_API_Exception(string login, string password, int id, string token)
        {
            _digimonAuthenticationAPI.Setup(x => x.GetToken(login, password)).ReturnsAsync(token);
            _digimonAPI.Setup(x => x.GetNameById(token, id)).ThrowsAsync(new Exception());
            Func<Task<string>> act = async () => await _digitalController.GetDigimon(login, password, id);

            await act.Should().ThrowAsync<Exception>();
        }
        [Theory]
        [InlineData("dk", "dk", 45, "arkaldjfasdion")]
        public async Task Have_Key_Not_Found_Exception(string login, string password, int id, string token)
        {
            _digimonAuthenticationAPI.Setup(x => x.GetToken(login, password)).ReturnsAsync(token);
            _digimonAPI.Setup(x => x.GetNameById(token, id)).ThrowsAsync(new KeyNotFoundException());
            Func<Task<string>> act = async () => await _digitalController.GetDigimon(login, password, id);

            await act.Should().ThrowAsync<KeyNotFoundException>();
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
            catch(KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Mission Key : " + ex.Message);
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
