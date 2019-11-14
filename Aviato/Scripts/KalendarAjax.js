var xhr;

if (window.XMLHttpRequest) {
    xhr = new XMLHttpRequest();
}
if (xhr == null) {
    console.log("Your browser does not support XMLHTTP!");
}
xhr.open("GET", "../Home/SkupiLetove");
xhr.send(null);

let vremena = [];
var sadrzaj;

(xhr.onreadystatechange = function () {
    if (xhr.status === 200 && xhr.readyState === 4) {
        let xhrt = xhr.responseText;
        conParser(xhrt);
    }
})();

function conParser(xhrt) {
    sadrzaj = JSON.parse(xhrt);
    popuniKalendar(sadrzaj);
};

function popuniKalendar(sadrzaj) {
    for (let c in sadrzaj) {
        let vreme = new Date(Number(sadrzaj[c].VremePoletanja.substring(6, 19)))
        let letovi = {
            title: sadrzaj[c].LetId,
            start: vreme,
            allDay: true
        };
        vremena.push(letovi);
        ispisiKalendar(vremena);
    }
}

let datumZaPrikaz = document.querySelector('.idLeta'),
    kalendar = document.querySelector('.idLeta');

////

var city = document.querySelector("#destinacijaSaControlla").value;


function prognoza(city) {
    //var city = document.querySelector("#destinacija").innerHTML.trim();
    
    var xhr;

    if (window.XMLHttpRequest) {
        xhr = new XMLHttpRequest();
    }
    if (xhr == null) {
        console.log("Vaš browser ne podržava ovu funkcionalnost!");
    }

    xhr.open('GET', 'https://api.openweathermap.org/data/2.5/weather?q=' + city + '&units=metric&appid=57dbd56603443fed04dfc54f9fe1808b');
    xhr.send(null);

    (xhr.onreadystatechange = function () {
        if (xhr.status === 200 && xhr.readyState === 4) {
            let xhrt = xhr.responseText;
            conParser(xhrt);
        }
    })();

    function conParser(xhrt) {

        var prognozaSadrzaj;
        prognozaSadrzaj = JSON.parse(xhrt);
        var vremeIspis = document.querySelector(".vremenska-ispis");
        vremeIspis.style.opacity = "1";
        vremeIspis.innerHTML = `<p><span class="prognoza-p">Temperatura:</span> ${prognozaSadrzaj.main.temp}° C</p>
                                <p><span class="prognoza-p">Vetar:</span> ${prognozaSadrzaj.wind.speed}km/h</p>
                                <p><span class="prognoza-p">Oblačnost:</span> ${prognozaSadrzaj.clouds.all}%</p>`;
        vremeIspis.style.transition = ".7s";
    };
}

window.onclick = (e) => {
    
    if (e.target.className == "fc-title") {
        datumZaPrikaz.value = e.target.innerHTML;
        $(".detalji").load(`/Let/DetaljiLeta?id=${e.target.innerHTML}`);

        setTimeout(function () { city = document.querySelector("#destinacija").innerHTML.trim(), prognoza(city) }, 70);
    }
    if (e.target.className == "fc-content") {
        datumZaPrikaz.value = e.target.innerText;
        $(".detalji").load(`/Let/DetaljiLeta?id=${e.target.innerText}`);
        
        
        setTimeout(function () { city = document.querySelector("#destinacija").innerHTML.trim(), prognoza(city) }, 70);
    }
}

prognoza(city);


    setTimeout(function () { city = document.querySelector("#destinacija").innerHTML.trim(); }, 2000);
    

