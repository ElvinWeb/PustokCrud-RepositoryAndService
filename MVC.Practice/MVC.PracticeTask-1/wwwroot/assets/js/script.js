let addToBasketBns = document.querySelectorAll(".add-to-basket");
addToBasketBns.forEach(btn => btn.addEventListener("click", function (e) {
    e.preventDefault();
    let url = btn.getAttribute("href");
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    fetch(url)
        .then(response => {
            if (response.ok == true) {
                toastr["success"]("Added Successful")
            } else {
                toastr["error"]("Error!")
            }
        })
}));


const btns = document.querySelectorAll(".show-modal-btn");

btns.forEach(btn => {
    btn.addEventListener("click", (e) => {
        e.preventDefault();
        const url = btn.getAttribute("href");

        fetch(url)
            .then(response => response.text())
            .then(data => {
                const modal = document.querySelector(".book-modal");
                modal.innerHTML = data
            })
    })
})
