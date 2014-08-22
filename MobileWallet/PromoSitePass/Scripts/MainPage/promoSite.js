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
    $('.advTableTd').hover(
        function () {
            switch ($(this).attr('id')) {
                case 'advTableTd1':
                    {
                        $('#advTableTd2, #advTableTd3, #advTableTd4').css('opacity', '0.4');
                        $('#advantageImg2, #advantageImg3').css('display', 'none');
                        $('#advantageImg1').css('display', 'block');
                        $('#plusIcon1').css('display', 'none');
                        $('#bluePlusIcon1').css('display', 'block');
                        break;
                    }
                case 'advTableTd2':
                    {
                        $('#advTableTd1, #advTableTd3, #advTableTd4').css('opacity', '0.4');
                        $('#advantageImg1, #advantageImg3').css('display', 'none');
                        $('#advantageImg2').css('display', 'block');
                        $('#plusIcon2').css('display', 'none');
                        $('#bluePlusIcon2').css('display', 'block');
                        break;
                    }
                case 'advTableTd3':
                    {
                        $('#advTableTd1, #advTableTd2, #advTableTd4').css('opacity', '0.4');
                        $('#advantageImg1, #advantageImg2').css('display', 'none');
                        $('#advantageImg3').css('display', 'block');
                        $('#plusIcon3').css('display', 'none');
                        $('#bluePlusIcon3').css('display', 'block');
                        break;
                    }
                case 'advTableTd4':
                    {
                        $('#advTableTd1, #advTableTd2, #advTableTd3').css('opacity', '0.4');
                        $('#plusIcon4').css('display', 'none');
                        $('#bluePlusIcon4').css('display', 'block');
                        break;
                    }
            }
        },
        function () {
            switch ($(this).attr('id')) {
                case 'advTableTd1':
                    {
                        $('#advTableTd2, #advTableTd3, #advTableTd4').css('opacity', '1');
                        $('#plusIcon1').css('display', 'block');
                        $('#bluePlusIcon1').css('display', 'none');
                        break;
                    }
                case 'advTableTd2':
                    {
                        $('#advTableTd1, #advTableTd3, #advTableTd4').css('opacity', '1');
                        $('#plusIcon2').css('display', 'block');
                        $('#bluePlusIcon2').css('display', 'none');
                        break;
                    }
                case 'advTableTd3':
                    {
                        $('#advTableTd1, #advTableTd2, #advTableTd4').css('opacity', '1');
                        $('#plusIcon3').css('display', 'block');
                        $('#bluePlusIcon3').css('display', 'none');
                        break;
                    }
                case 'advTableTd4':
                    {
                        $('#advTableTd1, #advTableTd2, #advTableTd3').css('opacity', '1');
                        $('#plusIcon4').css('display', 'block');
                        $('#bluePlusIcon4').css('display', 'none');
                        break;
                    }
            }
        });
});

$(document).ready(function() {
    $("#up").click(function() {
        var curPos = $(document).scrollTop();
        var scrollTime = curPos / 1.73;
        $("body,html").animate({ "scrollTop": 0 }, scrollTime);
    });
});

$(function() {
    $('.contactText').hover(function (evt) {
        var tempClassName = evt.target.id.replace('Text', 'Icon');
        $('.' + tempClassName).css('fill', '#2fe2bf');
    },
    function (evt) {
        var tempClassName = evt.target.id.replace('Text', 'Icon');
        $('.' + tempClassName).css('fill', '#1abc9c');
    });
});