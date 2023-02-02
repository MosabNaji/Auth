using Microsoft.AspNetCore.Identity;
using System;

namespace Auth.Web.Models
{
    public class User : IdentityUser
    {
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
