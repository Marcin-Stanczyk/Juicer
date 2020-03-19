Juicer App
=====

`Work in progress`


This will be the app implementing CRUD operations on juice recipes.  
Web application simple copy of [Philips Healthy Recipes App](https://www.philips.co.uk/c-m-ho/food-preparation/healthy-drinks-recipe-app).  
Deployed on Azure.

### Technologies stack
* ASP.NET Core 3.1
* Entity Framework Core 3.1.2
* Bootstrap 4.3.1

#### Done so far
1. Created simple inital model of Product for testing purposes
2. Created interface for Data Repository
3. Connected to local SQL DB with Entity Framework Core
4. [Deployed to Azure App Service](https://juicer.azurewebsites.net/)
5. Connected with Azure SQL Database

#### Next steps
6. Improve Products Page and Model
7. Add Recipe Model with ingredients (each igredient will be Product with it's amount and unit)
8. Adding images to Azure Blob Storage