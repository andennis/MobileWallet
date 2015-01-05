(function (helper, $) {
    var popupWindow = null;

    helper.ajaxOnSuccess = function (data) {
        if (data.Success)
            close();
    }

    var $ambientDiv = $($.parseHTML("<div id='popupWindowContent'></div>"));

    function show($wndDiv) {
        var wndAction = $wndDiv.data("popup-action");
        var dataHandler = $wndDiv.data("popup-datahandler");
        var prms = dataHandler ? eval(dataHandler + "()") : null;
        popupWindow = $wndDiv.data("kendoWindow");

        $.get(wndAction, prms, function (data) {
            var $ambientDivCopy = $ambientDiv.clone();
            $ambientDivCopy.html(data);
            popupWindow.content($ambientDivCopy[0].outerHTML);

            var $form = $("#popupWindowForm", $wndDiv);
            $("#btnCancel", $form).click(function () {
                close();
            });

            /*
            var fncSuccess = $wndDiv.data("popup-success");
            if (fncSuccess)
                $form.data("ajax-success", fncSuccess);
            */

            /*
            $form.submit(function(e) {
                e.preventDefault();
                close($wndDiv);
            });
            */

            //wnd.unbind("close", windowClose);
            popupWindow.bind("close", windowClose);
            popupWindow.center().open();
        });
    }

    function close() {
        popupWindow.close();
    }

    function windowClose(e) {
        e.sender.unbind("close", windowClose);
        e.sender.content(null);
    }

    function init() {
        $('body').on('click', '[data-popup-window]', null, function (e) {
            e.preventDefault();
            var wndId = $(this).data("popup-window");
            show($("#" + wndId));
        });
    }

    init();

}(window.PopupWindow = window.PopupWindow || {}, jQuery));