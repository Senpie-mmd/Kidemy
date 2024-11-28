
function UserFeedback(url) {

    var formData = {};
    addAntiforgeryToken(formData);

    $.ajax({
        url: url,
        type: 'GET',
        data: formData,
        success: function (result) {

            var feedback = document.getElementById("feedback");
            if (feedback) {
                feedback.style.display = 'none';
            }
            var responseObject = JSON.parse(result);
            Swal.fire({
                icon: 'success',
                title: responseObject.message,
                confirmButtonText: 'باشه'
            });
        },
        dataType: 'html'


    });
};
