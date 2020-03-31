Juicer App
=====

`Work in progress`


This will be the app implementing CRUD operations on juice recipes.  
Web application simple copy of [Philips Healthy Recipes App](https://www.philips.co.uk/c-m-ho/food-preparation/healthy-drinks-recipe-app).  
Deployed on Azure.

### Technologies
* ASP.NET Core 3.1
* Entity Framework Core 3.1.2
* Bootstrap 4.3.1
* jQuery.AJAX

#### Done so far
1. Created simple inital model of Product for testing purposes
2. Created interface for Data Repository
3. Connected to local SQL DB with Entity Framework Core
4. [Deployed blank version to Azure App Service](https://juicer.azurewebsites.net/)
5. Connected with Azure SQL Database
6. [Created REST API for Recipes](https://github.com/Marcin-Stanczyk/Juicer/blob/master/Juicer.API/RecipesController.cs)
7. Posting **new** recipes with it's ingredients using jQuery.AJAX post method

#### Next steps
8. Decouple Repository for RecipesRepo and ProductsRepo
9. Create API for Products
10. Adding images to Azure Blob Storage
11. Editing Recipes on client side with jQuery.AJAX
12. Improve look and feel