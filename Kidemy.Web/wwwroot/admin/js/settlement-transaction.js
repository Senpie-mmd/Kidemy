
    function openModal(response) {
        $("#SettlementTransactionBody").html(response.responseText);
    $("#editSettlementTransaction").data('validator', null);
    $.validator.unobtrusive.parse($("form"));
    $(".persianDate").pDatepicker({
        observer: true,
    format: 'YYYY/MM/DD',
    initialValue: false,
    altField: 'observer-example-alt'
            });

    $(".persianDateTime").pDatepicker({
        observer: true,
    format: 'YYYY/MM/DD HH:mm',
    altField: 'observer-example-alt',
    initialValue: false,
    timePicker: {
        enabled: true,
                },
    calenderType: "gregorian",
    calendar: {
        type: "gregorian", // allows only the Persian calendar type
    locale: "en" // sets the language/locale to English
                }
            });

    if (languageIsEn) {
                var TimeinputsWithValues = [];

    Array.from(document.querySelectorAll(".persianDate")).forEach(function (item) {
        TimeinputsWithValues.push({ input: item, value: item.value });
                });

    Array.from(document.querySelectorAll(".persianDateTime")).forEach(function (item) {
        TimeinputsWithValues.push({ input: item, value: item.value });
                });

    Array.from(document.querySelectorAll(".datepicker-container")).forEach(function (item) {
        item.querySelector(".pwt-btn-today").click();
    item.querySelector(".pwt-btn-calendar").click();
                });

    Array.from(TimeinputsWithValues).forEach(function (item) {
        $(`#${item.input.id}`).val(item.value);
                });
            }
    else {
                var TimeinputsWithValues = [];

    Array.from(document.querySelectorAll(".persianDate")).forEach(function (item) {
        TimeinputsWithValues.push({ input: item, value: item.value });
                });

    Array.from(document.querySelectorAll(".persianDateTime")).forEach(function (item) {
        TimeinputsWithValues.push({ input: item, value: item.value });
                });

    Array.from(document.querySelectorAll(".datepicker-container")).forEach(function (item) {
        item.querySelector(".pwt-btn-today").click();
                });

    Array.from(TimeinputsWithValues).forEach(function (item) {
        $(`#${item.input.id}`).val(item.value);
                });
            }

    $("#AddSettlementTransactionModal").modal("show");
}
