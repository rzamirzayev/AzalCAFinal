namespace Services
{
    public interface IContactPostService
    {
        Task<string> Add(string fullname,string email,string message);
    }
}
