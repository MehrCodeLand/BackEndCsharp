using Core.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsEmail(string email);
        bool IsUsername(string username);
        void Add(User user);
        bool ActiveUser(string id);
        void Update(User user);
        User FindUserByEmailOrUsername(SignInViewModel signInView);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
    }
}
