function FillPageId(page) {
    $("#Page").val(page);
    $("#filter-search").submit();
}

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

    input.value = convertToEnglish(input.value);
}

function onlyNumbers(event) {

    key = event.key;

    if (
        !/^[0-9\u06F0-\u06F9\b]$/.test(key) && // check for standard Arabic and Persian numerals
        key !== "Delete" &&
        key !== "Backspace" &&
        key !== "ArrowLeft" &&
        key !== "ArrowRight" &&
        key !== "ArrowUp" &&
        key !== "ArrowDown" &&
        key !== "."
    ) {
        event.preventDefault();
    }
}


function onlyValidNumbers(element) {
    element.value = Math.abs(element.value);

    let min = element.getAttribute("min");
    if (min) {
        min = parseInt(min);

        if (element.value < min) {
            element.value = min;
        }
    }
}

function convertToEnglish(value) {

    return value
        .replaceAll("۰", "0")
        .replaceAll("۱", "1")
        .replaceAll("۲", "2")
        .replaceAll("۳", "3")
        .replaceAll("۴", "4")
        .replaceAll("۵", "5")
        .replaceAll("۶", "6")
        .replaceAll("۷", "7")
        .replaceAll("۸", "8")
        .replaceAll("۹", "9");
}


function addCommas(num) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

// digit price -- price -- number
function initCommon() {

    var digitInput = document.getElementById("digitPrice");
    if (digitInput) {
        digitInput.addEventListener("keyup", () => format(digitInput));
        digitInput.addEventListener("keydown", (event) => onlyNumbers(event));

    }

    var prices = Array.from(document.getElementsByClassName("price"));
    prices.forEach(input => {


        if (parseInt(input.value?.split("٫")[1]) === 0) {
            input.value = input.value.split("٫")[0];
        }

        input.value = input.value.replace("٫", ".")

        format(input);

        input.addEventListener("keyup", () => format(input));
        input.addEventListener("keydown", (event) => onlyNumbers(event));

        if (input.form) {
            input.form.addEventListener("submit", (event) => {
                //event.preventDefault();

                input.value = input.value.replaceAll(",", "");

                //event.target.submit();
            })
        }
    });

    var numbers = Array.from(document.getElementsByClassName("number"));
    numbers.forEach(input => {
        input.addEventListener("keydown", (event) => onlyNumbers(event));
        input.addEventListener("keyup", (event) => event.target.value = convertToEnglish(event.target.value));
    });

    // min=0
    document.querySelectorAll("input[min='0']").forEach(input => {
        input.addEventListener("change", (e) => {
            if (e.target.value < 0) {
                e.target.value = 0;
            }
        })

        if (input.value < 0) {
            input.value = 0;
        }
    })

    // min=1
    document.querySelectorAll("input[min='1']").forEach(input => {
        input.addEventListener("change", (e) => {
            if (e.target.value < 1) {
                e.target.value = 1;
            }
        })

        if (input.value < 1) {
            input.value = 1;
        }
    })
}
initCommon();

// tagify
$(function () {

    document.querySelectorAll("[tagify]").forEach(input => {
        new Tagify(input, {
            originalInputValueFormat: valuesArr => valuesArr.map(item => item.value).join(',')
        });
    });
})

var editors = $("[ckeditor]");
var editorSrc = languageIsEn ? "https://cdn.ckeditor.com/ckeditor5/37.1.0/classic/ckeditor.js" : "/common/ckeditor/build/ckeditor.js";
var editorScriptIsGot = false;
var editorsList = [];
var applyCkeditor = function (editors) {
    if (editorScriptIsGot == false) {
        $.getScript(editorSrc, invoke);
        editorScriptIsGot = true;
    }
    else {
        invoke();
    }
    function invoke() {
        $(editors).each(function (index, value) {
            ClassicEditor.create(value,
                {
                    toolbar: {
                        items: [
                            'heading',
                            '|',
                            'bold',
                            'italic',
                            'underline',
                            'blockQuote',
                            'link',
                            '|',
                            'fontColor',
                            'fontSize',
                            '|',
                            'alignment',
                            'numberedList',
                            'bulletedList',
                            'indent',
                            'outdent',
                            '|',
                            'imageUpload',
                            'insertTable',
                            '|',
                            'codeBlock',
                            'removeFormat',
                        ]
                    },
                    language: 'fa',
                    image: {
                        toolbar: [
                            'imageTextAlternative',
                            'imageStyle:full',
                            'imageStyle:side'
                        ]
                    },
                    table: {
                        contentToolbar: [
                            'tableColumn',
                            'tableRow',
                            'mergeTableCells',
                            'tableCellProperties',
                            'tableProperties'
                        ]
                    },
                    simpleUpload: {
                        uploadUrl: '/SaveCkeditorImage'
                    },
                    licenseKey: '',
                    autoGrow: true,
                    autoGrow_minHeight: '800px'
                })
                .then(editor => {
                    window.editor = editor;
                    editorsList.push(editor);
                })
                .catch(error => {
                    console.log(error);
                });
        });
    }
}


if (editors.length > 0) {

    applyCkeditor(editors);

    window.addEventListener('keyup', function (event) {
        if (event.keyCode == 46) {
            var imagesName = [];
            imagesName.push($("#SelectedImageName").val());

            $.ajax({
                url: '/DeleteCkeditorImage',
                type: 'POST',
                data: { imagesName: imagesName }
            });
        }
    });

    window.addEventListener('click', function (event) {
        if (event.target.tagName.toLowerCase() === 'img') {
            $("#SelectedImageName").val(event.target.getAttribute('src'));
        }
    });
}


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


