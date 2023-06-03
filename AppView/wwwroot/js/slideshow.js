$(document).ready(function () {
    $('.customer-logos').slick({
        slidesToShow: 6,
        slidesToScroll: 1,
        autoplay: true,
        pauseOnFocus: false,
        pauseOnHover: false,
        touchThreshold:100,
        autoplaySpeed: 6000,
        arrows: false,
        dots: false,
        pauseOnHover: false,
        infinite: true,
        responsive: [{
            breakpoint: 768,
            settings: {
                slidesToShow: 4,
                swipe: false // tắt tính năng swipe
            }
        }, {
            breakpoint: 520,
            settings: {
                slidesToShow: 3,
                swipe: false // tắt tính năng swipe
            }
        }]
    });
})