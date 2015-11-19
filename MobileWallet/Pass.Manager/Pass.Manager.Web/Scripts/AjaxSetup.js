(function (helper, $) {

    helper.Init = function (loginUrl) {
        $(document).ajaxError(function (event, request) {
            if (request.status == 401) {
                alert("Your session has expired. You will be redirected to login page");
                window.location = loginUrl;
            } else {
                alert(request.responseText);
            }
        });
    };

}(window.AjaxSetup = window.AjaxSetup || {}, jQuery));

