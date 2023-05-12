var errorMessage = "{{ $errorMessage }}";
document.getElementById("error-message").textContent = errorMessage;
document.getElementById("error-popup").classList.remove("hidden");
document.querySelector(".close-popup").addEventListener("click", closeErrorPopup);

function closeErrorPopup() {
    document.getElementById("error-popup").classList.add("hidden");
}
