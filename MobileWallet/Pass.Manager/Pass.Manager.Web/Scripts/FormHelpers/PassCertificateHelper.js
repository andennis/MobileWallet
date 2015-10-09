(function (helper, $) {
    helper.PassSiteDataHandler = function (d) {
        return $.extend({}, d, {
            "PassCertificateId": $('#PassCertificateId').val()
        });
    };

}(window.PassCertificate = window.PassCertificate || {}, jQuery));