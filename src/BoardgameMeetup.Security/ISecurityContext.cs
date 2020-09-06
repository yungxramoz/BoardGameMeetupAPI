using BoardgameMeetup.Data.Models;

namespace BoardgameMeetup.Security
{
    public interface ISecurityContext
    {
        User User { get; }
        bool IsAdministrator { get; }
    }
}
