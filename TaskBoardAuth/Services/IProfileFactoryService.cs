using TaskBoardAuth.Models;

namespace TaskBoardAuth.Services
{
    public interface IProfileFactoryService
    {
        UserProfile GetUserProfile(string username);
        UserProfile GetUserProfile();
        string GetPropertyValue(string userName, string propertyName);
    }
}