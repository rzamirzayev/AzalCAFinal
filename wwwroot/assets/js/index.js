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
