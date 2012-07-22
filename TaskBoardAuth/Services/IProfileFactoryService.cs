using TaskBoardAuth.Models;

namespace TaskBoardAuth.Services
{
    public interface IProfileFactoryService
    {
        UserProfile GetUserProfile(string username);
        UserProfile GetUserProfile();
        string GetPropertyValue(string userName, string propertyName);
        void SetPropertyValue(string userName, string propertyName, string propertyValue);
        void Save(string userName);
    }
}