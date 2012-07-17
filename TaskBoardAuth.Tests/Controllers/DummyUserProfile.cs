using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using TaskBoardAuth.Models;

namespace TaskBoardAuth.Tests.Controllers
{
    public class DummyUserProfile: UserProfile
    {
        public new String FirstName { get; set; }
        public new String LastName { get; set; }
    }
}
