window.addEventListener("load", () => {
    var titleInput = document.getElementById("Title");
    var nameInput = document.getElementById("Name");
    var slugInput = document.getElementById("slug");

    if (slugInput && titleInput) {
        var slugIsSameTitle = titleInput.value.replaceAll(" ", "-") == slugInput.value;

        titleInput.addEventListener("change", (e) => {
            console.log(slugIsSameTitle);
            if (slugIsSameTitle) {
                slugInput.value = titleInput.value.replaceAll(" ", "-");
            }
        });

        slugInput.addEventListener("change", () => {
            slugIsSameTitle = titleInput.value.replaceAll(" ", "-") == slugInput.value;
        });
    }
    else if (slugInput && nameInput) {
        var slugIsSameTitle = nameInput.value.replaceAll(" ", "-") == slugInput.value;

        nameInput.addEventListener("change", (e) => {
            if (slugIsSameTitle) {
                slugInput.value = nameInput.value.replaceAll(" ", "-");
            }
        });

        slugInput.addEventListener("change", () => {
            slugIsSameTitle = nameInput.value.replaceAll(" ", "-") == slugInput.value;
        });
    }


    slugInput.addEventListener("change", () => {
        slugInput.value = slugInput.value.replaceAll(" ", "-");
    });
});