using Facebook.Shared.Models;

namespace Facebook.Shared.Interfaces
{
    public interface IAccount
    {
        AccountData AccountData { get; set; }
        bool Authorization();
        bool Like(string id, string id_comment = null);
        bool Comment(string id, string message);
    }
}
