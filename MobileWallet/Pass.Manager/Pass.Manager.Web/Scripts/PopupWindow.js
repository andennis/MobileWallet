(function (helper, $) {
    var popupWindow = null;
    var $popupWindowDiv = null;

    helper.ajaxOnSuccess = function (data) {
        if (data.Success) {
            var fncSuccess = $popupWindowDiv.data("popup-success");
            if (fncSuccess)
                eval(fncSuccess + "(data)");

            close();

        }
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
        //popupWindow.destroy();
        popupWindow = null;
        $popupWindowDiv = null;
    }

    function windowClose(e) {
        e.sender.unbind("close", windowClose);
        e.sender.content(null);
    }

    function init() {
        $('body').on('click', '[data-popup-window]', null, function (e) {
            e.preventDefault();
            var wndId = $(this).data("popup-window");
            $popupWindowDiv = $("#" + wndId);
            show($popupWindowDiv);
        });
    }

    init();

}(window.PopupWindow = window.PopupWindow || {}, jQuery));