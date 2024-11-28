//#region SideBar item Activation

document.addEventListener('DOMContentLoaded', () => {

    var hrefs = document.querySelectorAll(`[href]`);
    for (var i = 0; i < hrefs.length; i++) {


        if (hrefs[i].getAttribute('href').toLowerCase() == window.location.pathname.toLowerCase() && hrefs[i].classList.contains('menu-link')) {
            var parent = hrefs[i].parentElement;
            hrefs[i].parentElement.classList.add('active');

            while (true) {
                var parent = parent.parentElement;
                if (parent.classList.contains('menu-inner')) {
                    break;
                }
                else {
                    if (parent.classList.contains('menu-item')) {
                        parent.classList.add('open', "active");
                    }
                }
            }

        }
    }

})

//#endregion