
function openBlockedAmountModal(response) {
    $("#BlockedAmountBody").html(response.responseText);
    $("#editBlockedAmount").data('validator', null);
    $.validator.unobtrusive.parse($("form"));
    initCommon();
    $("#AddBlockedAmountModal").modal("show");
}

