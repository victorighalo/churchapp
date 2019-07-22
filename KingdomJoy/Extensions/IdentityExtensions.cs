using KingdomJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Web;
using KingdomJoy.Controllers;

namespace KingdomJoy.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserFirstName(this IIdentity identity)
        {
            var db = ApplicationDbContext.Create();
            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(identity.Name));
            return user != null ? user.FirstName : String.Empty;
   
        }

        public static async Task GetUsers(this List<UserViewModel> users)
        {
            var db = ApplicationDbContext.Create();
             users.AddRange( await (from user in db.Users
                                    from UserRole in user.Roles
                                    join role in db.Roles on UserRole.RoleId equals role.Id
                                    select new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                Role = role.Name
            }).OrderBy(u => u.Id).ToListAsync() );
      
        }

        public static IList<string> GetRoleName(string id)
        {
            var accountController = new AccountController();
            return accountController.UserManager.GetRoles(id);
        }
    }
}