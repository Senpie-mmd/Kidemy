window.addEventListener("load", async () => {
    addToCart();
    await getCart();
})

var courseId = 0;
function addToCart() {
    document.querySelectorAll(`.add-to-cart`)
        .forEach(button => {
            button.addEventListener("click", async (event) => {
                var btn = event.target;

                courseId = btn.dataset.courseid;

                btnToWaiting(btn);

                var data = {
                    courseId: courseId
                };

                addAntiforgeryToken(data)


                var response = await fetch("/AddToCart", {
                    method: "POST",
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                });

                var result = await response.json();

                if (result.isSuccess == true) {
                    showSuccess(result.message);
                    await getCart();
                }
                else {
                    showError(result.message)
                }
                    

                waitingToBtn(btn);
            });
        });
}

async function removeFromCart(cartItemId) {

    var data = {
        cartItemId: cartItemId
    }

    addAntiforgeryToken(data);

    var response = await fetch(`/RemoveFromCart`, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });

    var result = await response.json();

    if (result.isSuccess == true) {
        if (window.location.href.toLowerCase().includes("/cart")) {
            window.location.reload();
        }
        else {
            await getCart();
        }

    }
    else {
        showError(result.message);
    }
}

var cart = null;
async function getCart() {
    var response = await fetch(`/GetCart`, {
        method: "GET",
        headers: {
            'Content-Type': 'application/json'
        }
    });

    var result = await response.json();

    if (result.isSuccess == true) {
        cart = result.data;
        await updateCart();
    }
    else {
        //toastr.error(result.message, await jsLocalizer("Error.e"));
    }
}


async function updateCart() {
    var cartItemsCountLabel = document.querySelector("#cart-items-count");
    var cartModal = document.getElementById("cart-modal");

    if (cart.items.length == 0) {
        cartItemsCountLabel.innerHTML = "";
        cartItemsCountLabel.style.display = "none";

        cartModal.querySelector(".card-footer").classList.add("d-none");
        cartModal.querySelector(".card-body").innerHTML = `<div class="alert alert-info fit-content-height mx-3 mt-3">${await jsLocalizer("YourCartIsEmpty")}</div>`;
    }
    else {
        cartItemsCountLabel.innerHTML = cart.items.length;
        cartItemsCountLabel.style.display = "block";
        cartModal.querySelector(".card-footer").classList.remove("d-none");

        var modalBodyContent = ``;

        cart.items.forEach((item,index) => {
            modalBodyContent +=
                `<div class="row p-3 g-2">
					<!-- Image -->
					<div class="col-3">
						<img class="rounded-2" src="${courseImagesPath}${item.course.imageFileName}" alt="avatar">
					</div>

					<div class="col-9 my-auto">
						<!-- Title -->
						<div class="d-flex justify-content-between">
							<h6 class="m-0 fw-normal">${item.course.title}</h6>
							<a href="javascript:void(0)" onclick="removeFromCart(${item.id})" class="small text-primary-hover remove-item-from-cart"><i class="bi bi-x-lg"></i></a>
						</div>
					</div>
				</div>
                <hr class="m-0">`;
        });


        cartModal.querySelector(".card-body").innerHTML = ``;
        cartModal.querySelector(".card-body").insertAdjacentHTML("beforeend", modalBodyContent);
    }
}

function applyDiscountCode() {
    var discountCode = document.querySelector("input[name='DiscountCode']").value;

    window.location.href = "/Cart?discountCode=" + discountCode;
}