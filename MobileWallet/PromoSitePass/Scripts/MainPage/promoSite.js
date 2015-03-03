$(function () {
    $('#passbookLogoImgTd').hover(function () {
        $('#passbookPrompt').css('display', 'block');
    },
    function () {
        $('#passbookPrompt').css('display', 'none');
    });
    $('#passwalletLogoImgTd').hover(function () {
        $('#passwalletPrompt').css('display', 'block');
    },
    function () {
        $('#passwalletPrompt').css('display', 'none');
    });
});

$(function () {
    $('.advantage').hover(
        function () {
            switch ($(this).attr('id')) {
                case 'advantageText1':
                    {
                        $('.advImage').removeClass('advImage1').removeClass('advImage2').removeClass('advImage3').addClass('advImage1');
                        $('#advNumberCircle1').css('background-color', 'white').css('color', '#2d586f');
                        break;
                    }
                case 'advantageText2':
                    {
                        $('.advImage').removeClass('advImage1').removeClass('advImage2').removeClass('advImage3').addClass('advImage2');
                        $('#advNumberCircle2').css('background-color', 'white').css('color', '#2d586f');
                        break;
                    }
                case 'advantageText3':
                    {
                        $('.advImage').removeClass('advImage1').removeClass('advImage2').removeClass('advImage3').addClass('advImage3');
                        $('#advNumberCircle3').css('background-color', 'white').css('color', '#2d586f');
                        break;
                    }
                case 'advantageText4':
                    {
                        $('.advImage').removeClass('advImage1').removeClass('advImage2').removeClass('advImage3').addClass('advImage1');
                        $('#advNumberCircle4').css('background-color', 'white').css('color', '#2d586f');
                        break;
                    }
            }
        },
        function () {
            switch ($(this).attr('id')) {
                case 'advantageText1':
                    {
                        $('#advNumberCircle1').css('background-color', '#2d586f').css('color', 'white');
                        break;
                    }
                case 'advantageText2':
                    {
                        $('#advNumberCircle2').css('background-color', '#2d586f').css('color', 'white');
                        break;
                    }
                case 'advantageText3':
                    {
                        $('#advNumberCircle3').css('background-color', '#2d586f').css('color', 'white');
                        break;
                    }
                case 'advantageText4':
                    {
                        $('#advNumberCircle4').css('background-color', '#2d586f').css('color', 'white');
                        break;
                    }
            }
        });
});

$(document).ready(function () {
    $("#up").click(function () {
        var curPos = $(document).scrollTop();
        var scrollTime = curPos / 1.73;
        $("body,html").animate({ "scrollTop": 0 }, scrollTime);
    });
});

$(function () {
    $('.contactText').hover(function (evt) {
        var tempClassName = evt.target.id.replace('Text', 'Icon');
        $('.' + tempClassName).css('fill', '#fd678a');
    },
    function (evt) {
        var tempClassName = evt.target.id.replace('Text', 'Icon');
        $('.' + tempClassName).css('fill', '#02d5ac');
    });
});

//Replace all SVG images with inline SVG
jQuery(window).load(function () {
    jQuery('img.imgIcon').each(function () {
        var $img = jQuery(this);
        var imgID = $img.attr('id');
        var imgClass = $img.attr('class');
        var imgURL = $img.attr('src');
        jQuery.get(imgURL, function (data) {
            // Get the SVG tag, ignore the rest
            var $svg = jQuery(data).find('svg');

            // Add replaced image's ID to the new SVG
            if (typeof imgID !== 'undefined') {
                $svg = $svg.attr('id', imgID);
            }
            // Add replaced image's classes to the new SVG
            if (typeof imgClass !== 'undefined') {
                $svg = $svg.attr('class', imgClass);
            }

            // Replace image with new SVG
            $img.replaceWith($svg);
            //$svg.css('fill', jQuery('#labelColorPicker').val());

            jQuery('#' + imgID + ' path').css('fill', '#f25b3b');
        }, 'xml');

    });
});

//Change active menu item
function changeActiveMenuItem(menuItem) {
    $('.menuPanel1 li').removeClass('active');
    switch (menuItem) {
        case 'mainPage':
            $('#mainPageMenuItem a').addClass('active'); break;
        case 'strategies':
            $('#strategiesMenuItem a').addClass('active'); break;
        case 'contact':
            $('#contactsMenuItem a').addClass('active'); break;
        default:
            $('#mainPageMenuItem').addClass('active'); break;
    }
}