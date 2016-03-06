var contact = {
    init: function()
    {
        contact.regEvent();
    },

    regEvent:function()
    {
        $('#btnSend').off('click').on('click', function (e) {
            e.preventDefault();

            var name = $('#name').val();
            var phone = $('#phone').val();
            var email = $('#email').val();
            var address = $('#address').val();
            var content = $('#content').val();

            $.ajax({
                url: '/Contact/InsertFeedback',
                type: 'POST',
                dataType: 'json',
                data: {
                    name: name,
                    phone: phone,
                    email: email,
                    address: address,
                    content: content
                },
                success: function (data)
                {
                    
                    console.log(data);
                    if(data.Status == true)
                    {
                        console.log('123');
                        $("#alertFeedBack").removeClass("hide").addClass("alert-success");
              
                        $("#alertFeedBack").text("Gửi phản hồi thành công");
                        setTimeout(function () {
                            //$("#alertFeedBack").hide();
                            $("#alertFeedBack").removeClass("alert-success").addClass("hide");
                        }, 500);
                        contact.clearForm();
                        
                    }
                    else
                    {
                        $("#alertFeedBack").removeClass("hide").addClass("alert-danger");
        
                        $("#alertFeedBack").text("Gửi phản hồi thất bại");
                        setTimeout(function () {
                            $("#alertFeedBack").hide();
                        }, 500);
                        $("#alertFeedBack").removeClass("alert-danger").addClass("hide");
                    }
                },
                error: function () {
                    console.log('loi');
                }
            });

        });
    },

    clearForm: function()
    {
        $('#name').val('');
        $('#phone').val('');
        $('#email').val('');
        $('#address').val('');
        $('#content').val('');
    }

};
contact.init();
