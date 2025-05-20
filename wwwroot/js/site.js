document.addEventListener("DOMContentLoaded", (ev) => {
    document.querySelectorAll(".nav-link")
        .forEach((v) => {
            
            if (v.href == location.href) {
                v.classList.add("active")
            }
        })

})