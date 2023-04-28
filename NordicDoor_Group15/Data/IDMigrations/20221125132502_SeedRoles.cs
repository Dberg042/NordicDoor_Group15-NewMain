using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NordicDoor_Group15.Data.IDMigrations
{
    public partial class SeedRoles : Migration
    {

        private string UserRoleId = Guid.NewGuid().ToString();
        private string TeamManagerRoleId = Guid.NewGuid().ToString();
        private string AdminRoleId = Guid.NewGuid().ToString();

        private string User1Id = Guid.NewGuid().ToString();
        private string User2Id = Guid.NewGuid().ToString();

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSQL(migrationBuilder);

            SeedUser(migrationBuilder);

            SeedUserRoles(migrationBuilder);
        }

        private void SeedRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO `NordicDoor`.`AspNetRoles` (`Id`,`Name`,`NormalizedName`,`ConcurrencyStamp`)
            VALUES ('{AdminRoleId}', 'Administrator', 'ADMINISTRATOR', null);");
            migrationBuilder.Sql(@$"INSERT INTO `NordicDoor`.`AspNetRoles` (`Id`,`Name`,`NormalizedName`,`ConcurrencyStamp`)
            VALUES ('{TeamManagerRoleId}', 'TeamManager', 'TEAMMANAGER', null);");
            migrationBuilder.Sql(@$"INSERT INTO `NordicDoor`.`AspNetRoles` (`Id`,`Name`,`NormalizedName`,`ConcurrencyStamp`)
            VALUES ('{UserRoleId}', 'User', 'USER', null);");
        }

        private void SeedUser(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @$"INSERT `NordicDoor`.`AspNetUsers` (`Id`,`EmployeeNumber`, `FirstName`, `LastName`, `UserName`, `NormalizedUserName`, 
                `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, 
                `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) 
                VALUES 
                (N'{User1Id}', 10000, N'Team', N'Admin', N'10000', N'10000', 
                N'test2@test.ca', N'TEST2@TEST.CA', 0, 
                N'AQAAAAEAACcQAAAAEIWm4CdWvu1Vvr2PeMIk1OUey+CioPEeZbFLsn3AnrSbwU3578xssB225B7ZX0BDlg==', 
                N'YUPAFWNGZI2UC5FOITC7PX5J7XZTAZAA', N'8e150555-a20d-4610-93ff-49c5af44f749', NULL, 0, 0, NULL, 1, 0)");

            migrationBuilder.Sql(
                @$"INSERT `NordicDoor`.`AspNetUsers` (`Id`,`EmployeeNumber`, `FirstName`, `LastName`, `UserName`, `NormalizedUserName`, 
                `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, 
                `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) 
                VALUES 
                (N'{User2Id}',10001, N'NordicDoor', N'Admin', N'10001', N'10001', 
                N'test3@test.ca', N'TEST3@TEST.CA', 0, 
                N'AQAAAAEAACcQAAAAEOD2bmRcr+6ZV6HUuIyA8VRCHpsrwoMQjRzw2SCExJYms4HZ4VIWlMD4TCMZXd9FhQ==', 
                N'BQ7Q3CF3LXMF6BHZ4WCAIR6SUSIJ2BGU', N'aec40b7c-da19-4bf0-b08e-2ae28c33be7a', NULL, 0, 0, NULL, 1, 0)");
        }

        private void SeedUserRoles(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"
        INSERT INTO `NordicDoor`.`AspNetUserRoles`
           (`UserId`
           ,`RoleId`)
        VALUES
           ('{User1Id}', '{TeamManagerRoleId}');
        ");

            migrationBuilder.Sql(@$"
        INSERT INTO `NordicDoor`.`AspNetUserRoles`
           (`UserId`
           ,`RoleId`)
        VALUES
           ('{User2Id}', '{AdminRoleId}');
        INSERT INTO `NordicDoor`.`AspNetUserRoles`
           (`UserId`
           ,`RoleId`)
        VALUES
           ('{User2Id}', '{TeamManagerRoleId}');
        INSERT INTO `NordicDoor`.`AspNetUserRoles`
           (`UserId`
           ,`RoleId`)
        VALUES
           ('{User2Id}', '{UserRoleId}');");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}