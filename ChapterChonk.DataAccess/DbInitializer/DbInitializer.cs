using ChapterChonk.DataAccess.Data;
using ChapterChonk.Models;
using ChapterChonk.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterChonk.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDBContext _dBContext;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            // migrations
            try
            {
                if (_dBContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dBContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            // roles
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company));
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee));

                // admin
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "testAdmin@gmail.com",
                    Name = "Admin Admin",
                    PhoneNumber = "1234567890",
                    StreetAddres = "Storgata 45",
                    State = "Finmark",
                    PostalCode = "1234",
                    City = "Tromso"

                }, "Temp1212!").GetAwaiter().GetResult();

                ApplicationUser user = _dBContext.ApplicationUsers.FirstOrDefault(u => u.Email == "testAdmin@gmail.com");
                _userManager.AddToRoleAsync(user, SD.Role_Customer).GetAwaiter().GetResult();
            }

            return;
        }
    }
}
