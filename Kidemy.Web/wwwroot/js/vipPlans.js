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

            document.querySelector("#vIPMembers-form input[name='PlanId']").value = selectedPlan.dataset.planId;
            document.querySelector("#vIPMembers-form #InvoiceTotalPriceId").innerHTML = selectedPlan.value;
            document.querySelector("#vIPMembers-form #InvoicePayablePriceId").innerHTML = selectedPlan.value;
            document.getElementById('UseFromWalletBalnaceDivId').style.display = '';

            const useFromWalletBalnace = document.getElementById('UseFromWalletBalnace');

            if (useFromWalletBalnace.checked) {
                const amountDifference = parseInt(invoiceTotalPrice.innerHTML.toString().replaceAll(',', '').replaceAll('٬', '')) - walletBalance;

                if (amountDifference <= 0) {
                    document.querySelector("#vIPMembers-form #InvoicePayablePriceId").innerHTML = 0;
                }
                else {
                    document.querySelector("#vIPMembers-form #InvoicePayablePriceId").innerHTML = addCommas(amountDifference) ;
                }

            }

        })

    })

    const invoiceTotalPrice = document.getElementById('InvoiceTotalPriceId');

    const useFromWalletBalnace = document.getElementById('UseFromWalletBalnace');

    useFromWalletBalnace.addEventListener("change", () => {
        if (!useFromWalletBalnace.checked) {
            const selectedPlan = document.querySelector('input[name="plan"]:checked');
            document.querySelector("#vIPMembers-form #InvoicePayablePriceId").innerHTML = selectedPlan.value;

        }
        else {

            const amountDifference = parseInt(invoiceTotalPrice.innerHTML.toString().replaceAll(',', '').replaceAll('٬', '')) - walletBalance;

            if (amountDifference <= 0) {
                document.querySelector("#vIPMembers-form #InvoicePayablePriceId").innerHTML = 0;
            }
            else {
                document.querySelector("#vIPMembers-form #InvoicePayablePriceId").innerHTML = addCommas(amountDifference);
            }

        }
    });

    var vIPMembersForm = document.getElementById("vIPMembers-form");
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

    setTimeout(() => {
        document.querySelector('input[name="plan"]:checked')?.dispatchEvent(new Event('change'))

    }, 500)

})
