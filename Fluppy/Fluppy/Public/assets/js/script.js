$(document).ready(function () {

    // scroll nav js

    $(window).scroll(ScrollChange);
    function ScrollChange() {
        if ($(document).scrollTop() > 0) {
            $('#stickyy').css("top", "0")
        }
        else {
            $('#stickyy').css("top", "54px")
        }
    };

    $(window).on('load', function () {
        ScrollChange();
    });

    // testimonials js

    $('#testimonials .owl-carousel').owlCarousel({
        loop: true,
        margin: 10,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            }
        }
    });

    // Adopt details js

    $('#adopt-details .owl-carousel').owlCarousel({
        loop: true,
        nav: true,
        dots: false,
        margin: 10,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            }
        }
    });

    // adopt article js

    $('#articles .owl-carousel').owlCarousel({
        loop: true,
        margin: 10,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            }
        }
    })

    // sevice js slider

    $('#service-slider .owl-carousel').owlCarousel({
        loop: true,
        margin: 30,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            },
            576: {
                items: 2
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            }
        }
    });

     // shop-slider js 

    $('#shop-slider .owl-carousel').owlCarousel({
        loop: true,
        dots: false,
        nav: true,
        margin: 30,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            },
            576: {
                items: 2
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            },
            1200: {
                items: 5
            }
        }
    });

    // team-slider js

    $('#team-slider .owl-carousel').owlCarousel({
        loop: true,
        nav: true,
        dots: false,
        margin: 30,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            },
            576: {
                items: 2
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            }
        }
    });

    //adopt click show content js

    $(".content-adopt").hide();
    $(".adopt-card").click(function () {
        $(this).children(".content-adopt").toggle();
    });

    //team hover show content js

    $(".team-content").hide();
    $(".team-image").mouseover(function () {
        $(this).children(".team-content").show();
    });
    $(".team-image").mouseleave(function () {
        $(this).children(".team-content").hide();
    });

    //Counter js

    $(window).scroll(function () {
        if ($(document).scrollTop() > 1500) {
            
            if ($(".counter-area").length > 0) {
                $(".counter").counter({
                    autoStart: true, // true/false, default: true
                    duration: 2000, // milliseconds, default: 1500
                    countFrom: 1,// start counting at this number, default: 0
                    countTo: 1,// count to this number, default: 0
                    runOnce: true,// only run the counter once, default: false
                });
            }

        }
    });

    //Product Details image js

    $('.sp-wrap').smoothproducts();

    // Datetimepicker

    $('#DateAndTime').datetimepicker({
        format: 'd.m.Y H:i',
        minDate: 0,
        step: 15,
        minTime: '9:30',
        maxTime: '17:01',
        yearStart: 2020,
        disabledWeekDays: [0, 6]
    });

});


 



