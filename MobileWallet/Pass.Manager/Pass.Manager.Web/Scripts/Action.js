(function (action, $) {
    action.InitForm = function ($form) {

        $(":submit", $form).click(function (e) {
            e.preventDefault();
            var form = $(this).closest("form");
            var actionUrl = $(this).data("form-action");
            if (actionUrl)
                form.attr("action", actionUrl);

            form.submit();
        });

        $(":not(:submit)[data-form-action]", $form).click(function (e) {
            e.preventDefault();
            var actionUrl = $(this).data("form-action");
            if (actionUrl)
                window.location.href = actionUrl;
        });

        $("[data-ajax-action]", $form).click(function (e) {
            e.preventDefault();
            var actionUrl = $(this).data("ajax-action");
            if (!actionUrl)
                return;

            var confMsg = $(this).attr("confirmMessage");
            if (!confMsg)
                confMsg = "Are you sure you want to perform the operation?";
            if (!confirm(confMsg))
                return;

            var fncSuccess = $(this).data("on-success");
            var fncFail = $(this).data("on-fail");

            var postResult = $.post(actionUrl, function (data) {
                
                if (data.Success) {
                    if (fncSuccess)
                        eval(fncSuccess + "(data.Data)");

                    if (data.Message)
                        alert(data.Message);
                } else {
                    if (fncFail)
                        eval(fncFail + "(data.Data)");

                    if (data.Message)
                        alert(data.Message);
                }
                
            });

            postResult.fail(function () {
                alert("An error occurred");
            });

        });

    }

}(window.action = window.action || {}, jQuery));