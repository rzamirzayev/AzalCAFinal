const items = document.querySelectorAll(".carousel-itemm");
const prevBtn = document.getElementById("prevBtn");
const nextBtn = document.getElementById("nextBtn");

let currentIndex = 0;
const itemsToShow = 5;

function updateCarousel() {
  const offset = -currentIndex * (100 / itemsToShow);
  document.querySelector(
    ".carousel-innerr"
  ).style.transform = `translateX(${offset}%)`;
}
nextBtn.addEventListener("click", () => {
  if (currentIndex + itemsToShow < items.length) {
    currentIndex += itemsToShow;
  }
  updateCarousel();
});
prevBtn.addEventListener("click", () => {
  if (currentIndex - itemsToShow >= 0) {
    currentIndex -= itemsToShow;
  }
  updateCarousel();
});
updateCarousel();
