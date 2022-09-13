using Core.DTOs;
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
    public class AdminService : IAdminService
    {
        private readonly MyDbContext _db;
        public AdminService(MyDbContext db)
        {
            _db = db;
        }

        public AdminUsersViewModel GetUsers(int pageId = 1, string username = "" , string email = "")
        {
            IQueryable<User> users = _db.Users;


            if (!string.IsNullOrEmpty(username))
            {
                users = users.Where( u => u.Username.Contains(username));
            }

            if (!string.IsNullOrEmpty(email))
            {
                users = users.Where( u => u.Email.Contains(email));
            }

            // Show Items 
            int take = 1;
            int skip = (pageId - 1 )*take;

            AdminUsersViewModel lists = new AdminUsersViewModel();
            lists.CurrentPage = pageId;
            lists.CountPage = (users.Count());
            lists.Users = users.OrderBy(u => u.Created).Skip(skip).Take(take).ToList();

            return lists;
        }
    }
}
