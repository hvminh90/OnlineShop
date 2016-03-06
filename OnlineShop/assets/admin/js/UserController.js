var user = {
    init: function () {

        user.registerEvent();
    },

    registerEvent: function () {
        $('.btn-KichHoat').off('click').on('click', function (e) {
            e.preventDefault();
           
            var btn = $(this);
            var id = btn.data("id");
            console.log(id);
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                type: "POST",
                dataType: "json",
                data: { id: id },
                success:function(data)
                {
                    console.log(data.status);
                    if (data.status == true)
                        btn.text("Kích hoạt");
                    else btn.text("Khóa");
                },
                error: function () {

                }

            });

        });
    }

};

user.init();

