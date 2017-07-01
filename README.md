# VSC MongoDB Tutorial
My Visual Studio Code MongoDB tutorial and test project. 

View at your own risk!

Note: this is a testbed to test MongoDB stuff in isolation. 

This project was created to test two things:
1. Invoking a MapReduce function from c# (Crimes example)
2. Using the normalized data model with document references (uses Members and Items).

Notes:
Getting the mongodb driver
https://docs.mongodb.com/ecosystem/drivers/csharp/
To install use either:
dotnet add package mongodb.driver (official version I believe
), or mongocsharpdriver

useful shell commands:
help
db.help
show dbs
use <db>; // note will create if does not exist.
db
show collections
db.createCollection("name")
db.<collection>.find()

