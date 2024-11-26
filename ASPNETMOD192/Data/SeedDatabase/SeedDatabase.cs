using Microsoft.AspNetCore.Identity;

namespace ASPNETMOD192.Data.SeedDatabase
{
    public class SeedDatabase
    {

        public static void Seed(ApplicationDbContext context,
                                UserManager<IdentityUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager).Wait();
            SeedUsers(userManager).Wait();
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // CREATE ADMIN
            var roleCheck = await roleManager.RoleExistsAsync(ASPNETMOD192Constants.ROLES.ADMIN);

            if (!roleCheck)
            {
                var adminRole = new IdentityRole
                {
                    Name = ASPNETMOD192Constants.ROLES.ADMIN

                };

                await roleManager.CreateAsync(adminRole);
            }



            // CREATE DRIVER
            roleCheck = await roleManager.RoleExistsAsync(ASPNETMOD192Constants.ROLES.DRIVER);

            if (!roleCheck)
            {
                var driverRole = new IdentityRole
                {
                    Name = ASPNETMOD192Constants.ROLES.DRIVER
                };

                await roleManager.CreateAsync(driverRole);
            }


            // CREATE ADMINISTRATIVE
            roleCheck = await roleManager.RoleExistsAsync(ASPNETMOD192Constants.ROLES.ADMINISTRATIVE);

            if (!roleCheck)
            {
                var administrativeRole = new IdentityRole
                {
                    Name = ASPNETMOD192Constants.ROLES.ADMINISTRATIVE
                };

                await roleManager.CreateAsync(administrativeRole);
            }
        }


        private static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            var dbAdmin = await userManager.FindByNameAsync(ASPNETMOD192Constants.USERS.ADMIN.USERNAME);

            if (dbAdmin == null)
            {
                IdentityUser userAdmin = new IdentityUser
                {
                    UserName = ASPNETMOD192Constants.USERS.ADMIN.USERNAME,
                    Email = ASPNETMOD192Constants.USERS.ADMIN.EMAIL
                };

                var result = await userManager.CreateAsync(userAdmin, ASPNETMOD192Constants.USERS.ADMIN.PASSWORD);

                if (result.Succeeded)
                {
                    dbAdmin = await userManager.FindByNameAsync(ASPNETMOD192Constants.USERS.ADMIN.USERNAME);
                    await userManager.AddToRoleAsync(dbAdmin!, ASPNETMOD192Constants.ROLES.ADMIN);
                }
            }

            var dbDriver = await userManager.FindByNameAsync(ASPNETMOD192Constants.USERS.DRIVER.USERNAME);

            if (dbDriver == null)
            {
                IdentityUser userDriver = new IdentityUser
                {
                    UserName = ASPNETMOD192Constants.USERS.DRIVER.USERNAME,
                    Email = ASPNETMOD192Constants.USERS.DRIVER.EMAIL
                };

                var result = await userManager.CreateAsync(userDriver, ASPNETMOD192Constants.USERS.DRIVER.PASSWORD);

                if (result.Succeeded)
                {
                    dbDriver = await userManager.FindByNameAsync(ASPNETMOD192Constants.USERS.DRIVER.USERNAME);
                    await userManager.AddToRoleAsync(dbDriver!, ASPNETMOD192Constants.ROLES.DRIVER);
                }
            }


            var dbAdministrative = await userManager.FindByNameAsync(ASPNETMOD192Constants.USERS.ADMINISTRATIVE.USERNAME);

            if (dbAdministrative == null)
            {
                IdentityUser userAdministrative = new IdentityUser
                {
                    UserName = ASPNETMOD192Constants.USERS.ADMINISTRATIVE.USERNAME,
                    Email = ASPNETMOD192Constants.USERS.ADMINISTRATIVE.EMAIL
                };

                var result = await userManager.CreateAsync(userAdministrative, ASPNETMOD192Constants.USERS.ADMINISTRATIVE.PASSWORD);

                if (result.Succeeded)
                {
                    dbAdministrative = await userManager.FindByNameAsync(ASPNETMOD192Constants.USERS.ADMINISTRATIVE.USERNAME);
                    await userManager.AddToRoleAsync(dbAdministrative!, ASPNETMOD192Constants.ROLES.ADMINISTRATIVE);
                }
            }
        }
    }
}
