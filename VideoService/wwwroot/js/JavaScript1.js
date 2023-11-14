document.addEventListener("DOMContentLoaded", () => {
    "use strict";
    //получаем элементы со страницы
    const form = document.querySelector("form");
    const pass = document.querySelector(".formPass");
    const passConf = document.querySelector(".formPassConf");
    const check = document.querySelector(".form-check-input");
    let isSubmit = false;

    const regExpName = /^[a-z0-9_-]{3,16}$/;
    const regExpEmail = /^[A-Z0-9. %+-]+@[A-Z0-9-]+.+.[A-Z]{2,4}$/i;
    const regExpPass = /^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9][0-9])(?=.*[a-z].*[a-z].*[a-z]).{8,}$/;

    const submit = () => {
        alert("Данные отправлены");

        for (let elem of form.elements) {
            if (!elem.classList.contains("btn") && !elem.classList.contains("form-check-input")) {
                elem.value = "";
            }

            if (elem.classList.contains("form-check-input")) {
                elem.checked = false;
            }
        }
    };
});