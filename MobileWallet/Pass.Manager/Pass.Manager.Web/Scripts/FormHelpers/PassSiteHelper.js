(function (helper, $) {
    var _passSiteId;

    helper.Init = function(passSiteId) {
        _passSiteId = passSiteId;
    }
    helper.PassProjectDataHandler = function () {
        return { passSiteId: $("#PassSiteId").val() };
    }
}(window.PassSite = window.PassSite || {}, jQuery));