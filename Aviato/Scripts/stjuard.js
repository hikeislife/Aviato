////////////STJUARD Jezici////////////////
var nizIzabranihJezika = [];
var plusJezik = document.querySelector(".plus-jezik");
var jeziciUnos = document.querySelector("#JeziciUnos");
var porukaZaIzabranJezik = document.querySelector("#jezikPoruka");
var porukaZaNeizabrano = document.querySelector("#jezikPoruka1");
var dropDownJezici = document.getElementById("Stjuard_JezikId");
var ispisJezikaBox = document.querySelector("#ispis-jezika-box");
var izabraniJezikText;
var izabraniJezikSifra;

function uzmiTxtJezika(jezik) {
    izabraniJezikText = jezik;
    return izabraniJezikText;
}

function ispisiDodatiJezik() {
    var jezikTxt = document.createTextNode(izabraniJezikText);
    var para = document.createElement("span");
    para.appendChild(jezikTxt);
    
    var ukloniTxtJezik = document.createTextNode(" | ukloni");
    var ukloniDugmeJezik = document.createElement("a");
    ukloniDugmeJezik.appendChild(ukloniTxtJezik);
    ukloniDugmeJezik.id = dropDownJezici.value;
    ukloniDugmeJezik.classList.add("ukloniDugmeJezik");

    var jezikHolder = document.createElement("div");
    jezikHolder.appendChild(para);
    jezikHolder.appendChild(ukloniDugmeJezik);

    ukloniDugmeJezik.addEventListener("click", function (e) {
        var removeId = nizIzabranihJezika.indexOf(e.target.id);
        nizIzabranihJezika.splice(removeId, 1);
        ispisJezikaBox.removeChild(e.target.parentElement)
    }) 

    ispisJezikaBox.appendChild(jezikHolder);
}

function vratiNizJezika() {

    plusJezik.addEventListener("click", function () {

        izabraniJezikSifra = dropDownJezici.value;

        if (nizIzabranihJezika.includes(izabraniJezikSifra)) {
            porukaZaIzabranJezik.style.display = "inline";
        }
        else {
            if (izabraniJezikText == undefined || izabraniJezikText == "Izaberite jezik") {
                porukaZaNeizabrano.style.display = "inline";
                porukaZaIzabranJezik.style.display = "none";
            }
            else {
                nizIzabranihJezika.push(izabraniJezikSifra);
                jeziciUnos.value = nizIzabranihJezika;
                porukaZaIzabranJezik.style.display = "none";
                porukaZaNeizabrano.style.display = "none";

                ispisiDodatiJezik()
            }
        }
    });
}

vratiNizJezika();