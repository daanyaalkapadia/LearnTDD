namespace LearnTDD.Module_8
{
    public interface IDigimonAuthenticationAPI
    {
        string GetToken(string login, string password);
    }
}
