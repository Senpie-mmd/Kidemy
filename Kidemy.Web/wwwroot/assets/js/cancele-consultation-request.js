async function cancle(url) {
    Swal.fire({
        html: `<b>${await jsLocalizer("AreYouSureToCancele")}</b>`,
        showDenyButton: true,
        confirmButtonText: await jsLocalizer("Yes"),
        denyButtonText: await jsLocalizer("No")
    }).then((result) => {
        if (result.isConfirmed) {
            var formData = {};
            addAntiforgeryToken(formData);

            $.ajax({
                type: 'post',
                url: url,
                data: formData,
                success: function (response) {
                    window.location.reload();
                },
                error: async function (response) {
                    Swal.fire({
                        icon: 'error',
                        title: await jsLocalizer("Error.e"),
                        confirmButtonText: await jsLocalizer("Ok"),
                    });
                }
            });
        }
    });
}