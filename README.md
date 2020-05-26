# Web Api Task

This is a simple REST API for creating events. 
For this to be possible, users are to register and login to obtain an access token which is generated from the local server for authorization.
This access token which expires in 7 days and refreshed afterwards is validated by the server for access grant.
An api endpoint is further exposed for creating events and a pre-configured logger logs every request, response and error associated to a user to a textfile and database.
Note that for the logging feature into a database, an SQL query been executed on an SQL Server was used to create the table called Log by simply dropping and recreating the existing Log table which was created by Code-first approach.
 

 
  

