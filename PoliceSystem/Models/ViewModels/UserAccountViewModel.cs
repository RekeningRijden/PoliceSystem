using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.ViewModels
{
    public class UserAccountViewModel
    {
        public User User { get; set; }
        public List<User> Users { get; set; }

        public UserAccountViewModel(User user, List<User> users)
        {
            this.User = user;
            this.Users = users;
        }
    }
}