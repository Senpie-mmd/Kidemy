function ShowCreateDiscountLimitationModal(discountId) {
    statusChanged = false;
    $.ajax({
        url: `/Admin/Discount/CreateDiscountLimitation?discountId=${discountId}`,
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: async function (response) {
            close_waiting();
            $("#LargeModalTitle").html(await jsLocalizer("CreateDiscountLimitation"));
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal("show");
        },
        error: async function () {
            close_waiting();
            showError(await jsLocalizer("Error.e"), await jsLocalizer("Error"), "error");
        }
    });
    return false;
}

function ShowUpdateDiscountLimitationModal(id) {
    statusChanged = false;
    $.ajax({
        url: `/Admin/Discount/UpdateDiscountLimitation/${id}`,
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: async function (response) {
            close_waiting();
            $("#LargeModalTitle").html(await jsLocalizer("UpdateDiscountLimitation"));
            $("#LargeModalBody").html(response);
            $("#LargeModal").modal("show");
        },
        error: async function () {
            close_waiting();
            showError(await jsLocalizer("Error.e"), await jsLocalizer("Error"), "error");
        }
    });
    return false;
}

function disableSelectedUsersCheckbox() {
    selectedUserIds.forEach(userId => {
        document.querySelector(`#MediumModalBody table tr #user-${userId}`)?.remove();
    });
}

function ShowSearchUserModal() {
    $.ajax({
        url: "/Admin/Discount/FilterUsers",
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: function (response) {
            close_waiting();
            jsLocalizer("UsersList").then(resource => {
                $("#MediumModalTitle").html(resource);
                $("#MediumModalBody").html(response);

                disableSelectedUsersCheckbox();

                $("#MediumModal").modal("show");
            })
        },
        error: async function () {
            close_waiting();
            showError(await jsLocalizer("Error.e"), await jsLocalizer("Error"), "error");
        }
    });
}

function disableSelectedCoursesCheckbox() {
    selectedCourseIds.forEach(courseId => {
        document.querySelector(`#MediumModalBody table tr #course-${courseId}`)?.remove();
    });
}

function ShowSearchCourseModal() {
    $.ajax({
        url: "/Admin/Discount/FilterCourses",
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: function (response) {
            close_waiting();
            jsLocalizer("CoursesList").then(resource => {
                $("#MediumModalTitle").html(resource);
                $("#MediumModalBody").html(response);

                disableSelectedCoursesCheckbox();

                $("#MediumModal").modal("show");
            })
        },
        error: async function () {
            close_waiting();
            showError(await jsLocalizer("Error.e"), await jsLocalizer("Error"), "error");
        }
    });
}

function disableSelectedCategoriesCheckbox() {
    selectedCategoryIds.forEach(categoryId => {
        document.querySelector(`#MediumModalBody table tr #category-${categoryId}`)?.remove();
    });
}

function ShowSearchCategoryModal() {
    $.ajax({
        url: "/Admin/Discount/FilterCategories",
        type: "get",
        data: {
        },
        beforeSend: function () {
            open_waiting();
        },
        success: function (response) {
            close_waiting();
            jsLocalizer("CategoriesList").then(resource => {
                $("#MediumModalTitle").html(resource);
                $("#MediumModalBody").html(response);

                disableSelectedCategoriesCheckbox();

                $("#MediumModal").modal("show");
            })
        },
        error: async function () {
            close_waiting();
            showError(await jsLocalizer("Error.e"), await jsLocalizer("Error"), "error");
        }
    });
}

var selectedUserIds = [];
var selectedCourseIds = [];
var selectedCategoryIds = [];

function SelectUsers() {
    Array.from(document.querySelectorAll("input[name='userId']:checked"))
        .forEach(input => {
            selectedUserIds.push(input.value);
            $("#users tbody").append(`
                                        <tr for="${input.value}">
                                            <td>${input.getAttribute("username")}</td>
                                            <td>
                                                <a class="text-danger" href="javascript:void(0)" title="@Localizer["Delete"]" onclick="DeleteUser('${input.value}')">
                                                    <i class="bx bx-trash me-1"></i>
                                                </a>
                                            </td>
                                        </tr>
                                    `);
        });
    $("#MediumModal").modal("hide");
}

