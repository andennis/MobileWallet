(function ($) {

    function init() {
        $('body').on('click', '[data-popup-window]', null, function (e) {
            e.preventDefault();
            var $this = $(this);
            var wndId = $this.data("popup-window");

            var wnd = $("#" + wndId).data("kendoWindow");
            wnd.open();
            wnd.center();
        });
    }

    init();

}(jQuery));