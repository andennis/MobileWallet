(function ($) {

    var template = $.parseHTML('<div id="popupWindowContent"></div>');

    function show($wnd) {
        $.post();

        $wnd.center().open();
    }

    function close($wnd) {
        $wnd.close();
    }

    function init() {
        $('body').on('click', '[data-popup-window]', null, function (e) {
            e.preventDefault();
            var $this = $(this);
            var wndId = $this.data("popup-window");

            var wnd = $("#" + wndId).data("kendoWindow");
            show(wnd);
        });
    }

    init();

}(jQuery));