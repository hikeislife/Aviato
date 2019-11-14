

window.onload = () => {

    var destinacija = document.querySelector("#Destinacija");
    var letId = document.querySelector("#LetId");
    //${ destinacija.value }& ${ letId.value }
    $(".stjuardi").load(`/Let/StjuardiPoDestinaciji?id=${destinacija.value}`);
    destinacija.addEventListener("change", function () {
        $(".stjuardi").load(`/Let/StjuardiPoDestinaciji?id=${destinacija.value}`);
    })
}
