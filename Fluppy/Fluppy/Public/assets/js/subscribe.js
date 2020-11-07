$(document).ready(function () {

    $('#subscribeButton').click(function (e) {
        e.preventDefault();
        var success = $('<div class="alert alert-success">Your message sent successfully!</div >');
        var error = $('<div class="alert alert-warning">A error occured!</div >');
        var erroralready = $('<div class="alert alert-warning">The Email is already in use!</div >');
        var textnull = $('<div class="alert alert-warning">Insert a email!</div>');
        var correctEmail = $('<div class="alert alert-warning">Include correct email!</div>');
        var categories = $("#responsed");
        var emailval = $('#subscribeInput').val();
        obj = {
            Email:emailval
        }
        if (!emailval.includes("@")) {
            categories.empty();
            categories.append(correctEmail);
        }
        else if (emailval !== "") {
            $.ajax({
                url: '/Home/Subscribe/', 
                type: 'POST', 
                dataType: 'json',
                data:obj,
                success: function (response) {
                     console.log(response);
                     if (response === "true") {
                         categories.empty();
                         categories.append(success);
                     }
                     else if (response === "already") {
                         categories.empty();
                         categories.append(erroralready);
                     }
                     else {
                         categories.empty();
                         categories.append(error);
                     }
                  }
            })
        }
        else {
            categories.empty();
            categories.append(textnull);
        }
    });
});