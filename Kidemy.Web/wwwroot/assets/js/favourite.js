async function favourite(url) {
    var formData = {};
    addAntiforgeryToken(formData);
    debugger;

    $.ajax({
        type: 'Get',
        url: url,
        data: formData,
        success: function (response) {
            debugger;

            if (response.isSuccess == true) {

                var courseId = response.data.courseId;
                var isfavourite = response.data.isfavourite;

                if (isfavourite != false) {
                    var favouriteIcon = document.querySelectorAll(`#like-${courseId}`);

                    favouriteIcon.forEach(heart => {

                        heart.className = "fas fa-heart";
                        heart.style.color = 'red';

                    })
                }

                else {

                    var favouriteIcon = document.querySelectorAll(`#like-${courseId}`);

                    favouriteIcon.forEach(heart => {

                        heart.className = "far fa-solid fa-heart";
                        heart.style.color = '#383f45';

                    })
                
                }
            }
            else {
                window.location.reload();
            }
        }
    })
}

