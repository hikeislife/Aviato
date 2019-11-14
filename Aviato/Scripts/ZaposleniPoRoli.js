var zaposleniHolder = document.querySelector("#zaposleni-table-holder");

window.onload = () => {
    zaposleniHolder.style.opacity = "1";
} 
function rola(rola) {
    $("#zaposleni-table-holder").load(`/Zaposleni/IndexPoRoli?rola=${rola}`);
}