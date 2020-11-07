$(document).ready(function () {

    $(".categoriesContainer .addButton").click(function () {
        $.ajax({
            url: "/Product/GetPetTypes",
            type: "get",
            dataType: "json",
            success: function (response) {
                var categories = $(".categories");
                var div1 = $(' <div class="col-12 col-sm-12 mt-2"></div>');
                var select = $('<select class="form-control selects" name="PetTypeId" id="exampleFormControlSelect1">');

                for (var i = 0; i < response.length; i++) {
                    var option = $(' <option value="' + response[i].Id + '">' + response[i].Type + '</option>');
                    select.append(option);
                }

                div1.append(select);
                categories.append(div1);
            },
            error: function (error) {
                alert("You are logged out!");
            }
        });
    });
});