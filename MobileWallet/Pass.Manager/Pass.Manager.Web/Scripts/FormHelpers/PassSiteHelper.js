(function (helper, $) {

    helper.PassProjectDataHandler = function () {
        return { passSiteId: $("#PassSiteId").val() };
    }

    helper.UserDataHandler = function () {
        return { passSiteId: $("#PassSiteId").val() };
    }

}(window.PassSite = window.PassSite || {}, jQuery));