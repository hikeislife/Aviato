console.log("tu sam mehanicar");

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
    return izabraniDatumText;
}

function ispisiDodatuLicencu() {
    var licencaTxt = document.createTextNode(izabranaLicencaText);
    var datumTxt = document.createTextNode(izabraniDatumText);
    var space = document.createTextNode(" - ");
    var para = document.createElement("p");
    para.appendChild(licencaTxt);
    para.appendChild(space);
    para.appendChild(datumTxt);

    var ukloniTxt = document.createTextNode("ukloni");
    var ukloniDugme = document.createElement("a");
    ukloniDugme.appendChild(ukloniTxt);

    ispisLicenceBox.appendChild(para);
    ispisLicenceBox.appendChild(ukloniDugme);
    
}

function vratiNizLicenci() {

    plusLicenca.addEventListener("click", function () {

        izabranaLicencaSifra = dropDownlicence.value;
        datumLicenceSifra = inputDatumLicence.value;

        if (nizIzabranihLicenci.includes(izabranaLicencaSifra)) {
            porukaZaIzabranuLicencu1.style.visibility = "visible";
        }
        else { 
            if (izabranaLicencaText == undefined || izabranaLicencaText == "Izaberite licencu" || izabraniDatumText == undefined || izabraniDatumText == "") {
                porukaZaIzabranuLicencu2.style.visibility = "visible";
            }
            else {
                nizIzabranihLicenci.push(izabranaLicencaSifra);
                nizDatumaLicenci.push(datumLicenceSifra);
                //nizIzabranihJezika = [...new Set(nizIzabranihJezika)];
                licenceUnos.value = nizIzabranihLicenci;
                licenceDatumUnos.value = nizDatumaLicenci;
                porukaZaIzabranuLicencu1.style.visibility = "hidden";
                porukaZaIzabranuLicencu2.style.visibility = "hidden";
                //console.log("niz1 " + nizIzabranihLicenci, "niz2 " + nizDatumaLicenci)

                ispisiDodatuLicencu()
            }
            
        }
    });
}

vratiNizLicenci();