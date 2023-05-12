

function menuBtnClick() {
    let sortMenu = document.getElementById("sort-menu");
    let isVisible = sortMenu.style.display
    if (isVisible == "none") {
        sortMenu.style.display = 'block'
    }
    else {
        sortMenu.style.display = 'none'
    }

}


function filterClick() {
    let filters = document.getElementById("filters");
    let isVisible = filters.style.display

    if (isVisible == "none") {
        filters.style.display = 'block'
    }
    else {
        filters.style.display = 'none'
    }
}