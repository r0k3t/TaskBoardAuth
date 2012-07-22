using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskBoardAuth.Services
{
    public interface IFormsAuthenticationService
    {
        void SetAuthCookie(string userName, bool createPersistentCookie);
    }
}
