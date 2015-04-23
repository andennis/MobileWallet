(function (grid, $) {
    grid.Init = function($grid) {
        $grid.on('draw.dt', function() {

            $("[data-grid-action]", $grid).click(function (e) {
                e.preventDefault();
                var actionUrl = $(this).data("grid-action");
                if (!actionUrl)
                    return;

                var confMsg = $(this).attr("confirmMessage");
                if (!confMsg)
                    confMsg = "Are you sure you want to perform the operation?";
                if (!confirm(confMsg))
                    return;

                var postResult = $.post(actionUrl, function (data) {

                    grid.Reload($grid);
                    if (data.Message)
                        alert(data.Message);

                    /*
                    if (data.Success != null) {
                        if (data.Success) 
                            grid.Reload($grid);
                        else
                            alert(data.Message);
                    }
                    else {
                        grid.Reload($grid);
                    }
                    */
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