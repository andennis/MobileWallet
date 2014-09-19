(function (action, $) {
    action.Init = function ($form) {

        $(":submit", $form).click(function (e) {
            e.preventDefault();
            var actionUrl = $(this).data("action");
            if (actionUrl)
            {
                var form = $(this).closest("form");
                form.attr("action", actionUrl);
                form.submit();
            }
        });

        $(":not(:submit)[data-action]", $form).click(function(e) {
            e.preventDefault();
            window.location.href = $(this).data("action");
        });
    }

}(window.action = window.action || {}, jQuery));