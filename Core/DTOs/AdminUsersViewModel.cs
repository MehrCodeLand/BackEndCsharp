using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class AdminUsersViewModel
    {
        public List<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int CountPage { get; set; }
    }
}
