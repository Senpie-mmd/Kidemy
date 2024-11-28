function searchForm() {
    event.preventDefault();

    let form = $("#filterBlogComments");
    let resultElement = $('#CustomLargeModalBody')

    $.ajax({
        url: "/Admin/Blog/LoadBlogComments",
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

function confirmComment(commentId, currentPage, IsConfirmed, Message, UserId, UserName) {

    var encodedMessage = encodeURIComponent(Message);
    var encodedUserName = encodeURIComponent(UserName);

    $('#CustomLargeModalBody').load("/Admin/Blog/ConformBlogComment?commentId=" + commentId +
        "&currentPage=" + currentPage +
        "&IsConfirmed=" + IsConfirmed +
        "&CommentMessage=" + encodedMessage +
        "&UserId=" + UserId +
        "&UserName=" + encodedUserName);
}

function denyComment(commentId, currentPage, IsConfirmed, Message, UserId, UserName) {

    var encodedMessage = encodeURIComponent(Message);
    var encodedUserName = encodeURIComponent(UserName);

    $('#CustomLargeModalBody').load("/Admin/Blog/DenyBlogComment?commentId=" + commentId +
        "&currentPage=" + currentPage +
        "&IsConfirmed=" + IsConfirmed +
        "&CommentMessage=" + encodedMessage +
        "&UserId=" + UserId +
        "&UserName=" + encodedUserName);
}

async function showCustomModal(url, title) {
    open_waiting();
    var response = await fetch(url, { method: "GET" });
    close_waiting();
    debugger;
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