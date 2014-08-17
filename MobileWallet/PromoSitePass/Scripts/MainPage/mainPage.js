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
    jQuery('.advTableTd').hover(
        function () {
            switch ($(this).attr('id')) {
                case 'advTableTd1':
                    {
                        jQuery('#advTableTd2, #advTableTd3, #advTableTd4').css('opacity', '0.4');
                        jQuery('#advantageImg2, #advantageImg3').css('display', 'none');
                        jQuery('#advantageImg1').css('display', 'block');
                        jQuery('#plusIcon1').css('display', 'none');
                        jQuery('#bluePlusIcon1').css('display', 'block');
                        break;
                    }
                case 'advTableTd2':
                    {
                        jQuery('#advTableTd1, #advTableTd3, #advTableTd4').css('opacity', '0.4');
                        jQuery('#advantageImg1, #advantageImg3').css('display', 'none');
                        jQuery('#advantageImg2').css('display', 'block');
                        jQuery('#plusIcon2').css('display', 'none');
                        jQuery('#bluePlusIcon2').css('display', 'block');
                        break;
                    }
                case 'advTableTd3':
                    {
                        jQuery('#advTableTd1, #advTableTd2, #advTableTd4').css('opacity', '0.4');
                        jQuery('#advantageImg1, #advantageImg2').css('display', 'none');
                        jQuery('#advantageImg3').css('display', 'block');
                        jQuery('#plusIcon3').css('display', 'none');
                        jQuery('#bluePlusIcon3').css('display', 'block');
                        break;
                    }
                case 'advTableTd4':
                    {
                        jQuery('#advTableTd1, #advTableTd2, #advTableTd3').css('opacity', '0.4');
                        jQuery('#plusIcon4').css('display', 'none');
                        jQuery('#bluePlusIcon4').css('display', 'block');
                        break;
                    }
            }
        },
        function () {
            switch ($(this).attr('id')) {
                case 'advTableTd1':
                    {
                        jQuery('#advTableTd2, #advTableTd3, #advTableTd4').css('opacity', '1');
                        jQuery('#plusIcon1').css('display', 'block');
                        jQuery('#bluePlusIcon1').css('display', 'none');
                        break;
                    }
                case 'advTableTd2':
                    {
                        jQuery('#advTableTd1, #advTableTd3, #advTableTd4').css('opacity', '1');
                        jQuery('#plusIcon2').css('display', 'block');
                        jQuery('#bluePlusIcon2').css('display', 'none');
                        break;
                    }
                case 'advTableTd3':
                    {
                        jQuery('#advTableTd1, #advTableTd2, #advTableTd4').css('opacity', '1');
                        jQuery('#plusIcon3').css('display', 'block');
                        jQuery('#bluePlusIcon3').css('display', 'none');
                        break;
                    }
                case 'advTableTd4':
                    {
                        jQuery('#advTableTd1, #advTableTd2, #advTableTd3').css('opacity', '1');
                        jQuery('#plusIcon4').css('display', 'block');
                        jQuery('#bluePlusIcon4').css('display', 'none');
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