(function (helper, $) {
    var _passSiteId;

    helper.Init = function(passSiteId) {
        _passSiteId = passSiteId;
    }
    helper.PassProjectDataHandler = function() {
        return { passSiteId: _passSiteId };
    }
}(window.PassSite = window.PassSite || {}, jQuery));