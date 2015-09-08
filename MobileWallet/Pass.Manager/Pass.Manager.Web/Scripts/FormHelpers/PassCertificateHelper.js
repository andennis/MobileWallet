(function (helper, $) {
    helper.PassSiteDataHandler = function() {
        return { PassCertificateId: $("#PassCertificateId").val() };
    };

}(window.PassCertificate = window.PassCertificate || {}, jQuery));