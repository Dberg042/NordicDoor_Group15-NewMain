# NordicDoor_Group15

30.09.2022 from branch 'Teams'

Added TeamsController and views Index and Members for Teams.

Added Members action (currently static) in controller
Members action and view will need updating when connecting database.
Model needs to be added.

Additionally added a Database folder with a Creation.sql file where we can keep track of the SQL code used when creating our database.
Folder is meant as a place to store our SQL, be it for creation/editing/deleting or the shitload of Queries we are doomed to make.

All changes/additions are obv up for debate.

07.10.2022 from branch 'dockerConnection'

Added build.cmd file that corresponds to this project

Renamed webapp container to IMS, feel free to change
dockerfile moved from wwwroot folder to main project folder in order to work as intended


For Docker!!!!

docker build -t webapp .

docker run --env CONNECTION="Server=host.docker.internal;user=root;Database=NordicDoor;port=3306;Password=Testingtesting1234;" --publish 80:80 webapp

How to start and run the application in docker. Full guide:

Terminal:

./dbbuild.cmd

Package manager console:

update-database -Context ApplicationIdentityDbContext

Terminal:

./build.cmd
