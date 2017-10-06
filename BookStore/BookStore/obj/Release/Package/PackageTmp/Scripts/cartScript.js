$(document).ready(function () {
    $("#remove").click(function (e) {

        e.preventDefault();
        $.ajax({

            url: $(this).attr("href"),
            type:"GET",// comma here instead of semicolon   
            success: function () {
                $.ajax({

                    url: "/Cart/OrderItems",
                    type: "GET",// comma here instead of semicolon   
                    success: function (result) {
                        alert("hello");
                        $("#orders-table").html(result);
                    }

                });
            }

        });

    });
});