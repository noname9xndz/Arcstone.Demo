using System;
using System.Collections.Generic;

namespace Arcstone.Demo.Application.Fake
{
    public class UserModel
    {
        public UserModel()
        {
            Role = new List<string>();
        }

        public Guid Id { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public List<string> Role { set; get; }

    }
}