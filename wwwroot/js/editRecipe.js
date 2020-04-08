$(function () {

    // Creating new Recipe Data Transfer Object
    // Filling it with user input
    // Showing it's preview on screen at the same time
    // Posting to DB with jQuery.AJAX

    // -----------------------------------------------

    var recipeDto;

    if ($("#RecipeDto_Id").val() > 0) {
        $.ajax({
            url: "/api/recipes/" + $("#RecipeDto_Id").val(),
            success: function (response) {
                recipeDto = response;

                $("#nameView").text(recipeDto.name);
                $("#descriptionView").text(recipeDto.description);
                $(recipeDto.instructions).each(function (index, element) {
                    $("#instructionsView").append("<li>"
                        + element
                        + "<button class='remove btn btn-outline-danger btn-sm'>X</button>"
                        + "</li>");

                    $(".remove").on("click", function () {
                        $(this).parent().remove();
                    });
                });
                $(recipeDto.ingredients).each(function (index, element) {
                    $("#ingredientsView").append(
                        "<tr>"
                        + "<td class='product'>"
                        + element.productName
                        + "</td><td class='amount text-right'>"
                        + element.amount
                        + "</td><td class='unit text-right'>"
                        + element.unit
                        + "</td>"
                        + "<td><button class='removeIngredient btn btn-sm btn-danger'>X</button></td>"
                        + "</tr>");

                    $(".removeIngredient").on("click", function () {
                        $(this).closest("tr").remove();
                    });
                });
            }
        });

        
    } else {
        recipeDto = {
            "instructions": [],
            "ingredients": []
        };
    }


    // Name
    $("#RecipeDto_Name").keyup(function () {
        // 1) Get input
        let nameValue = $(this).val();
        // 2) Show preview
        $("#nameView").text(nameValue);
    });


    // Description
    $("#RecipeDto_Description").keyup(function () {
        // 1) Get input
        let descriptionValue = $(this).val();
        // 2) Show preview
        $("#descriptionView").text(descriptionValue);
    });


    // Instructions
    $("#addInstruction").on("click", function () {
        event.preventDefault();
        // 1) Get input
        let input = $("#RecipeDto_Instructions").val();
        // 2) Show preview
        if ($.trim(input) != '') {
            $("#instructionsView").append("<li>"
                + input
                + "<button class='remove btn btn-outline-danger btn-sm'>X</button>"
                + "</li>");

            $(".remove").on("click", function () {
                $(this).parent().remove();
            });
        }
        $("#RecipeDto_Instructions").val("");
    });


    // Ingredients
    $("#addIngredient").on("click", function () {
        event.preventDefault();

        // 1) Get input
        let product = $("#products option:selected").val();
        let amount = $("#amount").val();
        let unit = $("#units option:selected").text();

        // 2) Show preview
        $("#ingredientsView").append(
            "<tr>"
            + "<td class='product'>"
            + product
            + "</td><td class='amount text-right'>"
            + amount
            + "</td><td class='unit text-right'>"
            + unit
            + "</td>"
            + "<td><button class='removeIngredient btn btn-sm btn-danger'>X</button></td>"
            + "</tr>");


        $(".removeIngredient").on("click", function () {
            $(this).closest("tr").remove();
        });


        // Restart the input fields
        $("#products").val("");
        $("#amount").val("");
        $("#units").val("");
    });


    // ------------------------------------------------------------


    // POST or PUT RecipeDto to DB
    $("#submitBtn").on("click", function () {
        event.preventDefault();

        recipeDto.name = $("#nameView").text();
        recipeDto.description = $("#descriptionView").text();

        recipeDto.instructions = [];
        $("#instructionsView li").each(function () {
            let element = $(this).text();
            recipeDto.instructions.push(element);
        });

        recipeDto.ingredients = [];
        $("#ingredientsView tr").each(function () {

            let newIngredient = {
                "productName": $(this).find($(".product")).text(),
                "amount": $(this).find($(".amount")).text(),
                "unit": $(this).find($(".unit")).text()
            };
            recipeDto.ingredients.push(newIngredient);

        });

        



        if ($("#RecipeDto_Id").val() > 0) {
            $.ajax({
                type: "PUT",
                url: "/api/recipes/" + $("#RecipeDto_Id").val(),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(recipeDto),
                success: function () {
                    location.href = "./Details/" + recipeDto.Id;
                }
            });
        } else {
            $.ajax({
                type: "POST",
                url: "/api/recipes",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(recipeDto),
                success: function () {
                    location.href = "./List";
                }
            });
        }

        
    });
});