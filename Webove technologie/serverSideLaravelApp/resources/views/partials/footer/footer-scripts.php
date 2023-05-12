<script type="module" src="https://unpkg.com/ionicons@5.5.2/dist/ionicons/ionicons.esm.js"></script>
<script src="https://cdn.tailwindcss.com"></script>
<script nomodule src="https://unpkg.com/ionicons@5.5.2/dist/ionicons/ionicons.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/flowbite/1.6.4/flowbite.min.js"></script>
<script src="js/products.js"></script>
<script>
    const navLinks = document.querySelector('.nav-links')
    function onToggleMenu(e) {
        e.name = e.name === "grid-outline" ? "close" : "grid-outline";
        navLinks.classList.toggle('top-[9%]')
    }
</script>