const port = 8000;
const appAdres = "http://localhost:" + port;

const removeProductAdmin = document.querySelector(".remove-admin-btn");

removeProductAdmin.forEach((element) => {
    element.addEventListener("click", async (event) => {
        const clickedBtn = event.target;
        const tr = clickedBtn.parentNode.parentNode;
        const leftSibling = tr.querySelector("td.count-products");

        const tdImg = leftSibling.previousElementSibling.previousElementSibling;
        const img = tdImg.querySelector("img");
        const imgSrc = img.getAttribute("src");
        const id = imgSrc.split("/").pop();

        fetch(appAdres + "/cart" + id, {
            method: "DELETE",
        })
            .then((res) => res.body.json())
            .then((body) => {
                if (body["id"] == id) {
                    tr.remove();
                } else {
                    //error handling
                }
            });
    });
});
