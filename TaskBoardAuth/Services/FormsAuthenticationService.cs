using System;
using System.Web.Security;

namespace TaskBoardAuth.Services
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        #region IFormsAuthenticationService Members

        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        #endregion
    }
}