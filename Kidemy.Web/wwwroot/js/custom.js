// access denied alert
window.addEventListener("load", async () => {
    if (window.location.href.includes("?accessDenied")) {
        var message = "به قسمت مربوطه دسترسی ندارید";
        var title = "خطا";
        var btnTitle = "باشه";
        Swal.fire({
            icon: 'error',
            title: title,
            text: message,
            confirmButtonText: btnTitle,
        });

    }
});

window.addEventListener("load", () => {
    var startBtn = document.querySelector('a.scrollable')
    if (startBtn) {
        startBtn.addEventListener('click', function (e) {
            e.preventDefault();

            var target = document.getElementById(this.getAttribute('target-element'));

            //Height of header
            var offset = document.querySelector('header').clientHeight;
            var elementPosition = target.offsetTop;
            var offsetPosition = elementPosition - offset;

            $('html, body').animate({
                scrollTop: offsetPosition
            }, 500);
        });
    }
});

//reply comment
window.addEventListener("load", () => {
    var allReplyBtn = document.querySelectorAll('div a[reply-value]');

    var replyCommentIdInput = document.querySelector('input[name="ReplyCommentId"]');
    var showCommentTextElement = document.querySelector("h5#show-comment-text");

    if (allReplyBtn) {
        allReplyBtn.forEach((btn) => {
            btn.addEventListener('click', async function (e) {
                e.preventDefault();
                var replyValue = this.getAttribute('reply-value');
                replyCommentIdInput.value = replyValue;

                var commentText = this.getAttribute('comment-text');

                showCommentTextElement.innerHTML = await jsLocalizer("AnswerTo") + ":" + " " + commentText;
                showCommentTextElement.parentElement.classList.remove('d-none');
                showCommentTextElement.parentElement.classList.add('d-flex');
            })
        })
    }

    var cancelReplyBtn = document.querySelector('span.cancel-reply');

    if (cancelReplyBtn) {
        cancelReplyBtn.addEventListener('click', function (e) {

            showCommentTextElement.parentElement.classList.add('d-none');
            showCommentTextElement.parentElement.classList.remove('d-flex');

            replyCommentIdInput.value = "";

            showCommentTextElement.innerHTML = "";
        })
    }
})

function checkFavorite(courseIds) {

    if (courseIds) {
        courseIds.forEach(function (courseId) {

            var favourite = document.querySelectorAll(`#like-${courseId}`);
            if (favourite != null) {
                favourite.forEach(heart => {
                    heart.className = "fas fa-heart";
                    heart.style.color = 'red';
                });
            }
        });

    }
}
