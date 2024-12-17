let passangerButton = document.querySelectorAll(".passangerDiv button");
let passangerSum = document.querySelectorAll(".passanger_num");
let passangers = document.querySelector(".passangerButton p");
let passangerBtn = document.querySelector(".passangerButton");
let passangerDiv = document.querySelector(".passangerDivs");
let flightBtn = document.querySelector(".flightBtn");
let calendar = document.querySelector(".calendar");
let calendarBtn = document.querySelector(".calendar-controls");
let calendarDay = document.querySelectorAll(".dateNumber");
let calendarMonth = document.querySelector(".calendar-month-label");
let calendarYear = document.querySelector(".calendar-year-label");
let returnButton = document.querySelector(".return_svg");
let fromInput = document.querySelector(".from_input");
let toInput = document.querySelector(".to_input");
let promeCodeBtn = document.querySelector(".promeCodeButton");
let promeCodeInp = document.querySelector("#promeCodeInput");
let fromCountryDiv = document.querySelector(".from_country");
let toCountryDiv = document.querySelector(".to_country");
let countryButton = document.querySelectorAll(".country_button");
let countryInput = document.querySelectorAll(".cntr_input");
function updateTotalPassengers() {
    const total = Array.from(passangerSum)
        .map((p) => parseInt(p.textContent) || 0)
        .reduce((acc, curr) => acc + curr, 0);

    passangers.textContent = total;
}
const adultNumElement = document.querySelector(".adultnum");
adultNumElement.textContent = '1';
updateTotalPassengers();



//passangerButton.forEach((btn) => {
//    btn.addEventListener("click", function (e) {
//        e.preventDefault();
//        let number;

//        if (btn.classList.contains("passanger_minus")) {
//            number = parseInt(btn.nextElementSibling.textContent);

//            if (number > 0) {
//                btn.nextElementSibling.textContent = number - 1;
//            }
//        }

//        if (btn.classList.contains("passanger_plus")) {
//            number = parseInt(btn.previousElementSibling.textContent);
//            if (btn.previousElementSibling.classList.contains("infantnum")) {
//                const adultCount = parseInt(adultNumElement.textContent);
//                const currentInfants = parseInt(btn.previousElementSibling.textContent);
//                if (currentInfants < adultCount) {
//                    btn.previousElementSibling.textContent = number + 1;
//                }
//            } else {
//                btn.previousElementSibling.textContent = number + 1;
//            }
//        }

//        updateTotalPassengers();
//    });
//});


passangerBtn.addEventListener("click", function (e) {
    e.preventDefault();
    if (passangerDiv.style.display === "none") {
        passangerDiv.style.display = "block";
    } else {
        passangerDiv.style.display = "none";
    }
});
document.addEventListener('DOMContentLoaded', function () {
    const passangerButtons = document.querySelectorAll('.passanger_minus, .passanger_plus');
    const passangerSum = document.querySelectorAll('.passanger_num');

    passangerButtons.forEach((btn) => {
        btn.addEventListener("click", function (e) {
            e.preventDefault();

            let numberElement;
            let currentNumber;
            let passengerType;

            if (btn.classList.contains("passanger_minus")) {
                numberElement = btn.nextElementSibling;
                passengerType = numberElement.getAttribute("data-type");
                currentNumber = parseInt(numberElement.textContent);
                if (currentNumber > 0) {
                    numberElement.textContent = currentNumber - 1;
                }
            }

            if (btn.classList.contains("passanger_plus")) {
                numberElement = btn.previousElementSibling;
                passengerType = numberElement.getAttribute("data-type");
                currentNumber = parseInt(numberElement.textContent);
                if (btn.previousElementSibling.classList.contains("infantnum")) {
                    const adultCount = parseInt(adultNumElement.textContent);
                    const currentInfants = parseInt(btn.previousElementSibling.textContent);
                    if (currentInfants < adultCount) {
                        numberElement.textContent = currentNumber + 1;
                    }
                } else {
                    numberElement.textContent = currentNumber + 1;
                }
            }
            updateHiddenInputs(passengerType, numberElement.textContent);
            updateTotal(passangerSum);
        });
    });
    function updateHiddenInputs(type, value) {
        switch (type) {
            case 'adult':
                document.getElementById('adultCount').value = value;
                break;
            case 'children':
                document.getElementById('childrenCount').value = value;
                break;
            case 'infant':
                document.getElementById('infantCount').value = value;
                break;
        }
    }

    function updateTotal(elements) {
        const total = Array.from(elements)
            .map((p) => parseInt(p.textContent) || 0)
            .reduce((acc, curr) => acc + curr, 0);
        passangers.textContent = total;
    }

    updateTotal(passangerSum);
});


//passangerButton.forEach((btn) => {
//    btn.addEventListener("click", function (e) {
//        e.preventDefault();
//        if (btn.classList.contains("passanger_minus")) {
//            let number = parseInt(btn.nextElementSibling.textContent);

