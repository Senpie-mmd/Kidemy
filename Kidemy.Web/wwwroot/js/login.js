// login required
window.addEventListener("load", async () => {
    if (window.location.href.toLowerCase().includes("returnurl")) {
        showLoginModal();
    }
});

function showLoginModal() {
    document.querySelector(`button[data-bs-target="#loginmodal1"]`)?.click();
}

window.addEventListener("load", () => {
    /* modal wizard */

    // toggle login method
    $(document).ready(function () {
        $(".form-static").hide();
        //using jQuery code
        $('#logwps').on('change', function (e) {
            if (e.currentTarget.checked) {
                $(".form-static").show();
            } else {
                $(".form-static").hide();
            }
        });
    });

    // back to modal 1
    $("#loginmodal2").find('.btn-prev').click(function () {
        $("#loginmodal2").modal('hide');
        $("#loginmodal1").modal('show');
        clearInterval(againSendCodeInterval);
        $('.donesend').removeClass("show");
        $('.sendag').removeClass("show");
        $('.timerbox').removeClass("hide");
    });


    /*digits*/
    $("input").bind("input", function () {
        var $this = $(this);
        setTimeout(function () {
            if ($this.val().length >= parseInt($this.attr("maxlength"), 10))
                $this.next("input").focus();
        }, 0);
    });

    // submit login or register form
    document.querySelector("#login-register-form").addEventListener("submit", (event) => {
        event.preventDefault();
        event.stopPropagation();

        var submitBtn = event.target.querySelector("button[type='submit']");
        btnToWaiting(submitBtn);

        clearInterval(againSendCodeInterval);
        startSendAgainCodeTimer();

        var formData = new FormData(event.target);

        var form = event.currentTarget;
        var formAction = form.action;
        var formMethod = form.method;

        var request = new XMLHttpRequest();

        request.onreadystatechange = async () => {
            if (request.readyState === 4) {

                waitingToSubmitBtn(submitBtn);

                if (request.status === 200) {

                    var result = JSON.parse(request.response);

                    if (result.isSuccess) {

                        if (result.requiredCodeConfirmation) {
                            $("#loginmodal1").modal('hide');
                            $("#loginmodal2").modal('show');
                            document.getElementById("d-mobile").innerHTML = formData.get("Mobile");
                        }
                        else if (result.requiredRedirectTo !== null) {
                            window.location.href = result.requiredRedirectTo;
                        }

                    }
                    else {


                        var title = "خطا";

                        var btnTitle = "باشه";

                        Swal.fire({
                            icon: 'error',
                            title: title,
                            text: result.message,
                            confirmButtonText: btnTitle,
                        });
                    }
                }
                else {

                    var title = "خطا";

                    var btnTitle = "باشه";

                    Swal.fire({
                        icon: 'error',
                        title: title,
                        text: result.message,
                        confirmButtonText: btnTitle,
                    });
                }
            }
        };

        request.open(formMethod, formAction, true);
        request.send(formData);
    });


    document.querySelectorAll("#login-confirmationCode-form .digits input").forEach(input =>
        input.addEventListener("keyup", () => {
            var allHaveValue = true;
            document.querySelectorAll("#login-confirmationCode-form .digits input").forEach(currentInput => {
                if (currentInput.value == "") {
                    allHaveValue = false;
                }
            })
            if (allHaveValue) {
                var evt = new SubmitEvent('submit',
                    { 'view': window, 'bubbles': true, 'cancelable': true });

                document.querySelector("#login-confirmationCode-form").dispatchEvent(evt);
            }
            
        }))


        /*send code timer*/
        var againSendCodeInterval = null;
        function startSendAgainCodeTimer() {
            var timer = 120;
            $('#timer b').text(timer);
            $('.timerbox').removeClass("hide");

            againSendCodeInterval = setInterval(function () {
                timer--;
                var clickedOnSendAgainButton = true; /*$('.donesend').hasClass("show");*/

                $('#timer b').text(timer);
                if (timer === 0) {
                    clearInterval(againSendCodeInterval);
                    $('.sendag').addClass("show");
                    $('.timerbox').addClass("hide");
                    //if (!clickedOnSendAgainButton) {
                    //    $('.sendag').addClass("show");
                    //}
                }
            }, 1000);
        }


        $('.sendag').on('click', function (e) {
            document.querySelector("#login-register-form button[type='submit']").click();
            //$('.donesend').addClass("show");
            $('.sendag').removeClass("show");
        });

        // submit code
        document.querySelector("#login-confirmationCode-form").addEventListener("submit", (event) => {
            event.preventDefault();
            event.stopPropagation();

            var formData = new FormData(event.target);

            var submitBtn = event.target.querySelector("button[type='submit']");
            btnToWaiting(submitBtn);

            formData.append("Mobile", document.querySelector("#login-register-form #Mobile").value);
            formData.append("Code", Array.from(event.currentTarget.querySelectorAll("input[id^='digit']")).map(input => input.value).join(""));
            formData.append("Code", Array.from(event.currentTarget.querySelectorAll("input[id^='digit']")).map(input => input.value).join(""));


            var form = event.currentTarget;
            var formAction = form.action;
            var formMethod = form.method;

            var request = new XMLHttpRequest();

            request.onreadystatechange = async () => {
                if (request.readyState === 4) {

                    waitingToSubmitBtn(submitBtn);

                    if (request.status === 200) {

                        var result = JSON.parse(request.response);

                        if (result.isSuccess) {
                            window.location.href = result.requiredRedirectTo;
                        }
                        else {
                            var title = "خطا";

                            var btnTitle = "باشه";

                            Swal.fire({
                                icon: 'error',
                                title: title,
                                text: result.message,
                                confirmButtonText: btnTitle,
                            });
                        }
                    }
                    else {

                        var title = "خطا";

                        var btnTitle = "باشه";

                        Swal.fire({
                            icon: 'error',
                            title: title,
                            text: result.message,
                            confirmButtonText: btnTitle,
                        });
                    }
                }
            };

            request.open(formMethod, formAction, true);
            request.send(formData);
        });
    });
