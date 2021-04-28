using Facebook.Shared.Models;

namespace Facebook.Shared.Interfaces
{
    public interface IAccount
    {
        AccountData AccountData { get; set; }
        bool EnableProxies { get; set; }
        bool IsAuthorized { get; set; }

        bool Authorization();
        bool Like(string id, string id_comment = null);
        bool Comment(string id, string message);
        bool CommentStream(string id, string message);
    }
}
