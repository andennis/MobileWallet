(function ($) {

    function show($wndDiv) {
        var wndAction = $wndDiv.data("popup-action");
        var dataHandler = $wndDiv.data("popup-datahandler");
        var prms = dataHandler ? eval(dataHandler + "()") : null;
        var wnd = $wndDiv.data("kendoWindow");

        $.get(wndAction, prms, function (data) {
            wnd.content(data);
            wnd.center().open();
        });
    }

    function close($wndDiv) {
        var wnd = $wndDiv.data("kendoWindow");
        wnd.close();
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