using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PoliceSystem.Models.Domain
{
    public class UserGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public UserGroup() { }

        public UserGroup(string Name)
        {
            this.Name = Name;
        }
    }
}