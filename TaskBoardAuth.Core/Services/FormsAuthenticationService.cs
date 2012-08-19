using System.Web.Security;
using TaskBoardAuth.Core.Interfaces;

namespace TaskBoardAuth.Core.Services
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
    }
}