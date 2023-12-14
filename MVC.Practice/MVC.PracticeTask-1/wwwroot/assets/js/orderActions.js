let actionBtns = document.querySelectorAll(".action-btn")

actionBtns.forEach(actBtn => actBtn.addEventListener("click", (e) => {
    e.preventDefault();
    let url = actBtn.getAttribute("href")

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, do it!"
    }).then((result) => {
        if (result.isConfirmed) {

            fetch(url).then(res => {
                if (res.ok == true) {
                    window.location.reload(true);

                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Oops...",
                        text: "Something went wrong!",
                    });
                }

            })
        }
    });
}))


