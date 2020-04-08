Juicer App
=====

Web application for juice recipes based on [Philips Healthy Recipes](https://www.philips.co.uk/c-m-ho/food-preparation/healthy-drinks-recipe-app).  
[Deployed on Azure.](https://juicer.azurewebsites.net/)

### Technologies
* ASP.NET Core 3.1 with Razor Pages
* Entity Framework Core 3.1.2
* Bootstrap 4.3.1
* jQuery.AJAX [=> see code file](https://github.com/Marcin-Stanczyk/Juicer/blob/master/wwwroot/js/editRecipe.js)
* Azure App Service
* Azure SQL Database
* Azure Blob Storage
* REST API for Recipes [=> /api/recipes](https://juicer.azurewebsites.net/api/recipes) or [see code file](https://github.com/Marcin-Stanczyk/Juicer/blob/master/Juicer.API/RecipesController.cs)

### Future plans
* Add user authentication and authorization
* Extend Recipe class with properties like:
  * Date added / edited
  * Energy (kcal)
  * Time needed for preparation (min)
  * Difficulty level (easy, medium, hard)
  * Portions with option facility (portions calculator on client-side multiplying ingredients amounts)
  * Category (Breakfast, Snack, Dinner, etc.)
  * User Rating
* Many more ideas to re-make this app into a Cook Book