let Title = document.getElementById("Title");
let Slug = document.getElementById("Slug");
let canChangeSlug = true;

Slug.addEventListener("focusout", () => {
    if (Title.value == Slug.value) {
        canChangeSlug = true;
    }
    else {
        canChangeSlug = false;
    }
});

Title.addEventListener("focusin", () => {
    if (Title.value == Slug.value) {
        canChangeSlug = true;
    }
    else {
        canChangeSlug = false;
    }
});

Title.addEventListener("change", () => {
    if (canChangeSlug == true) {
        Slug.value = Title.value.replaceAll(" ", "-");
    }
});