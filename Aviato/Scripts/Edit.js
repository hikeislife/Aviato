
//////////////////  MEHANICAR  ///////////////////////
var licencaInit = document.querySelectorAll(".meh_Licenca");
var inputLicenca = document.querySelectorAll(".meh_Tip_NazivTipa");
var inputDatumLicence = document.querySelectorAll(".meh_DatumLicence");

var promenaLicenci = document.querySelector("#promenaLicenci");
var promenaDatuma = document.querySelector("#promenaDatuma");
var licencaPoruka = document.querySelector(".licencaPoruka");
if (licencaPoruka != null) {
    licencaPoruka.style.visibility = "hidden";
}



var initNizLicenci = [];
var nizLicenci = [];
var datumLicenci = [];

for (var i = 0; i < licencaInit.length; i++) {
    initNizLicenci.push(licencaInit[i].value)
    nizLicenci.push(licencaInit[i].value);
    datumLicenci.push(inputDatumLicence[i].value);
    inputLicenca[i].value = licencaInit[i].value;
}
console.log("init", initNizLicenci)
function uzmiLicencuIDatumOnSubmit() {

    for (var i = 0; i < nizLicenci.length; i++) {

        datumLicenci[i] = inputDatumLicence[i].value
        nizLicenci[i] = inputLicenca[i].value; 

        if (nizLicenci[i] == "") {
            nizLicenci[i] = licencaInit[i].value;
        }
    } 
    promenaDatuma.value = datumLicenci;
    promenaLicenci.value = nizLicenci;

    console.log("submit ", nizLicenci)
    console.log("submit ", datumLicenci)
}

function uzmiLicencuOnChange(licenca, event) {

    if (nizLicenci.includes(licenca) || initNizLicenci.includes(licenca)) {
        licencaPoruka.style.visibility = "visible";
        event.target.style.border = "1px solid red";
        event.target.value = "";
    }
    else {
        licencaPoruka.style.visibility = "hidden";
        event.target.style.border = "none";
    }
    for (var i = 0; i < inputLicenca.length; i++) {

        nizLicenci[i] = inputLicenca[i].value;
    }

    console.log("change ", nizLicenci)
}



//////////////////  STJUARD  ///////////////////////
var jezikInit = document.querySelectorAll(".stju_Stjuard");
var inputJezika = document.querySelectorAll(".stju_Jezik_Jezici");
var inputJezikaSingle = document.querySelector(".stju_Jezik_Jezici");

var promenaJezika = document.querySelector("#promenaJezika");
var jezikPoruka = document.querySelector(".jezikPoruka");
if (jezikPoruka != null) {
    jezikPoruka.style.visibility = "hidden";
}


var initNizJezici = [];
var nizJezika = [];

for (var i = 0; i < jezikInit.length; i++) {

    initNizJezici.push(jezikInit[i].value)
    nizJezika.push(jezikInit[i].value);
    inputJezika[i].value = jezikInit[i].value;
}
console.log("init", initNizJezici)
function uzmiJezikeOnSubmit() {

    for (var i = 0; i < nizJezika.length; i++) {

        nizJezika[i] = inputJezika[i].value;

        if (nizJezika[i] == "") {
            nizJezika[i] = jezikInit[i].value;
        }
    }
    promenaJezika.value = nizJezika;

    console.log("submit", nizJezika)
}

function uzmiJezikOnChange(jezik, event) {
    
    if (nizJezika.includes(jezik) || initNizJezici.includes(jezik)) {
        jezikPoruka.style.visibility = "visible";
        event.target.style.border = "1px solid red";
        event.target.value = "";
    }
    else {
        jezikPoruka.style.visibility = "hidden"; 
        event.target.style.border = "none"; 
    }
    for (var i = 0; i < inputJezika.length; i++) {

        nizJezika[i] = inputJezika[i].value;
    }

    console.log("change", nizJezika)
}

/////////////////// SUBMIT CLICK ////////////////////
var submitClick = document.querySelector(".submitClick");

submitClick.addEventListener('click', function () {

    if (nizLicenci.length > 0) {
        uzmiLicencuIDatumOnSubmit();
    }
     if (nizJezika.length > 0) {
        uzmiJezikeOnSubmit();
    }

    console.log("submit", nizJezika)
})