//#region Paging Function
function FillPartialPageId(pageId, baseName) {
    var formId = GetFormIdForSearchModal(baseName);
    $("#PartialPageId").val(pageId);
    $(`#${formId}`).submit();
}

onImageInputChange();

//#endregion
function onImageInputChange() {
    let button = document.getElementById('uploadBtn');
    if (button != null) {
        button.addEventListener("click", function () {
            let file = document.getElementById("uploadImage");
            file.click();
        });
    }

    let button2 = document.getElementById('uploadBtn2');
    if (button2 != null) {
        button2.addEventListener("click", function () {
            let file = document.getElementById("uploadImage2");
            file.click();
        });
    }

    //#region Change Image 

    $("[ImageInput]").change(function () {
        var x = $(this).attr("ImageInput");
        var submitFormAfterUpload = $(this).attr("SubmitFormAfterUpload");

        if (submitFormAfterUpload !== null && submitFormAfterUpload !== undefined && submitFormAfterUpload !== "") {
            $(`#${submitFormAfterUpload}`).submit();
        } else {
            if (this.files && this.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $("[ImageFile=" + x + "]").attr('src', e.target.result);
                };
                reader.readAsDataURL(this.files[0]);
            }
        }
    });

    // #endregion

    //#region Change Images
    $("[ImageInput]").change(function () {
        if (typeof (FileReader) != "undefined") {
            var dvPreview = $("#dvPreview");
            dvPreview.html("");
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp|.svg)$/;
            $($(this)[0].files).each(function () {
                var file = $(this);
                if (regex.test(file[0].name.toLowerCase())) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var img = $("<img />");
                        img.attr("src", e.target.result);
                        img.attr("class", "w-100 rounded-2 mb-3");
                        dvPreview.append(img);
                    }
                    reader.readAsDataURL(file[0]);
                } else {
                    //alert(file[0].name + " is not a valid image file.");
                    dvPreview.html("");
                    return false;
                }
            });
        }
        else {
            alert("This browser does not support HTML5 FileReader.");
        }
    });
}


// #endregion

//#region another way to get excel report

function makeSheetRtl(workBook) {
    if (!workBook.Workbook) workBook.Workbook = {};
    if (!workBook.Workbook.Views) workBook.Workbook.Views = [];
    if (!workBook.Workbook.Views[0]) workBook.Workbook.Views[0] = {};
    workBook.Workbook.Views[0].RTL = true;
    return workBook;
}

function html_table_to_excel(type, tableId) {
    var data = document.getElementById(`${tableId}`);

    var file = XLSX.utils.table_to_book(data, { sheet: "sheet1" });

    let rtlFile = makeSheetRtl(file);

    XLSX.write(rtlFile, { bookType: type, bookSST: true, type: "base64" });

    XLSX.writeFile(rtlFile, "file." + type);
}


//#endregion


// resources
async function jsLocalizer(resourceName) {
    var response = await fetch(`/Home/GetResource?resourceName=${resourceName}`);
    return await response.text();
}
// end resources

function btnToWaiting(btn) {
    btn.setAttribute("prev-inner", btn.innerHTML);
    var type = btn.getAttribute("type");
    if (type !== null && type !== undefined && type !== "") {
        btn.setAttribute("prev-type", type);
    }
    else {
        btn.setAttribute("prev-type", "button");
    }
    btn.setAttribute("type", "button");
    btn.innerHTML = `<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>`;

    setTimeout(() => waitingToBtn(btn), 5000);
}

function waitingToBtn(btn) {
    btn.innerHTML = btn.getAttribute("prev-inner");
    btn.setAttribute("type", btn.getAttribute("prev-type"));
}

$(document).ready(function () {
    $(".persianDate").pDatepicker({
        observer: true,
        format: 'YYYY/MM/DD',
        initialValue: false,
        autoClose: true,
        todayHighlight: true,
        altField: 'observer-example-alt'
    });

    $(".persianDateTime").pDatepicker({
        observer: true,
        format: 'YYYY/MM/DD HH:mm',
        altField: 'observer-example-alt',
        autoClose: true,
        todayHighlight: true,
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

});

window.addEventListener('keyup', function (event) {
    if (event.keyCode == 46) {
        var imagesName = [];
        imagesName.push($("#SelectedImageName").val());

        $.ajax({
            url: '/DeleteCkeditorImage',
            type: 'POST',
            data: { imagesName: imagesName }
        });
    }
});

window.addEventListener('click', function (event) {
    if (event.target.tagName.toLowerCase() === 'img') {
        $("#SelectedImageName").val(event.target.getAttribute('src'));
    }
});

// loading-support
window.addEventListener("load", () => {
    document.querySelectorAll(".loading-support").forEach(button => {
        if (button.type == 'submit') {

            button.form.addEventListener("submit", () => {

                if (button.form.querySelector(".input-validation-error")) {
                    return;
                }

                btnToWaiting(button);

            });
        }
        else {
            button.addEventListener("click", () => btnToWaiting(button));
        }
    })
})
async function showError(message) {
    var ok = await jsLocalizer("Ok");
    Swal.fire({
        icon: 'error',
        title: message,
        confirmButtonText: ok
    });
}

async function showSuccess(message) {
    var ok = await jsLocalizer("Ok");
    Swal.fire({
        icon: 'success',
        title: message,
        confirmButtonText: ok
    });
}

function addAntiforgeryToken(data) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    data["__RequestVerificationToken"] = token;
}

function addAntiforgeryTokenToFormData(formData) {
    var token = $('input[name="__RequestVerificationToken"]').val();
    formData.append("__RequestVerificationToken", token);
}

try {
    $(".select2").select2()
} catch (e) {

}