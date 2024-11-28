function ShowSearchUserModal(baseName, roleId = undefined) {
    $.ajax({
        url: "/Admin/Home/SearchUserModal",
        type: "post",
        data: {
            baseName: baseName,
            RoleId :roleId
        },
        beforeSend: function () {
        //    open_waiting();
        },
        success: async function (response) {
            close_waiting();

            //TODO
            let title = "لیست کاربران";
            $("#LargeModalTitle").html(title);
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal("show");
        },
        error: function () {
        //    close_waiting();
        }
    });
}


function SelectUser(userId, userName, baseName) {
    var inputId = GetInputIdForSearchModal(baseName);
    var displayId = GetDisplayIdForSearchModal(baseName);

    $(`#${inputId}`).val(userId).trigger("change");
    $(`#${displayId}`).val(userName).trigger("change");

    $("#LargeModal").modal("hide");
}

function GetInputIdForSearchModal(baseName) {
    return `${baseName}-Input`;
}

function GetDisplayIdForSearchModal(baseName) {
    return `${baseName}-Display`;
}

function GetFormIdForSearchModal(baseName) {
    return `${baseName}-Form`;
}

function FillPartialPageId(pageId, baseName) {
    var formId = GetFormIdForSearchModal(baseName);
    $("#PartialPageId").val(pageId);
    $(`#${formId}`).submit();
}

function ShowSearchUserForSendSMSModal(baseName) {
    $.ajax({
        url: "/Admin/Home/SearchUserModalForSendSMS",
        type: "post",
        data: {
            baseName: baseName
        },
        beforeSend: function () {
            open_waiting();

        },
        success: async function (response) {
            let result = getChildrenOfTagDivThatIncludeNumbers();
            close_waiting();
            let title = "لیست کاربران";
            $("#LargeModalTitle").html(title);
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal("show");

            //Checked inputs that have already been selected
            if (result.length > 0) {

                let numbers = getListOfAvailableNumbersAtdiv();

                for (let i = 0; i < result.length; i++) {

                    let valueInput = result[i].value;

                    if (numbers.includes(valueInput)) {
                        let input = document.querySelector(`input[value="${valueInput}"]`);
                        input.checked = true;
                    }
                }
            }
        },
        error: function () {
            close_waiting();
        }
    });
}

function ShowSearchUserForSendEmailModal(baseName) {
    $.ajax({
        url: "/Admin/Home/SearchUserModalForSendEmail",
        type: "post",
        data: {
            baseName: baseName
        },
        beforeSend: function () {
            open_waiting();

        },
        success: async function (response) {
            let result = getChildrenOfTagDivThatIncludeEmails();
            close_waiting();
            let title = "لیست کاربران";
            $("#LargeModalTitle").html(title);
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal("show");

            //Checked inputs that have already been selected
            if (result.length > 0) {

                let emails = getListOfAvailableEmailsAtdiv();

                for (let i = 0; i < result.length; i++) {

                    let valueInput = result[i].value;

                    if (emails.includes(valueInput)) {
                        let input = document.querySelector(`input[value="${valueInput}"]`);
                        input.checked = true;
                    }
                }
            }
        },
        error: function () {
            close_waiting();
        }
    });
}

function getChildrenOfTagDivThatIncludeNumbers() {
    let div = document.getElementById('containNumbers');
    let children = div.children;
    return children;
}

function getChildrenOfTagDivThatIncludeEmails() {
    let div = document.getElementById('containEmails');
    let children = div.children;
    return children;
}

function getListOfAvailableNumbersAtdiv() {

    let children = getChildrenOfTagDivThatIncludeNumbers();
    let numbers = new Array();
    if (children.length > 0) {

        for (let i = 0; i < children.length; i++) {

            let element = children[i];
            let value = element['value'];
            console.log(value);
            numbers.push(value);
        }
        return numbers;
    }
}

function getListOfAvailableEmailsAtdiv() {

    let children = getChildrenOfTagDivThatIncludeEmails();
    let emails = new Array();
    if (children.length > 0) {

        for (let i = 0; i < children.length; i++) {

            let element = children[i];
            let value = element['value'];
            emails.push(value);
        }
        return emails;
    }
}

function fillUserEmails() {

    let getCheckBoxInputs = document.querySelectorAll('input[type="checkbox"]');

    const emails = new Array();

    for (let i = 0; i < getCheckBoxInputs.length; i++) {

        if (getCheckBoxInputs[i].checked) {

            emails.push(getCheckBoxInputs[i].value);
        }
    }
    const filteredEmails = emails.filter(email => email !== 'on');

    let containEmails = document.getElementById('containEmails');

    //Delete Previous Children Of div Tag
    while (containEmails.firstChild) {
        containEmails.removeChild(containEmails.lastChild);
    }

    //

    //Create New Input That Include Selected Email
    for (let i = 0; i < filteredEmails.length; i++) {

        let newInput = document.createElement("input");
        newInput.type = 'hidden';
        newInput.name = 'Emails';
        newInput.value = `${filteredEmails[i]}`;

        containEmails.appendChild(newInput);
    }

    //Fill input user display  
    let displayInput = document.getElementById("User-Display");
    displayInput.value = filteredEmails.join(", ");

    $("#LargeModal").modal("hide");

}

function fillUsersNumber() {

    let getCheckBoxInputs = document.querySelectorAll('input[type="checkbox"]');

    let numbers = new Array();

    for (let i = 0; i < getCheckBoxInputs.length; i++) {

        if (getCheckBoxInputs[i].checked) {

            numbers.push(getCheckBoxInputs[i].value);
        }
    }

    let filteredNumbers = numbers.filter(numbers => numbers !== 'on');

    let containNumbers = document.getElementById('containNumbers');

    //Delete Previous Children Of div Tag
    while (containNumbers.firstChild) {
        containNumbers.removeChild(containNumbers.lastChild);
    }

    //

    //Create New Input That Include Selected Mobile
    for (let i = 0; i < filteredNumbers.length; i++) {

        let newInput = document.createElement("input");
        newInput.type = 'hidden';
        newInput.name = 'MobileNumbers';
        newInput.value = `${filteredNumbers[i]}`;

        containNumbers.appendChild(newInput);
    }

    //Fill input user display  
    let displayInput = document.getElementById("User-Display");
    displayInput.value = filteredNumbers.join(", ");

    $("#LargeModal").modal("hide");
}

function FillPartialPageId(pageId, baseName) {
    let formId = GetFormIdForSearchModal(baseName);
    $("#PartialPageId").val(pageId);
    $(`#${formId}`).submit();
}

function hideUserDisplayEmail(element) {

    let userdisplay = document.getElementById("user-display");
    let containEmails = document.getElementById("containEmails");
    let userDisplay = document.getElementById("User-Display");

    if (element.checked) {

        userdisplay.style.display = 'none';
        while (containEmails.firstChild) {
            containEmails.removeChild(containEmails.lastChild);
        }
        userDisplay.value = "";
    }
    else {
        userdisplay.style.display = "flex";
    }

}

function hideUserDisplayNumbers(element) {

    let userdisplay = document.getElementById("user-display");
    let containNumbers = document.getElementById("containNumbers");
    let userDisplay = document.getElementById("User-Display");

    if (element.checked) {

        userdisplay.style.display = 'none';

        while (containNumbers.firstChild) {
            containNumbers.removeChild(containNumbers.lastChild);
        }
        userDisplay.value = "";
    }
    else {
        userdisplay.style.display = "flex";
    }

}
