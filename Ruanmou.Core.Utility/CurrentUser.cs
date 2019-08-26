using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruanmou.NetCore2.MVC6.Models
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime LoginTime { get; set; }
    }

    public class UserService
    {
        public CurrentUser FindUser(string userName, string password)
        {
            return new CurrentUser()
            {
                Id = 123,
                Name = "Eleven",
                Account = "Administrator",
                Password = "123456",
                Email = "57265177@qq.com",
                LoginTime = DateTime.Now,
                Role = userName.Equals("Eleven") ? "Admin" : "User"
            };
        }
    }
}