function DeleteUser(userId) {
    document.querySelector(`#users tbody tr[for='${userId}']`).remove();
    selectedUserIds = selectedUserIds.filter(id => id != userId);
}

function SelectCourses() {
    Array.from(document.querySelectorAll("input[name='courseId']:checked"))
        .forEach(input => {
            selectedCourseIds.push(input.value);
            $("#courses tbody").append(`
                                            <tr for="${input.value}">
                                                <td>${input.getAttribute("title")}</td>
                                                <td>
                                                    <a class="text-danger" href="javascript:void(0)" title="@Localizer["Delete"]" onclick="DeleteCourse('${input.value}')">
                                                        <i class="bx bx-trash me-1"></i>
                                                    </a>
                                                </td>
                                            </tr>
                                        `);
        });
    $("#MediumModal").modal("hide");
}

function DeleteCourse(courseId) {
    document.querySelector(`#courses tbody tr[for='${courseId}']`).remove();
    selectedCourseIds = selectedCourseIds.filter(id => id != courseId);
}

function SelectCategories() {
    Array.from(document.querySelectorAll("input[name='categoryId']:checked"))
        .forEach(input => {
            selectedCategoryIds.push(input.value);
            $("#categories tbody").append(`
                                                <tr for="${input.value}">
                                                    <td>${input.getAttribute("title")}</td>
                                                    <td>
                                                        <a class="text-danger" href="javascript:void(0)" title="@Localizer["Delete"]" onclick="DeleteCategory('${input.value}')">
                                                            <i class="bx bx-trash me-1"></i>
                                                        </a>
                                                    </td>
                                                </tr>
                                            `);
        });
    $("#MediumModal").modal("hide");
}

function DeleteCategory(categoryId) {
    document.querySelector(`#categories tbody tr[for='${categoryId}']`).remove();
    selectedCategoryIds = selectedCategoryIds.filter(id => id != categoryId);
}

function FillPartialPageId(pageId) {
    $("#PartialPageId").val(pageId);
    $(`#filter-for-discount-form`).submit();
}

function onSearchModalShowed() {

    disableSelectedUsersCheckbox();
    disableSelectedCoursesCheckbox();
    disableSelectedCategoriesCheckbox();

    document.querySelector("#filter-for-discount-form")?.addEventListener("submit", (e) => {
        disableSelectedUsersCheckbox();
        disableSelectedCoursesCheckbox();
        disableSelectedCategoriesCheckbox();
    })
}

try {
    document.getElementById("discount-limitation-form")?.addEventListener("submit", (e) => {
        e.preventDefault();
        e.stopPropagation();

        if (e.target["UsageCount.Count"]) {
            var usageCountValue = e.target["UsageCount.Count"].value;
            if (usageCountValue === null || usageCountValue === undefined || usageCountValue.trim() == '') {
                e.target["UsageCount.Count"].value = 0;
            }
        }

        var counter = 0;
        selectedUserIds.forEach(userId => {
            var input = document.createElement("input");
            input.setAttribute("type", "hidden");
            input.setAttribute("name", `Users[${counter}].UserId`);
            input.setAttribute("value", userId);
            e.target.appendChild(input);
            counter++;
        });

        counter = 0;
        selectedCourseIds.forEach(courseId => {
            var input = document.createElement("input");
            input.setAttribute("type", "hidden");
            input.setAttribute("name", `Courses[${counter}].CourseId`);
            input.setAttribute("value", courseId);
            e.target.appendChild(input);
            counter++;
        });

        counter = 0;
        selectedCategoryIds.forEach(categoryId => {
            var input = document.createElement("input");
            input.setAttribute("type", "hidden");
            input.setAttribute("name", `Categories[${counter}].CategoryId`);
            input.setAttribute("value", categoryId);
            e.target.appendChild(input);
            counter++;
        });

        e.target.submit();
    });
} catch (e) {
    console.log(e);
}