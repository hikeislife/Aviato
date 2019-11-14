//Validacija poslednjeg lekarskog - -6 meseci uvek, -1 mesec za prizemljenog pilota
var zsInput = document.querySelector("#Pilot_OcenaZS");
var button = document.querySelector(".okidac"); 
var vreme = document.querySelector(".datum"); 
var ispisGreske = document.querySelector(".zs");

//dodaje i oduzima mesece 
function addMonths(date, months) {
    date.setMonth(date.getMonth() + months);
    return date;
}

//format i konvert vremena
function stringToDate(_date, _format, _delimiter) {
    var formatLowerCase = _format.toLowerCase();
    var formatItems = formatLowerCase.split(_delimiter);
    var dateItems = _date.split(_delimiter);
    var monthIndex = formatItems.indexOf("mm");
    var dayIndex = formatItems.indexOf("dd");
    var yearIndex = formatItems.indexOf("yyyy");
    var month = parseInt(dateItems[monthIndex]);
    month -= 1;
    var formatedDate = new Date(dateItems[yearIndex], month, dateItems[dayIndex]);
    return formatedDate;
}

//stringToDate("17/9/2014", "dd/MM/yyyy", "/");
//stringToDate("9/17/2014", "mm/dd/yyyy", "/")
//stringToDate("9-17-2014", "mm-dd-yyyy", "-")

button.addEventListener("click", function (e) {
    console.log("tu sam")
    if (!zsInput.checked && stringToDate(vreme.value, "mm/dd/yyyy", "/") > addMonths(new Date(), -6)) {
        if (stringToDate(vreme.value, "mm/dd/yyyy", "/") < addMonths(new Date(), -1)) {
            ispisGreske.innerHTML = "Vreme je van opsega od -1 meseca od sadašnjeg za prizemljenog pilota";
            e.preventDefault();
            return false;
        }
        else {
            ispisGreske.innerHTML = "";
        }
    }
    else if (zsInput.checked && stringToDate(vreme.value, "mm/dd/yyyy", "/") < addMonths(new Date(), -6)) {
        ispisGreske.innerHTML = "Vreme je van opsega od -6 meseci od sadašnjeg";
        e.preventDefault();
        return false;
    }
})

