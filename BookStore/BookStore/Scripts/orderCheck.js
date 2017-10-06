window.onload = function () {
    $.ajax({
        url: "/Cart/GetBooksQuantity",
        success: function (result) {
            if (result == 0) {
                $('#create-order').remove();
            }
        }
    });
}