//            if (number === 0) {
//            } else {
//                btn.nextElementSibling.textContent = number - 1;
//            }
//        }
//        if (btn.classList.contains("passanger_plus")) {
//            let number = parseInt(btn.previousElementSibling.textContent);
//            btn.previousElementSibling.textContent = number + 1;
//        }
//        const total = Array.from(passangerSum)
//            .map((p) => parseInt(p.textContent) || 0)
//            .reduce((acc, curr) => acc + curr, 0);
//        passangers.textContent = total;
//    });
//});
flightBtn.addEventListener("click", function (e) {
    e.preventDefault();
    if (calendar.style.display === "none") {
        calendar.style.display = "block";
    } else {
        calendar.style.display = "none";
    }
});
calendarBtn.addEventListener("click", function (e) {
    e.preventDefault();
});
//calendarDay.forEach((day) => {
//    day.addEventListener("click", function (e) {
//        e.preventDefault();
//        let textMonth = calendarMonth.textContent;
//        let textYear = calendarYear.textContent;

//        let datetime = day.textContent + "." + textMonth + "." + textYear;
//        console.log(datetime);
//        flightBtn.value = datetime;
//        calendar.style.display = "none";
//    });
//});

calendarDay.forEach((day) => {
    day.addEventListener("click", function (e) {
        e.preventDefault();
        let selectedDay = day.textContent.trim().padStart(2, '0');
        let selectedMonth = calendarMonth.textContent;
        let monthNumber = new Date(Date.parse(selectedMonth + " 1, 2022")).getMonth() + 1;
        let selectedYear = calendarYear.textContent;
        let datetime = `${selectedDay}.${monthNumber.toString().padStart(2, '0')}.${selectedYear}`;
        flightBtn.value = datetime;
        calendar.style.display = "none";
    });
});
returnButton.addEventListener("click", function (e) {
    e.preventDefault();
    let from = fromInput.value;
    fromInput.value = toInput.value;
    toInput.value = from;
});

promeCodeBtn.addEventListener("click", function (e) {
    e.preventDefault();
    promeCodeBtn.style.display = "none";
    promeCodeInp.style.display = "block";
});
fromInput.addEventListener("click", function (e) {
    e.preventDefault();
    fromCountryDiv.style.display == "none"
        ? (fromCountryDiv.style.display = "block")
        : (fromCountryDiv.style.display = "none");
});
toInput.addEventListener("click", function (e) {
    e.preventDefault();
    toCountryDiv.style.display == "none"
        ? (toCountryDiv.style.display = "block")
        : (toCountryDiv.style.display = "none");
});
countryButton.forEach((button) => {
    button.addEventListener("click", (e) => {
        e.preventDefault();
        let countryText = button.querySelector("p").textContent;
        if (button.classList.contains("from_input")) {
            fromInput.value = countryText;
            fromCountryDiv.style.display = "none";
        }
        if (button.classList.contains("to_inputt")) {
            toInput.value = countryText;
            toCountryDiv.style.display = "none";
        }
    });
});
countryInput.forEach((input) => {
    input.addEventListener("input", function () {
        if (input.classList.contains("from_input")) {
            let searchText = input.value.toLowerCase();
            countryButton.forEach((btn) => {
                if (btn.classList.contains("from_input")) {
                    let countryName = btn.querySelector("p").textContent.toLowerCase();
                    if (countryName.textContent == "") {
                        btn.classList.remove("d-none");
                        btn.classList.add("d-flex");
                    }
                    if (countryName.startsWith(searchText)) {
                        btn.classList.remove("d-none");
                        btn.classList.add("d-flex");
                    } else {
                        btn.classList.remove("d-flex");
                        btn.classList.add("d-none");
                    }
                }
            });
        }
        else {
            let searchText = input.value.toLowerCase();
            countryButton.forEach((btn) => {
                if (btn.classList.contains("to_inputt")) {
                    let countryName = btn.querySelector("p").textContent.toLowerCase();
                    if (countryName.textContent == "") {
                        btn.classList.remove("d-none");
                        btn.classList.add("d-flex");
                    }
                    if (countryName.startsWith(searchText)) {
                        btn.classList.remove("d-none");
                        btn.classList.add("d-flex");
                    } else {
                        btn.classList.remove("d-flex");
                        btn.classList.add("d-none");
                    }
                }
            });
        }
    });
});
let passangerClass = document.querySelectorAll(".passangerClass");
passangerClass.forEach((p) => {
    p.addEventListener("click", function () {
        passangerClass.forEach((s) => s.classList.remove("activeClass"));

        this.classList.add("activeClass");
    });
});

const sections = {
    bookflight: document.querySelector(".bookflight"),
    managebooking: document.querySelector(".managebooking"),
    flightstatus: document.querySelector(".flightstatus"),
};

document
    .querySelector(".bookFlightBtn button")
    .addEventListener("click", () => showSection("bookflight"));
document
    .querySelector(".manageFlightBtn button")
    .addEventListener("click", () => showSection("managebooking"));
document
    .querySelector(".flightstatusBtn button")
    .addEventListener("click", () => showSection("flightstatus"));

function showSection(sectionToShow) {
    Object.keys(sections).forEach((section) => {
        sections[section].style.display =
            section === sectionToShow ? "block" : "none";
    });
}

//vaxti selecte add elemek
function formatDate(date) {
    const day = String(date.getDate()).padStart(2, "0");
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const year = date.getFullYear();
    return `${day}.${month}.${year}`;
}

function populateDateOptions() {
    const dateSelect = document.getElementById("dateselect");

    const today = new Date();
    for (let i = -3; i <= 3; i++) {
        const tempDate = new Date();
        tempDate.setDate(today.getDate() + i);

        const formattedDate = formatDate(tempDate);
        const option = document.createElement("option");
        option.value = formattedDate;
        option.textContent = formattedDate;

        dateSelect.appendChild(option);
    }
}
populateDateOptions();