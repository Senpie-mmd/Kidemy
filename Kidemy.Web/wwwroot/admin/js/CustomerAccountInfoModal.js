var currentStep = -1;

function ShowSearchCustomerAccountInfoModal(step) {
    statusChanged = false;
    $.ajax({
        url: `/Admin/Game/FilterCustomerAccountInfo`,
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: async function (response) {
            currentStep = step;
            close_waiting();
            $("#MediumModalTitle").html(await jsLocalizer("CustomerAccountInfoList"));
            $("#MediumModalBody").html(response);
            $("#MediumModal").modal("show");
        },
        error: async function () {
            close_waiting();
            showError("خطا", await jsLocalizer("Error"), "error");
        }
    });
    return false;
}

function FillPartialFilterCustomerAccountInfo(pageId) {
    $("#PartialFilterCustomerAccountInfoPageId").val(pageId);
    $(`#filterCustomerAccountInfo-Form`).submit();
}

function SelectCustomerAccountInfo(loginMethodId, title) {
    $(`#CustomerAccountInfo-Input-${currentStep}`).val(loginMethodId).trigger("change");
    $(`#CustomerAccountInfo-Display-${currentStep}`).val(title).trigger("change");

    $("#MediumModal").modal("hide");
}