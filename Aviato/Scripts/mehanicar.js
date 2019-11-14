
////////////MEHANICAR Licence////////////////
var nizIzabranihLicenci = [];
var nizDatumaLicenci = [];
var plusLicenca = document.querySelector(".plus-licenca");
var dropDownlicence = document.getElementById("Mehanicar_Licenca");
var inputDatumLicence = document.getElementById("Mehanicar_DatumLicence");
var porukaZaIzabranuLicencu1 = document.querySelector("#licencaPoruka1");
var porukaZaIzabranuLicencu2 = document.querySelector("#licencaPoruka2");
var licenceUnos = document.querySelector("#LicenceUnos");
var licenceDatumUnos = document.querySelector("#licenceDatumUnos");
var izabranaLicencaSifra;
var datumLicenceSifra;
var ispisLicenceBox = document.querySelector("#ispis-licence-box");
var izabranaLicencaText;
var izabraniDatumText;

function uzmiTxtLicence(licenca) {
    izabranaLicencaText = licenca;
    return izabranaLicencaText;
}

function uzmiDatumLicence(datum) {
    izabraniDatumText = datum;
    console.log("onchange", datum)
    return izabraniDatumText;
}

function ispisiDodatuLicencu() {
    var licencaTxt = document.createTextNode(izabranaLicencaText);
    var datumTxt = document.createTextNode(datumLicenceSifra);
    var space = document.createTextNode(" - ");
    var para = document.createElement("p");
    para.appendChild(licencaTxt);
    para.appendChild(space);

    console.log(datumTxt)

    para.appendChild(datumTxt);
    para.classList.add("mb-1");
    para.style.display = "inline-block";

    var ukloniTxt = document.createTextNode(" | ukloni");
    var ukloniDugme = document.createElement("a");
    ukloniDugme.appendChild(ukloniTxt);
    ukloniDugme.id = dropDownlicence.value;
    ukloniDugme.classList.add("ukloniDugme");

    var licencaHolder = document.createElement("div");
    licencaHolder.appendChild(para);
    licencaHolder.appendChild(ukloniDugme);
    licencaHolder.classList.add("mt-2");

    ukloniDugme.addEventListener("click", function (e) {
        var removeId = nizIzabranihLicenci.indexOf(e.target.id);
        nizDatumaLicenci.splice(removeId, 1);
        nizIzabranihLicenci.splice(removeId, 1);
        ispisLicenceBox.removeChild(e.target.parentElement)
    }) 
    
    ispisLicenceBox.appendChild(licencaHolder);

}

function vratiNizLicenci() {

    plusLicenca.addEventListener("click", function () {
        console.log(inputDatumLicence.value)
        izabranaLicencaSifra = dropDownlicence.value;
        datumLicenceSifra = inputDatumLicence.value;
        

        if (nizIzabranihLicenci.includes(izabranaLicencaSifra)) {
            porukaZaIzabranuLicencu1.style.display = "inline";
            porukaZaIzabranuLicencu2.style.display = "none";
        }
        else {
            if (izabranaLicencaText == null || izabranaLicencaText == "Izaberite licencu" || datumLicenceSifra == "" || datumLicenceSifra == "") {
                porukaZaIzabranuLicencu2.style.display = "inline";
                porukaZaIzabranuLicencu1.style.display = "none";
            }
            else {
                nizIzabranihLicenci.push(izabranaLicencaSifra);
                nizDatumaLicenci.push(datumLicenceSifra);
                //nizIzabranihJezika = [...new Set(nizIzabranihJezika)];
                licenceUnos.value = nizIzabranihLicenci;
                licenceDatumUnos.value = nizDatumaLicenci;
                porukaZaIzabranuLicencu1.style.display = "none";
                porukaZaIzabranuLicencu2.style.display = "none";
                
                ispisiDodatuLicencu()
            }

        }
    });
}

vratiNizLicenci();