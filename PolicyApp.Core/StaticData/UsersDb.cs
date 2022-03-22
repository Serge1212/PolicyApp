using PolicyApp.Core.Models;
using System.Collections.Generic;

namespace PolicyApp.Core.StaticData
{
    public class UsersDb
    {
        public static List<User> Users => new()
        {
            new User
            {
                Id = 1,
                Name = "John",
                Surname = "Williams",
                Role = "Admin",
                Email = "john@gmail.com",
                Password = "123",
                IsMetalFan = true
            },
            new User
            {
                Id = 2,
                Name = "Chop",
                Surname = "Suey",
                Role = "Pielegniarka",
                Email = "chop@gmail.com",
                Password = "1234",
                IsMetalFan = true
            },
            new User
            {
                Id = 3,
                Name = "Olaf",
                Surname = "Peters",
                Role = "Valuable Asset",
                Email = "olaf@gmail.com",
                Password = "12345",
                IsMetalFan = false
            },
            new User
            {
                Id = 4,
                Name = "Hassan",
                Surname = "Chop",
                Role = "Admin",
                Email = "hassan@gmail.com",
                Password = "123456",
                IsMetalFan = true
            },
            new User
            {
                Id = 5,
                Name = "Scar",
                Surname = "Tissue",
                Role = "Admin",
                Email = "scar@gmail.com",
                Password = "1234567",
                IsMetalFan = false
            },
            new User
            {
                Id = 6,
                Name = "Drowning",
                Surname = "Pool",
                Role = "Pielegniarka",
                Email = "drowning@gmail.com",
                Password = "12345678",
                IsMetalFan = true
            }
        };
    }
}
