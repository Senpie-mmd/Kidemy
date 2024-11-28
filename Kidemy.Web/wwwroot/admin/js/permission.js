$(document).ready(function () {

    function checkAll() {
        if ($("input[name=PermissionsId]").length === $("input[name=PermissionsId]:checked").length) {
            $("#SelectAll").prop("checked", true);
        }
    }

    function expandAll() {
        $(".accordion-button").removeClass("collapsed");
        $(".accordion-collapse").addClass("show");
        $(".accordion-button, .accordion-collapse").attr("aria-expanded", true);
        $(".accordion-collapse").css("height", "auto");
    }

    function collapseAll() {
        $(".accordion-button").addClass("collapsed");
        $(".accordion-collapse").removeClass("show");
        $(".accordion-button, .accordion-collapse").attr("aria-expanded", false);
        $(".accordion-collapse").css("height", "0");
    }

    function setCheckboxState(id, state) {
        var input = $(`input[data-id=${id}]`);
        input.prop("checked", state);

        //var parentid = input.attr("data-parentId");
        //onCheckboxClick(id, parentid);
    }

    function isAnySubPermissionChecked(parentId) {
        var subPermissions = $(`input[data-parentId=${parentId}]`);
        return subPermissions.is(":checked");
    }

    function isAllSubPermissionsChecked(parentId) {
        var subPermissions = $(`input[data-parentId="${parentId}"]`);
        return (subPermissions.length > 0) && subPermissions.not(":checked").length === 0;
    }

    function updateParentCheckbox(parentId) {

        if (parentId === undefined) {
            return;
        }

        if (isAllSubPermissionsChecked(parentId)) {
            setCheckboxState(parentId, true);
            updateParentCheckbox($(`input[data-id=${parentId}]`).attr("data-parentId"));
        } else if (!isAnySubPermissionChecked(parentId)) {
            setCheckboxState(parentId, false);
            updateParentCheckbox($(`input[data-id=${parentId}]`).attr("data-parentId"));
        }
        else if (isAnySubPermissionChecked(parentId)) {
            setCheckboxState(parentId, true);
            updateParentCheckbox($(`input[data-id=${parentId}]`).attr("data-parentId"));
        }
    }

    function onCheckboxClick(id, parentId) {
        if (parentId === undefined) {
            id = $(this).attr("data-id");
            parentId = $(this).attr("data-parentId");
        }

        input = $(`input[data-id=${id}]`);

        if (input.checked) {
            if (parentId !== undefined) {
                setCheckboxState(parentId, true);
                updateParentCheckbox(parentId);
            }
        } else {
            if (parentId !== undefined) {
                updateParentCheckbox(parentId);
            }
            $("#SelectAll").prop("checked", false);
        }
    }

    function onSelectAllClick() {
        if (this.checked) {
            expandAll();
            $("input[name=PermissionsId]").each(function () {
                setCheckboxState($(this).attr("data-id"), true);
            });
        } else {
            collapseAll();
            $("input[name=PermissionsId]").each(function () {
                setCheckboxState($(this).attr("data-id"), false);
            });
        }
    }

    checkAll();
    $("#SelectAll").on("click", onSelectAllClick);
    $("input[name=PermissionsId]").on("click", onCheckboxClick);

    document.querySelector("#collapse-1").classList.add('show')

});
