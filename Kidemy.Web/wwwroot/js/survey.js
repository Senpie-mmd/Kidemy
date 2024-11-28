window.addEventListener("load", () => {
    // Get the form element
    const form = document.getElementById('survey-form');
    const surveySubmitBtn = form.querySelector("button[type='submit']");

    // Function to validate the form
    function validateForm() {
        let isValid = true;

        // Validate text inputs
        const textInputs = form.querySelectorAll('input[type="text"]');
        textInputs.forEach(input => {
            if (input.value.trim() === '') {
                isValid = false;
                input.classList.add('error');
            } else {
                input.classList.remove('error');
            }
        });

        // Validate radio inputs
        const radioInputs = form.querySelectorAll('input[type="radio"]');
        const radioGroupNames = new Set();
        radioInputs.forEach(input => {
            const groupName = input.getAttribute('name');
            radioGroupNames.add(groupName);
        });
        radioGroupNames.forEach(groupName => {
            const selectedRadio = form.querySelector(`input[type="radio"][name="${groupName}"]:checked`);
            if (!selectedRadio) {
                isValid = false;
                const radioGroup = form.querySelector(`input[type="radio"][name="${groupName}"]`);
                radioGroup.classList.add('error');
            } else {
                const radioGroup = form.querySelector(`input[type="radio"][name="${groupName}"]`);
                radioGroup.classList.remove('error');
            }
        });

        // Validate checkbox inputs
        const checkboxInputs = form.querySelectorAll('input[type="checkbox"]');
        const checkboxGroupNames = new Set();
        checkboxInputs.forEach(input => {
            const groupName = input.getAttribute('name');
            checkboxGroupNames.add(groupName);
        });
        checkboxGroupNames.forEach(groupName => {
            const checkedCheckboxes = form.querySelectorAll(`input[type="checkbox"][name="${groupName}"]:checked`);
            if (checkedCheckboxes.length === 0) {
                isValid = false;
                const checkboxGroup = form.querySelectorAll(`input[type="checkbox"][name="${groupName}"]`);
                checkboxGroup.forEach(checkbox => checkbox.classList.add('error'));
            } else {
                const checkboxGroup = form.querySelectorAll(`input[type="checkbox"][name="${groupName}"]`);
                checkboxGroup.forEach(checkbox => checkbox.classList.remove('error'));
            }
        });

        return isValid;
    }

    // Add form submit event listener
    form.addEventListener('submit', async (event) => {
        event.preventDefault(); // Prevent form submission

        btnToWaiting(surveySubmitBtn);

        let isValid = validateForm();

        if (isValid) {
            // Form is valid, you can perform additional actions, such as submitting the form
            var responseText = await fetch(form.action, { method: "POST", body: new FormData(form) });
            if (responseText.ok) {
                var result = await responseText.json();
                if (result.isSuccess == true) {
                    return window.location.href = result.data;
                }
                else {
                    showError(result.message)
                }
            }
            else {
                console.log(responseText);
                showError(await jsLocalizer("ProcessFailed"));
            }
        } else {
            // Form is not valid, display an error message or take appropriate action
            console.log('Please fill in all required fields.');

            showError(await jsLocalizer("PleaseFillAllRequiredFields"));

            const formInputs = form.querySelectorAll('input');
            formInputs.forEach(input => {
                input.onchange = validateForm;
            });
        }

        waitingToBtn(surveySubmitBtn);

    });
})