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

        public void SetPropertyValue(string userName, string propertyName, string propertyValue)
        {
            GetUserProfile(userName).SetPropertyValue(propertyName, propertyValue);
        }

        public void Save(string userName)
        {
            GetUserProfile(userName).Save();
        }
    }
}