/**
 * Make sure your app runs on port 8000
 */
const csrfToken = document
    .querySelector('meta[name="csrf-token"]')
    .getAttribute("content");
const removeFromCart = document.getElementsByClassName("remove-cart-btn");

function isCountZero(string) {
    return parseInt(string) == 0;
}

for (var element of removeFromCart) {
    element.addEventListener("click", async (event) => {
        const clickedBtn = event.target;
        const tr = clickedBtn.parentNode.parentNode;
        const leftSibling = tr.querySelector("td.count-products");

        const tdImg = leftSibling.previousElementSibling.previousElementSibling;
        const img = tdImg.querySelector("img");
        const imgSrc = img.getAttribute("src");
        const id = imgSrc.split("/").pop();

        fetch("/cart/" + id, {
            method: "DELETE",

            mode: "same-origin", // no-cors, *cors, same-origin
            credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
                "X-CSRF-TOKEN": csrfToken,
            },
        })
            .then((rej) => {
                console.log(rej);
            })
            .then((res) => {
                console.log(res);
            })
            .then((body) => {
                if (body["id"] == id) {
                    leftSibling.textContent =
                        "" + parseInt(leftSibling.textContent) - 1;

                    if (isCountZero(leftSibling.textContent)) {
                        tr.remove();
                    }
                } else {
                    //error handling
                }
            });
    });
}
