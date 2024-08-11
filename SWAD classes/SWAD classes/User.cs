using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_classes
{
    abstract class User
    {
        public User(int userId, string name, string email, string contactNo)
        {
            UserId = userId;
            Name = name;
            Email = email;
            ContactNo = contactNo;
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        
    }
}
