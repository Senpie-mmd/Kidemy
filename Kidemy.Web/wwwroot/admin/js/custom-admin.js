//#region Waiting

function open_waiting(selector = 'body') {
    $.blockUI({
        message: '<div class="spinner-border text-white" role="status"></div>',

        css: {
            backgroundColor: 'transparent',
            border: '0'
        },
        overlayCSS: {
            opacity: 0.5
        }
    });
}

function close_waiting(selector = 'body') {
    $.unblockUI();
}

//#endregion

async function DeleteConfirm(url) {
    Swal.fire({
        html: `<b>${await jsLocalizer("AreYouSureToDelete")}</b>`,
        showDenyButton: true,
        confirmButtonText: await jsLocalizer("Yes"),
        denyButtonText: await jsLocalizer("No")
    }).then((result) => {
        if (result.isConfirmed) {
            var formData = {};
            addAntiforgeryToken(formData);

            $.ajax({
                type: 'post',
                url: url,
                data: formData,
                success: async function (response) {
                    window.location.reload();
                },
                error: async function (response) {
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

function kamaDatePickerCustom(selectorId) {
    kamaDatepicker(selectorId, {
        twodigit: true,
        closeAfterSelect: true,
        forceFarsiDigits: true,
        markToday: true,
        markHolidays: true,
        highlightSelectedDay: true,
        sync: true,
        gotoToday: true,
        buttonsColor: "#6c757d",
        nextButtonIcon: "fa-solid fa-chevron-right",
        previousButtonIcon: "fa-solid fa-chevron-left"
    });
}

//#region change order status

/*const changeOrderStatus = (url, status) => {

    $.ajax({
        url: url,
        data: { orderId: status },
    });
}
*/
//#endregion

function showOrderItem(url) {
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
            $("#LargeModalTitle").html(await jsLocalizer('ViewTitle.Admin.OrderItems'));
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

function openInNewTab(event) {
    event.preventDefault();
    event.stopPropagation();

    if (event.target.classList.contains('file-upload')) {
        selectedFileInput = document.getElementById(event.target.parentElement.getAttribute("for"));
    }

    var url = event.currentTarget.getAttribute("href"); // Get the URL from the href attribute

    //if (selectedFileInput != null) {
    //    url += `?targetId="${selectedFileInput.id}"`
    //}
    const windowFeatures = 'width=1200,height=600,modal=yes';
    window.open(url, '_blank', windowFeatures); // Open the URL in a new tab/window
}



var toman = '';

jsLocalizer("Toman").then(res => toman = res);
function showConvertedPrice() {
    let priceInput = document.querySelector("#Price");

    priceInput.parentElement
        .querySelector("span")
        .innerHTML = convertToToman(priceInput.value) + ' ' + toman;
}

function convertToToman(value) {
    value = convertToEnglish(value.replaceAll(",", ""));
    value = parseInt(parseFloat(value) * usdRate);
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(value)) {
        value = value.toString().replace(rgx, '$1' + ',' + '$2');
    }

    value = value.toString().replace("NaN", "0")

    return value;
}


function fillImageSrc(element) {
    if (element) {
        document.getElementById("image-name").value = element.getAttribute("src");
    }
}

//create file and assign them to input with type file
function fileCreator(imgTagId, fileInputTagId, imageNamehiddenInputId) {
    var imagesrc = document.getElementById(imageNamehiddenInputId).value;
    if (imagesrc !== '' && imagesrc !== null) {

        var imgTag = document.getElementById(imgTagId);

        if (imgTag) {
            imgTag.src = imagesrc;
        }

        var inputFile = document.getElementById(fileInputTagId);

        if (inputFile) {

            var contentType = imagesrc.split(';')[0].split(':')[1];
            var fileExtention = imagesrc.split(';')[0].split('/')[1];

            if (contentType.includes('svg')) {
                fileExtention = 'svg';
            }
            if (contentType.includes('vnd.microsoft.icon')) {
                fileExtention = 'ico';
            }

            fetch(imagesrc)
                .then(response => response.blob())
                .then(blob => {

                    const file = new File([blob], `file.${fileExtention}`, { type: contentType });

                    const fileList = new DataTransfer();
                    fileList.items.add(file);

                    inputFile.files = fileList.files;

                })
                .catch(error => {
                    console.error('Error fetching image:', error);
                });

        }
    }
}


//function hideDatePicker() {

//    document.querySelectorAll("div[class='datepicker-container']").forEach((e) => {
//        e.classList = 'datepicker-container pwt-hide';
//    });
//}

//window.addEventListener('load', () => {
//    //var tdElements = document.querySelectorAll("td[data-unix]");

//    //tdElements.forEach((element) => {
//    //    element.addEventListener('click', hideDatePicker());
//    //});
//    //document.getElementById("BirthDate").addEventListener('input', console.log('changed'))

////    document.getElementById("BirthDate").addEventListener('change', hideDatePicker());
//});


// back button
function backUrl() {
    var stringifiedRequestsHistory = getCookie("requestsHistory");
    var requestsHistory = JSON.parse(stringifiedRequestsHistory);

    // Remove the current URL from the requestsHistory list
    requestsHistory = requestsHistory.filter(function (url) {
        return url !== window.location.pathname && url !== previousUrl;
    });

    // Set the updated requestsHistory list in the cookie
    document.cookie = "requestsHistory=" + JSON.stringify(requestsHistory) + "; path=/Admin;";

    // Redirect to the previous URL
    window.location.href = previousUrl;
}

var previousUrl = '';
if (window.location.pathname !== null && window.location.pathname !== "") {
    var stringifiedRequestsHistory = getCookie("requestsHistory");
    var requestsHistory = [];
    try {
        var requestsHistory = JSON.parse(stringifiedRequestsHistory);
        previousUrl = requestsHistory[requestsHistory.length - 1];
        if (requestsHistory[requestsHistory.length - 1] === window.location.pathname) {
            previousUrl = requestsHistory[requestsHistory.length - 2];
        }
    }
    catch {
        console.log('has error')
    }
    if (requestsHistory[requestsHistory.length - 1] !== window.location.pathname) {
        requestsHistory.push(window.location.pathname);
        document.cookie = "requestsHistory=" + JSON.stringify(requestsHistory) + "; path=/Admin;";
    }
}

function getCookie(name) {
    var cookieName = name + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var cookieArray = decodedCookie.split(";");

    for (var i = 0; i < cookieArray.length; i++) {
        var cookieValue = cookieArray[i];
        while (cookieValue.charAt(0) === " ") {
            cookieValue = cookieValue.substring(1);
        }

        if (cookieValue.indexOf(cookieName) === 0) {
            return cookieValue.substring(cookieName.length, cookieValue.length);
        }
    }

    return "";
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

// modals
async function showModal(url, title) {
    open_waiting();
    var response = await fetch(url, { method: "GET" });
    close_waiting();

    if (response.ok) {
        document.querySelector("#LargeModalBody").innerHTML = await response.text();
        document.querySelector("#LargeModalTitle").innerHTML = title;

        $("#LargeModal").modal("show");

    }
    else {
        Swal.fire({
            icon: 'error',
            title: await jsLocalizer("Error.e"),
            html: await jsLocalizer("Error")
        });
    }
}