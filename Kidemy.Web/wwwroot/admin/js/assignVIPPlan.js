window.addEventListener("load", () => {

    document.querySelectorAll("tr[for*='plan-']").forEach(plan => {
        plan.addEventListener('click', () => {
            document.getElementById(plan.getAttribute('for')).click()

        })
    })

    const radioInputs = document.querySelectorAll('input[name="plan"]');

    radioInputs.forEach(radio => {
        radio.addEventListener("change", () => {
            const selectedPlan = document.querySelector('input[name="plan"]:checked');

            document.querySelector("#JoinToVIPMembersForm input[name='PlanId']").value = selectedPlan.dataset.planId;


        })

    })

    var vIPMembersForm = document.getElementById("JoinToVIPMembersForm");
    vIPMembersForm.addEventListener("submit", async (e) => {
        e.preventDefault();
        e.stopPropagation();

        const selectedPlan = document.querySelector('input[name="plan"]:checked');

        if (selectedPlan == null) {
            showError(await jsLocalizer("PleaseSelectOneItem"))
            return;
        }

        e.target.submit();

    });


})