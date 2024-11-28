function onModalShow() {
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

    var questionForm = document.getElementById("survey-question-form");
    var questionTypeInput = questionForm.querySelector("#Type");
    var priorityInput = questionForm.querySelector("#Priority");
    var persianTitleInput = questionForm.querySelector("#Title");

    questionForm.querySelectorAll("[tagify]").forEach(input => {
        new Tagify(input, {
            originalInputValueFormat: valuesArr => valuesArr.map(item => item.value).join(',')
        });
    });

    questionTypeInput.addEventListener("change", function (e) {
        if (questionTypeInput.value == '0') {
            questionForm.querySelectorAll("[tagify]").forEach(input => input.parentElement.classList.add("d-none"))
        }
        else {
            questionForm.querySelectorAll("[tagify]").forEach(input => input.parentElement.classList.remove("d-none"))
        }
    });

    questionTypeInput.dispatchEvent(new Event("change"))

    questionForm.querySelectorAll("button[data-bs-target^='#navs-pills-top-']").forEach(button => {
        button.setAttribute("data-bs-target", button.getAttribute("data-bs-target") + "-2");
        button.setAttribute("aria-controls", button.getAttribute("aria-controls") + "-2");
    })

    questionForm.querySelectorAll("div[id^='navs-pills-top-']").forEach(div => {
        div.id = div.id + "-2";
    })

    questionForm.addEventListener("submit", async (e) => {
        e.preventDefault();

        if (persianTitleInput.value == '' || persianTitleInput.value == null || persianTitleInput.value == undefined) {
            showError(await jsLocalizer("PleaseFillQuestionTitle"))
            return;
        }

        if (priorityInput.value <= 0 || priorityInput.value > 2147483647) {
            showError(await jsLocalizer("PrioriyIsNotValid"))
            return;
        }

        if (questionTypeInput.value != '0') {
            var tagsCount = [];
            var optionsRequiredError = await jsLocalizer("PleaseFillOptions");

            var Optionsinput = questionForm.querySelector("[tagify][name='Options']");

            if (Optionsinput.value == '' || Optionsinput.value == null || Optionsinput.value == undefined) {
                showError(optionsRequiredError)
                return;
            }

            questionForm.querySelectorAll("[tagify]").forEach(input => {
                if (input.value != '' && input.value != null && input.value != undefined) {
                    tagsCount.push(input.value.split(",").length);
                }
            });

            if (tagsCount.filter(tc => tc == tagsCount[0]).length != tagsCount.length) {
                showError(await jsLocalizer("OptionsDoNotHaveEqualLength"))
                return;
            }
        }
        var data = new FormData(questionForm);

        var responseText = await fetch(questionForm.action, {
            method: "POST",
            body: data
        })

        if (responseText.ok) {
            var result = await responseText.json();

            if (result.isSuccess == true) {
                $("#LargeModal").modal("hide")
                refreshQuestions();
            }
            else {
                showError(result.message)
            }
        }
        else {
            console.log(responseText)
        }
    });
}

async function showSurveyQuestionModal(url, title) {

    open_waiting();
    var response = await fetch(url, { method: "GET" });
    close_waiting();

    if (response.ok) {
        document.querySelector("#LargeModalBody").innerHTML = await response.text();
        document.querySelector("#LargeModalTitle").innerHTML = title;

        onModalShow();

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

async function confirmDeleteAjax(url, itemId) {
    var swalResult = await Swal.fire({
        html: `<b>${await jsLocalizer("AreYouSureToDelete")}</b>`,
        showDenyButton: true,
        confirmButtonText: await jsLocalizer("Yes"),
        denyButtonText: await jsLocalizer("No")
    });

    if (swalResult.isConfirmed) {
        var data = new FormData();
        data.append("id", itemId);
        addAntiforgeryTokenToFormData(data);

        open_waiting();
        var response = await fetch(url, { method: "POST", body: data });
        close_waiting();

        if (response.ok) {
            var responseResult = await response.json();
            if (responseResult.isSuccess == true) {
                refreshQuestions();
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: await jsLocalizer("Error.OperationFailedError"),
                    confirmButtonText: await jsLocalizer("Ok")
                })
            }
        }
        else {
            Swal.fire({
                icon: 'error',
                title: await jsLocalizer("Error.e"),
                html: await jsLocalizer("Error")
            });
        }
    }
}