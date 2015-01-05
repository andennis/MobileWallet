(function ($) {

    var $ambientDiv = $($.parseHTML("<div id='popupWindowContent'></div>"));

    function show($wndDiv) {
        var wndAction = $wndDiv.data("popup-action");
        var dataHandler = $wndDiv.data("popup-datahandler");
        var prms = dataHandler ? eval(dataHandler + "()") : null;
        var wnd = $wndDiv.data("kendoWindow");

        $.get(wndAction, prms, function (data) {
            var $ambientDivCopy = $ambientDiv.clone();
            $ambientDivCopy.html(data);
            wnd.content($ambientDivCopy[0].outerHTML);

            var $form = $("#popupWindowForm", $wndDiv);
            $("#btnCancel", $form).click(function () {
                close($wndDiv);
            });

            $form.submit(function(e) {
                e.preventDefault();
                close($wndDiv);
            });

            wnd.center().open();
        });
    }

    function close($wndDiv) {
        var wnd = $wndDiv.data("kendoWindow");
        $.post()
        wnd.close($wndDiv);
    }

    function init() {
        $('body').on('click', '[data-popup-window]', null, function (e) {
            e.preventDefault();
            var $this = $(this);
            var wndId = $this.data("popup-window");
            show($("#" + wndId));
        });
    }

    init();

}(jQuery));