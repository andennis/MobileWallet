$(document).on('change', '.btn-file :file', function () {
    var input = $(this),
        numFiles = input.get(0).files ? input.get(0).files.length : 1,
        label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
    var textInput = $(this).parents('.form-group').find(':text'),
            log = numFiles > 1 ? numFiles + ' files selected' : label;

    if (textInput.length) {
        textInput.val(log);
    } else {
        if (log) alert(log);
    }
});
