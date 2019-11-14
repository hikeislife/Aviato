var specRolaDiv = document.querySelector(".spec-rola-izbor");

function SpecZaposlenog(rola) {
    switch (rola) {
        case "Stjuard":
            $(".spec-rola-izbor").load("/Stjuard/Create");
            break;
        case "Mehaničar":
            $(".spec-rola-izbor").load("/Mehanicar/Create");
            break;
        case "Pilot":
            $(".spec-rola-izbor").load("/Pilot/Create");
            break;
        default:
            specRolaDiv.innerHTML = "";
    }
}

