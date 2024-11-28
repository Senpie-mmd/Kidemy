document.addEventListener("DOMContentLoaded", function () {
    hideCustomizer();
});

const hideCustomizer = () => {
    const customizer = document.getElementById("template-customizer");
    if (customizer != null) {
        customizer.style.display = "none";
    } else {
        setTimeout(hideCustomizer, 1);
    }
}

function FillPageId(id) {
    $("#Page").val(id);
    $("#filter-search").submit();
}
jQuery(document).ready(function () {
    "use strict";




    $('.gallery-main').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        fade: true,
        asNavFor: '.left-slider-image'
    });

    $('.left-slider-image').slick({
        slidesToShow: 4,
        slidesToScroll: 1,
        asNavFor: '.gallery-main',
        dots: false,
        focusOnSelect: true,
        vertical: true,
        center: true,
        responsive: [{
            breakpoint: 1400,
            settings: {
                slidesToShow: 4,
                vertical: true,
            }
        },
        {
            breakpoint: 1200,
            settings: {
                slidesToShow: 4,
                vertical: true,
            }
        },
        {
            breakpoint: 992,
            settings: {
                slidesToShow: 4,
                vertical: false,
            }
        },
        {
            breakpoint: 768,
            settings: {
                slidesToShow: 3,
                vertical: false,
            }
        }, {
            breakpoint: 576,
            settings: {
                slidesToShow: 3,
                vertical: false,
            }
        }, {
            breakpoint: 430,
            settings: {
                slidesToShow: 3,
                vertical: false,
            }
        },
        ]
    });


    new WOW().init();
});


function format(input) {
    var nStr = input.value + '';
    nStr = nStr.replace(/\,/g, "");
    var x = nStr.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    input.value = x1 + x2;
}

let inputPrice = document.getElementById("digitPrice");

inputPrice?.addEventListener("keypress", () => format(inputPrice));

async function confirmBankAccountCard(url) {
    Swal.fire({
        html: `<b>${await jsLocalizer("ConfirmBankAccoutnCardMessage")}</b>`,
        showDenyButton: true,
        confirmButtonText: await jsLocalizer("Yes"),
        denyButtonText: await jsLocalizer("No")
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = url;
        }
    });
}

function showRejectModal(url) {
    $.ajax({
        type: 'get',
        url: url,
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: async function (response) {
            close_waiting();
            $("#LargeModalTitle").html(await jsLocalizer('ViewTitle.Admin.BankCardNotApproved'));
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal('show');
        },
        error: async function (response) {
            close_waiting();
            Swal.fire({
                icon: 'error',
                title: await jsLocalizer("Error.e"),
                confirmButtonText: await jsLocalizer("Ok"),
            });
        }
    });
}

// withdraw request
function rejectWithdrawRequest(id) {
    document.getElementById("toRejectWithdrawRequestId").value = id;

    $("#rejectModal").modal("show");
}

function acceptWithdrawRequest(id) {
    document.getElementById("toAcceptWithdrawRequestId").value = id;

    $("#acceptModal").modal("show");
}

function showBankAccountCard(id) {
    $.ajax({
        type: 'get',
        url: `/admin/getbankaccountcard/${id}`,
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: async function (response) {
            close_waiting();
            $("#LargeModalTitle").html(await jsLocalizer('DestinationBankAccountCard'));
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal('show');
        },
        error: async function (response) {
            close_waiting();
            Swal.fire({
                icon: 'error',
                title: await jsLocalizer("Error.e"),
                confirmButtonText: await jsLocalizer("Ok"),
            });
        }
    });
}

function showRejectModal(url) {
    $.ajax({
        type: 'get',
        url: url,
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: async function (response) {
            close_waiting();
            $("#LargeModalTitle").html(await jsLocalizer('ViewTitle.Admin.BankCardNotApproved'));
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal('show');
        },
        error: async function (response) {
            close_waiting();
            Swal.fire({
                icon: 'error',
                title: await jsLocalizer("Error.e"),
                confirmButtonText: await jsLocalizer("Ok"),
            });
        }
    });
}

async function confirmPublish(element) {
    console.log(element);
    var publishUrl = element.dataset.publishurl;
    var id = element.dataset.id;
    var courseid = element.dataset.courseid;
    Swal.fire({
        title: await jsLocalizer("AreYouSure?"),
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: await jsLocalizer("Yes"),
        cancelButtonText: await jsLocalizer("No"),
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'post',
                url: publishUrl,
                data: {
                    id: id,
                    courseId: courseid
                },
                beforeSend: function () {
                    open_waiting();
                },
                success: async function (response) {
                    close_waiting(); 
                    if (response.isSuccess) {
                        Swal.fire({
                            icon: 'success',
                            title: response.message,
                            confirmButtonText: await jsLocalizer("Ok"),
                        });
                        setTimeout(2000 ,location.reload()); 
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: response.Message,
                            confirmButtonText: await jsLocalizer("Ok"),
                        });
                    }
                },
                error: async function (response) {
                    close_waiting();
                    Swal.fire({
                        icon: 'error',
                        title: await jsLocalizer("Error.e"),
                        confirmButtonText: await jsLocalizer("Ok"),
                    });
                }
            });
        }
    });
}
