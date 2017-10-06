window.onload = function () {
    $.ajax({
        url: "/Catalog/IsCommentsAvailable",
        type: "GET",
        success: function (result) {
            if (result == true) {
                $('#CommentArea').attr('placeholder', 'Напишите отзыв о книге )');
                $('#CommentArea').prop('disabled', false)
            }
            else {
                $('#CommentArea').attr('placeholder', 'Комментарии доступны только для зарегистрированных пользователей');
                $('#CommentArea').prop('disabled', true);
            } 
        }
    });
    $("#add-btn").click(function (e) {

        e.preventDefault();
        $.ajax({

            url: $(this).attr("href"),
            type:"GET",// comma here instead of semicolon   
            success: function () {
                $("#add-message").animate({ opacity: 1.0 }, 500);
                $("#add-message").animate({ opacity: 0.0 }, 500);
                $.ajax({
                    url: "/Cart/GetBooksQuantity",
                    type: "GET",
                    success: function (result) {
                        $("#shop-count").html(result);
                    }
                });
            }

        });

    });
};