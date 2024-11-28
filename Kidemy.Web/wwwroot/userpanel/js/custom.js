
// customed navbar for userpanel
window.addEventListener('load', () => {
    var navbar = document.querySelector(".header.header-transparent.change-logo");
    if (navbar) {
        navbar.classList.remove('header-transparent')
    }
})

// activation of user panel sidebar items
window.addEventListener('load', () => {
    var url = new URL(window.location);

    var path = url.pathname;

    document.querySelector(`#offcanvasSidebar .list-group a.list-group-item[href='${path}']`)?.classList.add("active");
})


window.addEventListener('load', () => {

    const togglePassword = document.querySelector('#togglePassword');
    const password = document.querySelector('#id_password');
    
    togglePassword?.addEventListener('click', function (e) {
        // toggle the type attribute
        const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
        password.setAttribute('type', type);
        // toggle the eye slash icon
        this.classList.toggle('fa-eye-slash');
    });
   
})

// show message in modal
function showMessage(target) {
    $("#showMessage .modal-body").html(target.parentElement.querySelector("span").innerHTML);
    $("#showMessage").modal("show");
}