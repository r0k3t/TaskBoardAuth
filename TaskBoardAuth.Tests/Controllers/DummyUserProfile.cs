using System;
using TaskBoardAuth.Core.Models;

namespace TaskBoardAuth.Tests.Controllers
{
    public class DummyUserProfile : UserProfile
    {
        public new String FirstName { get; set; }
        public new String LastName { get; set; }
    }
}