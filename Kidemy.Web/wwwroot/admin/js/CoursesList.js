function searchForm() {
    event.preventDefault();

    let form = $("#filterCourseComments");
    let resultElement = $('#CustomLargeModalBody')

    $.ajax({
        url: "/Admin/Course/LoadCourseComments",
        type: "get",
        data: form.serialize(),
        success: function (response) {
            resultElement.html(response);
        }
    });
}

function ModelFillPageId(id) {
    event.preventDefault();

    $("#pageNumber").val(id);
    searchForm();
}

function confirmComment(commentId, currentPage, CommentScore, IsConfirmed, Message, UserId, UserName) {

    var encodedMessage = encodeURIComponent(Message);
    var encodedUserName = encodeURIComponent(UserName);

    $('#CustomLargeModalBody').load("/Admin/Course/ConformCourseComment?commentId=" + commentId +
        "&currentPage=" + currentPage +
        "&CommentScore=" + CommentScore +
        "&IsConfirmed=" + IsConfirmed +
        "&CommentMessage=" + encodedMessage +
        "&UserId=" + UserId +
        "&UserName=" + encodedUserName);
}

function denyComment(commentId, currentPage, CommentScore, IsConfirmed, Message, UserId, UserName) {

    var encodedMessage = encodeURIComponent(Message);
    var encodedUserName = encodeURIComponent(UserName);

    $('#CustomLargeModalBody').load("/Admin/Course/DenyCourseComment?commentId=" + commentId +
        "&currentPage=" + currentPage +
        "&CommentScore=" + CommentScore +
        "&IsConfirmed=" + IsConfirmed +
        "&CommentMessage=" + encodedMessage +
        "&UserId=" + UserId +
        "&UserName=" + encodedUserName);
}

async function showCustomModal(url, title) {
    open_waiting();
    var response = await fetch(url, { method: "GET" });
    close_waiting();

    if (response.ok) {
        document.querySelector("#CustomLargeModalBody").innerHTML = await response.text();
        document.querySelector("#CustomLargModalTitle").innerHTML = title;

        $("#CustomLargeModal").modal("show");

    }
    else {
        Swal.fire({
            icon: 'error',
            title: await jsLocalizer("Error.e"),
            html: await jsLocalizer("Error")
        });
    }
}