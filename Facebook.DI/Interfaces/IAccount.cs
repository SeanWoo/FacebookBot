namespace Facebook.Core.Interfaces
{
    public interface IAccount
    {
        bool Authorization();
        bool Like(string id, string id_comment = null);
        bool Comment(string id, string message);
    }
}
