window.addEventListener("load", () => {
    var autoUseCheckBox = document.getElementById("AutoUse");
    var discountCodeInput = document.getElementById("Code");
    var isPercentageCheckBox = document.getElementById("IsPercentage");
    var discountValueInput = document.getElementById("Value");

    autoUseCheckBox.addEventListener("change", () => {
        autoUseCheckBoxChanged();
    })

    autoUseCheckBoxChanged();

    function autoUseCheckBoxChanged() {
        if (autoUseCheckBox.checked) {
            discountCodeInput.disabled = true;
            discountCodeInput.value = "";
        } else {
            discountCodeInput.disabled = false;
        }
    }

    isPercentageCheckBox.addEventListener("change", () => {
        if (isPercentageCheckBox.checked && parseInt(discountValueInput.value.replaceAll(",", "")) > 100) {
           discountValueInput.value = "100";
        }
    })

    discountValueInput.addEventListener("keyup", () => {
        if (isPercentageCheckBox.checked && parseInt(discountValueInput.value.replaceAll(",", "")) > 100) {
            discountValueInput.value = "100";
        }
    })
})