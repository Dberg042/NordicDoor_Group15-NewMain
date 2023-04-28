Guide to running the application for the first time(windows):

1)build the database in docker. Terminal:
./dbbuild.cmd

2)Open package manager console and update database:
Update-Database -Context ApplicationIdentityDbContext

3)Build docker webapp. Terminal:
./build.cmd

Guide to running the application for the first time(mac):

1)build the database in docker. Terminal:
./dbbuild.sh

2)Open package manager console and update database:
Update-Database -Context ApplicationIdentityDbContext

3)Build docker webapp. Terminal:
./build.sh
_________________________________________________________

if you somehow need to delete the local database:
./dbbuildnew.sh or ./dbbuildnew.cmd will delete the database and build a clean docker database.

_________________________________________________________

If it is needed delete all database and do migration manually on package manager console 
First:

Add-Migration -Context ApplicationIdentityDbContext -OutputDir Data\IDMigrations Initial
Update-Database -Context ApplicationIdentityDbContext
Second:
Add-Migration -Context ApplicationIdentityDbContext -OutputDir Data\IDMigrations SeedRoles

Open Data folder and copy all content in _SeedRoles.txt file and 
paste it to Data/IDMigration SeedRoles.cs file

then

Update-Database -Context ApplicationIdentityDbContext

System ready to run:

You can login with admin user below;
UserName:10001
Password:Abcd.1234








