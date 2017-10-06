$(document).ready(function () {
    $('#myPager').on('click', 'a', function () {
        $.ajax({
            url: this.href,
            type: 'GET',
            cache: false,
            success: function (result) {
                $('#comms').html(result);
            }
        });
        return false;
    });
});