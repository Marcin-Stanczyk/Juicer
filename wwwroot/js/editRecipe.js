$(function () {

    // Creating new Recipe Data Transfer Object
    // Filling it with user input
    // Showing it's preview on screen at the same time
    // Posting to DB with jQuery.AJAX

    // -----------------------------------------------

    var recipeDto = {
        "instructions": "",
        "ingredients": []
    };


    // Name
    $("#Recipe_Name").keyup(function () {
        // 1) Get input
        let nameValue = $(this).val();
        // 2) Show preview
        $("#nameView").text(nameValue);
        // 3) Pass to DTO
        recipeDto.name = nameValue;
    });


    // Description
    $("#Recipe_Description").keyup(function () {
        // 1) Get input
        var descriptionValue = $(this).val();
        // 2) Show preview
        $("#descriptionView").text(descriptionValue);
        // 3) Pass to DTO
        recipeDto.description = descriptionValue;
    });


    // Instructions
    $("#addInstruction").on("click", function () {
        event.preventDefault();
        // 1) Get input
        let input = $("#Recipe_Instructions").val();
        // 2) Show preview
        if ($.trim(input) != '') {
            $("#instructionsView").append("<li>"
                + input
                + "<button class='remove btn btn-sm btn-danger'>X</button>"
                + "</li>");
            $("#Recipe_Instructions").val("");

            $(".remove").on("click", function () {
                $(this).parent().remove();
            });
        }
        // 3) Will be passed to DTO just before sending to DB
    });


    // Ingredients
    $("#addIngredient").on("click", function () {
        event.preventDefault();

        // 1) Get input
        let product = $("#products option:selected").val();
        let amount = $("#amount").val();
        let unit = $("#units option:selected").val();

        // 2) Show preview
        $("#ingredientsView").append("<li class='list-group-item'>"
            + $("#products option:selected").text()
            + " "
            + $("#amount").val()
            + " "
            + $("#units option:selected").text()
            + " </li>");

        // 3) Pass to DTO
        let newIngredient = {
            "productName": product,
            "amount": amount,
            "unit": unit
        };
        recipeDto.ingredients.push(newIngredient);

        // Restart the input fields
        $("#products").val("");
        $("#amount").val("");
        $("#units").val("");
    });


    // ------------------------------------------------------------


    // Post RecipeDto to DB
    $("#submitBtn").on("click", function () {
        event.preventDefault();

        // Pass instructions to DTO from instructions preview
        $("#instructionsView li").each(function () {
            recipeDto.instructions += $(this).text();
        });


        $.ajax({
            type: "post",
            url: "/api/recipes",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(recipeDto),
            success: function (result) {
                console.log('Data received: ');
                console.log(result);
            },
            error: function (request, status, error) {
                // If Recipe.Name is taken there will be 400 Bad Request response "Name already taken"
                alert(request.responseText);
            }
        });
    });
});