using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IAdminService
    {
        AdminUsersViewModel GetUsers(int pageId = 1 , string username = "", string email = "");
    }
}
