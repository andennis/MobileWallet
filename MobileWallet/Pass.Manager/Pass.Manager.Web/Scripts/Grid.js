(function (grid, $) {
    grid.Init = function($grid) {
        $grid.on('draw.dt', function() {

            $("[data-action]", $grid).click(function (e) {
                e.preventDefault();
                var actionUrl = $(this).data("action");
                if (!actionUrl)
                    return;

                if (!confirm("Are you sure you want to delete?"))
                    return;

                var postResult = $.post(actionUrl, function (data) {
                    if (data.Success != null) {
                        if (data.Success) {
                            grid.Reload($grid);
                        }
                        else
                            alert(data.Message);
                    } else {
                        grid.Reload($grid);
                    }
                });
                postResult.fail(function () {
                    alert("An error occurred");
                });

            });

        });
    }

    grid.Reload = function($grid) {
        $grid.DataTable().ajax.reload();
    }

}(window.grid = window.grid || {}, jQuery));