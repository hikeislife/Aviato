//function prognoza() {
//    var city = document.querySelector("#destinacija").innerHTML.trim();

//    var xhr;

//    if (window.XMLHttpRequest) {
//        xhr = new XMLHttpRequest();
//    }
//    if (xhr == null) {
//        console.log("Vaš browser ne podržava ovu funkcionalnost!");
//    }

//    xhr.open('GET', 'https://api.openweathermap.org/data/2.5/weather?q=' + city + '&units=metric&appid=57dbd56603443fed04dfc54f9fe1808b');
//    xhr.send(null);

//    (xhr.onreadystatechange = function () {
//        if (xhr.status === 200 && xhr.readyState === 4) {
//            let xhrt = xhr.responseText;
//            conParser(xhrt);
//        }
//    })();

//    function conParser(xhrt) {

//        var prognozaSadrzaj;
//        prognozaSadrzaj = JSON.parse(xhrt);
//        var vremeIspis = document.querySelector(".vremenska-ispis");
//        vremeIspis.innerHTML = `<p><span class="prognoza-p">Temperatura:</span> ${prognozaSadrzaj.main.temp}° C</p>
//                                <p><span class="prognoza-p">Vetar:</span> ${prognozaSadrzaj.wind.speed}km/h</p>
//                                <p><span class="prognoza-p">Oblačnost:</span> ${prognozaSadrzaj.clouds.all}%</p>`;

//    };
//}

//window.onclick = (e) => {

//    if (e.target.className == "fc-title") {

//        datumZaPrikaz.value = e.target.innerHTML;
//        $(".detalji").load(`/Let/DetaljiLeta?id=${e.target.innerHTML}`);
//        var vremeIspis = document.querySelector(".vremenska-ispis");
//        vremeIspis.innerHTML = '';
//        prognoza();
//    }
//    if (e.target.className == "fc-content") {

//        datumZaPrikaz.value = e.target.innerText;
//        $(".detalji").load(`/Let/DetaljiLeta?id=${e.target.innerText}`);
//        var vremeIspis = document.querySelector(".vremenska-ispis");
//        vremeIspis.innerHTML = '';
//        prognoza();
//    }
//}






