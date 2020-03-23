$(function () {


    // Populate Name & Description on screen
    $("#Recipe_Name").keyup(function () {
        var nameVal = $(this).val();
        $("#nameView").text(nameVal);
    });

    $("#Recipe_Description").keyup(function () {
        var descriptionVal = $(this).val();
        $("#descriptionView").text(descriptionVal);
    });

    $("#Recipe_Instructions").keyup(function () {
        var instructionsVal = $(this).val();
        $("#instructionsView").text(instructionsVal);
    });

});