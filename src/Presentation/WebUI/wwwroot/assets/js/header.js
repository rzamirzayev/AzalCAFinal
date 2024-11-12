let toggleButton = document.querySelectorAll(".head_button");

toggleButton.forEach((item) => {
  item.addEventListener("click", function () {
    let div = item.nextElementSibling;
    if (div.classList.contains("active")) {
      div.classList.remove("active");
    } else {
      div.classList.add("active");
      let liItem = document.querySelectorAll(".active li");
      liItem.forEach((list) => {
        list.addEventListener("click", function () {
          item.textContent = list.textContent;
          div.classList.remove("active");
        });
      });
    }
    // document.addEventListener("click", (event) => {
    //   console.log(event);
    //   if (item.contains(event.target) && div.contains(event.target)) {
    //     div.classList.remove = "active";
    //   }
    // });
  });
});
