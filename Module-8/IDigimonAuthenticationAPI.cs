namespace LearnTDD.Module_8
{
    public interface IDigimonAuthenticationAPI
    {
        Task<string> GetToken(string login, string password);
    }
}
