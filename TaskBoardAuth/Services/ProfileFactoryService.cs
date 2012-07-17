using TaskBoardAuth.Models;

namespace TaskBoardAuth.Services
{
    class ProfileFactoryService : IProfileFactoryService
    {
        public UserProfile GetUserProfile(string username)
        {
            return UserProfile.GetUserProfile(username);
        }

        public UserProfile GetUserProfile()
        {
            return UserProfile.GetUserProfile();
        }

        public string GetPropertyValue(string userName, string propertyName)
        {
            return (string)GetUserProfile(userName).GetPropertyValue(propertyName);
        }
    }
}