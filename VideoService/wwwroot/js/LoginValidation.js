

document.addEventListener("DOMContentLoaded", () => {
    "use sctrict";

    const regExpName = /^[a-zA-Zа-яА-ЯёЁ0-9]{4,16}$/;
    const regExpPass = /^(?=^.{8,32}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z]).*$/;
    let isValid = false;

    const form = document.querySelector("form");
    const submitBtn = document.getElementById("but");
    var inputs = document.querySelectorAll("input");

    const validateElem = (elem) => {

        if (elem.name === "username") {

            if (!regExpName.test(elem.value) && elem.value !== "") {

                if (elem.value.length < 4 || elem.value.length > 16)
                    elem.nextElementSibling.textContent = "Длина имени от 4 до 16 символов";
                else
                    elem.nextElementSibling.textContent = "Имя должно содержать только буквы и цифры";

                isValid = false;
            }
            else {
                elem.nextElementSibling.textContent = "";
                isValid = true;
            }
        }
        if (elem.name === "password") {

            if (!regExpPass.test(elem.value) && elem.value !== "") {

                if (elem.value.length < 8 || elem.value.length > 32)
                    elem.nextElementSibling.textContent = "Длина пароля от 8 до 32 символов";

                else if (!bigLetterCheck.test(elem.value))
                    elem.nextElementSibling.textContent = "Должна быть минимум 1 заглавная буква";

                else if (!numberCheck.test(elem.value))
                    elem.nextElementSibling.textContent = "Пароль должен содержать минимум 1 цифру";

                else
                    elem.nextElementSibling.textContent = "Введите корректный пароль";

                isValid = false;
            }
            else {
                pass.nextElementSibling.textContent = "";
                passConf.nextElementSibling.textContent = "";
                isValid = true;
            }
        }
        if (elem.value === "")
            isValid = false;
        //else
        //    isValid = true;

        if (!isValid) {
            //console.log("error");
            submitBtn.setAttribute('disabled', 'disabled');
        }
        else {
            //console.log("valid");
            submitBtn.removeAttribute('disabled');
        }
    }; // Конец функции validateElem



    for (let elem of form.elements) {
        elem.addEventListener("blur", () => {
            validateElem(elem); // Описание функции validateElem выше
        });
    }
    form.addEventListener("submit", (even) => {
        even.preventDefault();
        for (let elem of inputs) {
            if (elem.value === "") {
                elem.nextElementSibling.textContent = "Данное поле не заполнено!";
                isValid = false;
            }
        }
        if (isValid) {
            //console.log("Форма успешно отправлена");
            form.submit();
            form.reset();
        }
    });

})