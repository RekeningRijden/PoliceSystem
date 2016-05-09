using PoliceSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserGroup UserGroup { get; set; }

        public User(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
    }
}