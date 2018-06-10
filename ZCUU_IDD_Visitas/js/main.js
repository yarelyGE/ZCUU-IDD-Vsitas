$(function () {
    $(window).load(function () {
        // Animate loader off screen
        $(".se-pre-con").fadeOut("slow");;
    });

    //Weather
    $(document).ready(function () {
        $.simpleWeather({
            location: 'Chihuahua, MX',
            woeid: '',
            unit: 'C',
            success: function (weather) {
                html = '<h3><i class="icon-' + weather.code + '"></i> ' + weather.temp + '&deg;' + weather.units.temp + '</h3>';

                $("#weather").html(html);
            },
            error: function (error) {
                $("#weather").html('<p>' + error + '</p>');
            }
        });
    });

    //menu home
    $('.home-principal .info-apu:first').show();
    $('.menu-apu a:first').addClass('activo');

    $('.menu-apu a').on('click', function () {

        $('.menu-apu a').removeClass('activo');
        $(this).addClass('activo');
        $('.ocultar').hide();
        var enlace = $(this).attr('href');
        $(enlace).fadeIn(500);
        return false;
    })


})







