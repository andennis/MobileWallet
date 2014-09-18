(function (grid, $) {
    grid.Reload = function(gridId) {
        $(gridId).DataTable().ajax.reload();
    }
}(window.grid = window.grid || {}, jQuery));