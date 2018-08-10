$().ready(function () {
    $("#registerform").validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            Organisation: {
                required: true,
                minlength: 2
            },
            Password: {
                required: true,
                minlength: 5
            },
            ConfirmPassword: {
                required: true,
                minlength: 5,
                equalTo: "Password"
            }
        }
    });
});