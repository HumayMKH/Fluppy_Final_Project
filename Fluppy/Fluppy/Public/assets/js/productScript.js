// WISHLIST

$(document).ready(function () {

    //ADD WISHLIST

    $(".addWishList").click(function (e) {
        e.preventDefault();
        var id = parseInt($(this).data("id"));
        var This = $(this);

        $.ajax({
            url: "/Shop/AddToWishList/" + id,
            type: "get",
            dataType: "json",
            success: function (response) {
                if (response === "success-true") {
                    var oldCountTrue = parseInt($(".wishlistCount").text());
                    oldCountTrue++;
                    $(".wishlistCount").text(oldCountTrue);

                    This.addClass("addedWishlist");

                } else if (response === "success-false") {
                    var oldCountFalse = parseInt($(".wishlistCount").text());
                    oldCountFalse--;
                    $(".wishlistCount").text(oldCountFalse);
                    This.removeClass("addedWishlist");
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    //REMOVE WISHLIST

    $(".addWishListRemove").click(function (e) {
        e.preventDefault();
        var id = parseInt($(this).data("id"));
        var This = $(this);
        $(this).parent().parent().remove();

        $.ajax({
            url: "/Shop/AddToWishList/" + id,
            type: "get",
            dataType: "json",
            success: function (response) {
                if (response === "success-true") {
                    var oldCountTrue = parseInt($(".wishlistCount").text());
                    oldCountTrue++;
                    $(".wishlistCount").text(oldCountTrue);

                    This.addClass("addedWishlist");

                } else if (response === "success-false") {
                    var oldCountFalse = parseInt($(".wishlistCount").text());
                    oldCountFalse--;
                    $(".wishlistCount").text(oldCountFalse);
                    This.removeClass("addedWishlist");
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    //ADD CART

    $(".addToCart").click(function (e) {
        Id = parseInt($(this).data("id"));
        Count = $(".productCount").val();

        refreshCart(Id, Count);
    });
    $(".cartCountIndex").change(function () {
        var Id = parseInt($(this).data("id"));
        var Count = parseFloat($(this).val());
        var price = parseFloat($(this).data("price"));

        refreshCart(Id, Count);

        //Own total price
        $(".TotalPrice" + Id + "").text(Count * price + ",00");

        //All total price
        var inputs = $(".cartCountIndex");
        var AllTotalPrice = 0;
        for (var i = 0; i < inputs.length; i++) {
            AllTotalPrice += (parseFloat(inputs[i].value) * parseFloat(inputs[i].dataset.price));
        }

        $(".cart-total").text(AllTotalPrice + ",00");
    });

    //REMOVE CART

    $(".btn-remove").click(function (e) {
        e.preventDefault();

        var Id = parseInt($(this).data("id"));
        var ThisTotalPrice = parseFloat($(".TotalPrice" + Id + "").text());

        $(this).parent().parent().remove();
        //Decrease count
        var oldCountFalse = parseInt($(".cartCount").text());
        oldCountFalse--;
        $(".cartCount").text(oldCountFalse);

        //Decrease Total price
        var TotalPrice = parseFloat($(".cart-total").text());
        TotalPrice = TotalPrice - ThisTotalPrice;
        $(".cart-total").text(TotalPrice + ",00");

        //Remove from cookie
        $.ajax({
            url: "/Shop/RemoveFromCart/" + Id,
            type: "get",
            dataType: "json",
            success: function (response) {
                console.log(response);
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    //REFRESH CART

    function refreshCart(Id, Count) {
        var cartElem = {
            id: Id,
            count: Count
        };

        $.ajax({
            url: "/Shop/AddToCart/",
            type: "get",
            dataType: "json",
            data: cartElem,
            success: function (response) {
                if (response === "success-true") {
                    var oldCountTrue = parseInt($(".cartCount").text());
                    oldCountTrue++;
                    $(".cartCount").text(oldCountTrue);
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
});

