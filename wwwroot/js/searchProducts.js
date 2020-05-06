$(function () {

    let products = [];
    let filtered = false;
    const minSearchLetters = 2;
    const maxSuggestions = 3;
    const notFound = -1;

    $.ajax({
        url: "/api/products",
        success: function (response) {
            products = response;
            showGalleryOf(products);
        }
    });

    $("#search-input").on("keyup", function() {
        clearSearch();

        // using RegExp to clear input from whitespaces
        let searchValue = $(this).val().replace(/\s+/g, " ").trim();;
        
        if (searchValue.length >= minSearchLetters) {
            let searchResult = searchByNameAndCategory(searchValue, products);
            showSearch(searchResult);
        } else {
            showGalleryOfAll();
        }
    });

    function clearSearch() {
        $("#search-list").empty();
        $("#search-list").css("visibility", "hidden");
        $("#see-all-btn").css("visibility","hidden");
    };

    function searchByNameAndCategory(searchValue, searchArray) {
       
        let results = [];
        results = searchArray.filter(function (element) {
            return results.indexOf(element) === notFound
                    && element.name.toLowerCase().includes(searchValue)
                    || element.category.toLowerCase().startsWith(searchValue);
        });

        if (results.length) {
            return results;
        } else {
            return searchValue;
        }
    };

    function showSearch(searchResult) {
        filtered = true;
        showSearchList();

        if (Array.isArray(searchResult)) {
            fillSearchWith(searchResult, maxSuggestions);
            showGalleryOf(searchResult);
            return;
        }

        if (typeof(searchResult) === "string") {
            fillSearchWithNotFound(searchResult);
            clearGallery();
            return;
        }        
    };

    function showSearchList() {
        $("#search-list").css("visibility","visible");
        showSeeAllButton();
    };

    function fillSearchWith(suggestions, amountLimit) {
        let shownSuggestions = suggestions.slice(0, amountLimit);
        shownSuggestions.forEach(function(element) {
            $("#search-list").append(`<li class="list-group-item" link-id="${element.id}">${element.name}</li>`);
        });
        activateSearchListEvents();
    };

    function fillSearchWithNotFound(searchValue) {
        $("#search-list").append(
            `<li class="list-group-item">Didn't find any products with <strong><mark>${searchValue}</strong></mark> in their name.</li>`);
    };

    function activateSearchListEvents() {
        let initialInput = $("#search-input").val();

        $("#search-list li").hover(function() {
            let item = $(this).text();
            setInput(item);

            $(this).addClass("active");
            $(this).css("cursor", "pointer");
        }, function() {
            setInput(initialInput);

            $(this).removeClass("active");
            $(this).css("cursor", "default");
        });

        $("#search-list li").on("click", function() {
            let id = $(this).attr("link-id");
            let product = products.find(function(element) {
                return element.id == id;
            });
            showGalleryOfOne(product);
            initialInput = product.name;
        });
    };

    function showSeeAllButton() {
        $("#see-all-btn").css("visibility","visible");
        $("#see-all-btn").on("click", function() {
            clearSearch();
            showGalleryOfAll();
            setInput("");
        });
    }

    function setInput(value) {
        $("#search-input").val(value);
    };

    function clearGallery() {
        $("#gallery").empty();
    };
    
    function showGalleryOfAll() {
        if (filtered) {
            showGalleryOf(products);
            filtered = false;
        }
    };

    function showGalleryOf(elements) {
        clearGallery();

        let categories = [];
        elements.forEach(function(element) {
            if (categories.indexOf(element.category) == notFound) {
                categories.push(element.category);
            }
        });

        categories.sort();
        categories.forEach(function(category) {
            appendCategory(category);

            let productsWithThisCategory = elements.filter(function(element) {
                return element.category == category;
            });
            productsWithThisCategory.forEach(function(product) {
                appendProductToCategory(product, category);
            });
        });   
    };

    function showGalleryOfOne(element) {
        clearGallery();
        appendCategory(element.category);
        appendProductToCategory(element, element.category);
    }

    function appendCategory(category) {
        $("#gallery").append(
            `<h3>${category}</h3>
            <hr>
            <div class="d-flex flex-row flex-wrap align-items-center" id="${category}">`
        );
    };

    function appendProductToCategory(product, category) {
        $(`#${category}`).append(
            `<div class="col-md-2 col-4">
                <a href="./Details/${product.id}"> 
                    <figure class="text-center">
                        <img src="${product.photoPath}" class="img-cover img-nohover img-fluid">
                        <figcaption class="text-dark">${product.name}</figcaption>
                    </figure>
                </a>
            </div>`
        );
    };
});