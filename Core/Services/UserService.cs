using Core.DTOs;
using Core.Generator;
using Core.Security;
using Core.Services.Interfaces;
using Data.Context;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _db;
        public UserService(MyDbContext db)
        {
            _db = db;
        }

        public bool ActiveUser(string id)
        {
            User user = _db.Users.FirstOrDefault(x => x.ActiveCode == id);
            user.IsActive = true;
            user.ActiveCode = ActiveCodeGen.GenerateCode();

            Update(user);

            return true;
        }

        public void Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public User FindUserByEmailOrUsername( SignInViewModel signIn)
        {
            signIn.Password = PasswordHashC.EncodePasswordMd5(signIn.Password);
            User user = _db.Users.SingleOrDefault(u => (u.Email == signIn.UsernameOrEmail) || (u.Username == signIn.UsernameOrEmail) && (u.Password == signIn.Password));
            return user;
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _db.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public User GetUserByEmail(string email)
        {
            return _db.Users.SingleOrDefault(u => u.Email == email);
        }

        public bool IsEmail(string email)
        {
            return _db.Users.Any(u => u.Email == email);
        }

        public bool IsUsername(string username)
        {
            return _db.Users.Any(u => u.Username == username);
        }

        public void Update(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
        }
    }
}
