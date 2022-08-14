# Introduction 
Apartments Parser is the service for more convenient and easy way of searching for Polish apartments available on the website. 

#Getting Started
TODO:
1.  You need to specify a connection string for SQL Server Database. You can do it in "appsettings.json" file in ApartmentsParser.UI folder
2.  You can change the number of pages and the list of cities that will be parsed and then showed. You can do it in "appsettings.json" file in
ApartmentsParser.JobsRunner folder
3.	Now you can run the application

# How does it work
After launch you will see the table that will be empty at first. When you run the application, it starts to parse new apartments and add them to 
the database. It means that all that you need to do to see the list of available apartments is to refresh the page. Also program automatically 
refreshes the page every 2 minutes. In the table you will see the name of advertisement, the city and the link to the advertisement.


# Build and Test
Also you can run the tests that briefly checks operability of the program. To do it you need to specify the connection string in 
"UnitTestsConstants" in ApartmentsParser.SharedData.TestsData folder.

