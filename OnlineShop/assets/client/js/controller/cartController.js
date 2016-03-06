var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $("#btnTiepTucMuaHang").off("click").on("click", function () {
            window.location.href = "/";
        });


        $("#btnXoaGioHang").off("click").on("click", function () {
            $.ajax({
                url: "/Cart/DeleteAll",
                type: "POST",
                dataType: "json",
                success: function (data) {
                    if (data.status == true)
                        window.location.href = "/gio-hang";
                    else alert("Lỗi xóa giỏ hàng");
                },
                error: function () {

                }

            });
        });

        $(".btnXoaSPGioHang").off("click").on("click", function () {
            var id = $(this).data("id");
            $.ajax({
                url: "/Cart/Delete",
                type: "POST",
                dataType: "json",
                data: { id: id },
                success: function (data) {
                    if (data.status == true)
                        window.location.href = "/gio-hang";
                    else alert("Lỗi xóa giỏ hàng");
                },
                error: function () {

                }

            });
        })

        $("#btnCapNhatGioHang").off("click").on("click", function () {
            var listProduct = $('.txtquantity');
            var model = [];
            $.each(listProduct, function (i, item) {
                model.push({
                    Quantity: $(item).val(),
                    Product:{
                        ID: $(item).data("id")
                    }
                })
            });

            $.ajax({
                url: "/Cart/Update",
                type: "POST",
                dataType: "json",
                data: { model: JSON.stringify(model) },
                success:function(data)
                {
                    if (data.status == true)
                        window.location.href = "/gio-hang";
                    else alert("Lỗi cập nhật giỏ hàng");
                },
                error:function()
                {

                }
            });
        });

        $("#btnThanhToan").off("click").on("click", function () {
            window.location.href = "/thanh-toan";
        });

    }
};
cart.init();
