# WigwamShop
## The application was created for the purchase and sale of wigwams. At the beginning of use, registration will be available for residents and buyers, communication with them and prices for each of the wigwams.
## Used technologies: 
+ `.NET Core`
+ `Automapper`
+ `Fluent API/Validation`
+ `MediatR`
+ `Entity Framework Core (v.6, SQL Server and Postgres SQL)`
+ `Hangfire`
+ `xUnit`
+ `SignalR`
+ `Identity Server 4`
+ `React`
+ `Hooks`
+ `Docker`
+ `ELK`
+ `Ocelot API`
# Get started
+ Step 1. To run this application on your local computer, you must download and install Docker Desktop (https://www.docker.com/products/docker-desktop/)
+ After fully installing Docker and testing it, you can start working.
+ Step 2. To start working with the application, you must launch it. To do this, open the console in the root folder of the project and enter the following commands:
1. `docker-compose build`
2. `docker-compose run`
+ Step 3. If you did everything right, then you can check the connection to the database SQL Server and PostgresSQL, ideally using SSMS and PgAdmin 4. 
+ Next, you can try get some test this api by using Postman or other programs for testing APIs (ports, hosts and other information you can check in appsettings.json files). 
# Services
## Identity Server API (API for register/login)
+ You can try send request for Authentification in our application by using this request: http://localhost:5103/TestUser/signup. You must use JSON format for sending request on endpoint of controller and you must identify this fields by using JSON: userName, password, confirmPassword, firstName, lastName, email. 
+ Next step, you must login in API for getting access in out APIs. 
## Catalog Service
+ If you gave done last steps correctly you can send request like it: http://localhost:8002/wigw/orders or http://localhost:8002/wigw/wigwams pre-filling in the information for OAuth 2.0 (https://learning.postman.com/docs/sending-requests/authorization/).
If this request was sending successfully, you can explain this API for next tests and using. 
## Basket Service
+ We also work with the Basket Service.
+ If you have done latest steps, ypu can send requests like these: http://localhost:8001/api/orders or http://localhost:8001/api/orders (with price and sellerId in JSON). 
+ You still need to learn the API data to work with it further, so I suggest you use the pochlon for testing and learning the APIs.
# Finally
+ This application has a lot of different features and technologies that simply cannot fit in one readme file, so with further questions, please contact: hleb.punko01@gmail.com
