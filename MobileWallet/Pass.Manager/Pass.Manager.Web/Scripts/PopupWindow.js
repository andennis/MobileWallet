(function (helper, $) {
    var popupWindow = null;
    var $popupWindowDiv = null;

    helper.ajaxOnSuccess = function (data) {
        if (data.Success) {
            var fncSuccess = $popupWindowDiv.data("popup-success");
            if (fncSuccess) {
                eval(fncSuccess + "(data)");
            }
            close();
        } else {
            initCloseBtn($popupWindowDiv);
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

            initCloseBtn($wndDiv);
            popupWindow.bind("close", windowClose);
            popupWindow.center().open();
        });
    }

    function initCloseBtn($wndDiv) {
        var $form = $("#popupWindowForm", $wndDiv);
        $("#btnCancel", $form).click(function () {
            close();
        });
    }

    function close() {
        popupWindow.close();
    }

    function windowClose(e) {
        popupWindow = null;
        $popupWindowDiv = null;

        e.sender.unbind("close", windowClose);
        e.sender.content(null);
    }

    function init() {
        $('body').on('click', '[data-popup-window]', null, function (e) {
            e.preventDefault();
            var $this = $(this);
            var wndId = $this.data("popup-window");
            $popupWindowDiv = $("#" + wndId);

            var action = $this.attr("href");
            if (action == null)
                action = $this.data("action");
            if (action)
                $popupWindowDiv.data("popup-action", action);

            show($popupWindowDiv);
        });
    }

    init();

}(window.PopupWindow = window.PopupWindow || {}, jQuery));