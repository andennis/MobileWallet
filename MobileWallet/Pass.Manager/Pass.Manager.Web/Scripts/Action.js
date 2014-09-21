(function (action, $) {
    action.Init = function ($form) {

        $(":submit", $form).click(function (e) {
            e.preventDefault();
            var form = $(this).closest("form");
            var actionUrl = $(this).data("action");
            if (actionUrl)
                form.attr("action", actionUrl);

            form.submit();
        });

        $(":not(:submit)[data-action]", $form).click(function(e) {
            e.preventDefault();
            var actionUrl = $(this).data("action");
            if (actionUrl)
                window.location.href = actionUrl;
        });
    }

}(window.action = window.action || {}, jQuery));