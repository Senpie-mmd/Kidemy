
async function unableToWithdraw(url) {
    Swal.fire({
        html: `<b>${await jsLocalizer("AreYouSureToUnable")}</b>`,
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
                success: async function (response) {
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

async function ableToWithdraw(url) {
    Swal.fire({
        html: `<b>${await jsLocalizer("AreYouSureToable")}</b>`,
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
                success: async function (response) {
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
