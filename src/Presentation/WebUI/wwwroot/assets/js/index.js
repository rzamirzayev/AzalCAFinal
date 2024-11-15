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
passangerBtn.addEventListener("click", function (e) {
    e.preventDefault();
    if (passangerDiv.style.display === "none") {
        passangerDiv.style.display = "block";
    } else {
        passangerDiv.style.display = "none";
    }
});

passangerButton.forEach((btn) => {
    btn.addEventListener("click", function (e) {
        e.preventDefault();
        if (btn.classList.contains("passanger_minus")) {
            let number = parseInt(btn.nextElementSibling.textContent);

            if (number === 0) {
            } else {
                btn.nextElementSibling.textContent = number - 1;
            }
        }
        if (btn.classList.contains("passanger_plus")) {
            let number = parseInt(btn.previousElementSibling.textContent);
            btn.previousElementSibling.textContent = number + 1;
        }
        const total = Array.from(passangerSum)
            .map((p) => parseInt(p.textContent) || 0)
            .reduce((acc, curr) => acc + curr, 0);
        passangers.textContent = total;
    });
});
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
calendarDay.forEach((day) => {
    day.addEventListener("click", function (e) {
        e.preventDefault();
        let textMonth = calendarMonth.textContent;
        let textYear = calendarYear.textContent;

        let datetime = day.textContent + "." + textMonth + "." + textYear;
        flightBtn.textContent = datetime;
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
    console.log("2");
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
        } else {
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
