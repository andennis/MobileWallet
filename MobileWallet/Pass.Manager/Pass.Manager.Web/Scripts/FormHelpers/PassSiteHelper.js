(function (helper, $) {

    helper.PassProjectDataHandler = function () {
        return { PassSiteId: $("#PassSiteId").val() };
    }

    helper.UserDataHandler = function () {
        return { PassSiteId: $("#PassSiteId").val() };
    }

}(window.PassSite = window.PassSite || {}, jQuery));