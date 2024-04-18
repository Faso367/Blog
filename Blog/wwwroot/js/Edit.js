
document.addEventListener("DOMContentLoaded", () => {
    // Используем строгий синтаксис во избежание ошибок и уязвимостей
    "use sctrict";

    const realFileBtn = document.getElementById("real-life");
    const customBtn = document.getElementById("custom-button");
    const customTxt = document.getElementById("custom-text");

    customBtn.addEventListener("click", function () {
        realFileBtn.click();
    });

    realFileBtn.addEventListener("change", function () {
        if (realFileBtn.value) {
            customTxt.innerHTML = realFileBtn.value.match(/[\/\\]([\w\d\s\.\-\(\)]+)$/)[1];
        }
        else {
            customTxt.innerHTML = "Файл не выбран";
        }
    });

    // -------------------------    Валидация
    const form = document.querySelector("form");
    //const tagsValidationRegex = /(#(\w){1,20})/;
    //const tagsValidationRegex = /[0-9]/;
    const tagsValidationRegex = /(#([а-яёА-ЯЁa-zA-Z]){1,20})$/;
    const linkValidationRegex = /t.me[/][\w]$/;

    // Содержит все input и textarea кроме тех, у которых id = ID, but, CurrentImageName, real-life
    const inputsAndTextareas = form.querySelectorAll('input:not(#Id):not(#but):not(#CurrentImageName):not(#real-life), textarea');
    //console.log(inputsAndTextareas);
    const submitBtn = document.getElementById("but");

    const validateElem = (elem) => {
        //let isValid = false;
        let isValid = true;
        //var ent = elem.nextElementSibling.textContent;
        console.log(isValid);
        switch (elem.name) {
            

            //if (elem.name === "groupName") {
            case "groupName":
                // Если имя пользователя не соответствует регулярке и поле пустое
                // Тк если поля еще не заполнены, то ошибка не должна выводиться
                // test - встроенный bool метод для соответствия ввода строке валидации
                if (elem.value !== "" && elem.value.length > 100) {
                    console.log(1);
                    console.log(isValid);
                    //if (elem.value.length > 100) {
                    elem.nextElementSibling.textContent = "Длина названия максимум 100 символов";
                    isValid = false;
                    //}
                }
                else {
                    elem.nextElementSibling.textContent = "";
                    isValid = true;
                }
                break;
            //}

            //if (elem.name === "description") {
            case "description":
                //console.log('description');
                if (elem.value !== "" && elem.value.length > 1000) {
                    console.log(2);
                    console.log(isValid);
                    elem.nextElementSibling.textContent = "Длина описания максимум 1000 символов";
                    isValid = false;
                    // Тут можно поменять цвет border
                }
                else {
                    elem.nextElementSibling.textContent = "";
                    isValid = true;
                }
                break;
            //}

            //if (elem.name === "password") {
            case "tags":
                console.log(elem.value);
                if (!tagsValidationRegex.test(elem.value) && elem.value !== "") {
                    console.log(3);
                    console.log(isValid);

                    if (elem.value.length > 100) {
                        elem.nextElementSibling.textContent = "Длина строки с тегами максимум 200 символов";
                    }
                    else {
                        elem.nextElementSibling.textContent = "Длина тега максимум 20 букв. Формат строки: #тег#тег";
                    }
                    isValid = false;
                }

                else {
                    elem.nextElementSibling.textContent = "";
                    isValid = true;
                }
                break;

            case "link":

                if (!linkValidationRegex.test(elem.value) && elem.value !== "") {
                    console.log(4);
                    console.log(isValid);
                    if (elem.value.length > 100) {
                        elem.nextElementSibling.textContent = "Длина гиперссылки максимум 100 символов";
                    }
                    else {
                        elem.nextElementSibling.textContent = "Гиперссылка должна начинаться с t.me/";
                    }
                    isValid = false;
                }
                else {
                    elem.nextElementSibling.textContent = "";
                    isValid = true;
                }
                break;
        } // switch закончился

        if (elem.value === "")
            isValid = false;
        else
            isValid = true;

        if (isValid === false) {
            //console.log("error");
            submitBtn.setAttribute('disabled', 'disabled');
        }
        else {
            //console.log("valid");
            submitBtn.removeAttribute('disabled');
        }
    };// validateElem закончился

    //for (let elem of form.elements) {
    for (let elem of inputsAndTextareas) {
        elem.addEventListener("blur", () => {
            validateElem(elem); // Описание функции validateElem выше
        })
    }

    // Событие происходит при клике на кнопку с type="submit"
    form.addEventListener("submit", (even) => {
        even.preventDefault();

        //for (let elem of inputs) {
        for (let elem of inputsAndTextareas) {
            //elem.value - это текст внутри элемента (текст, который вводит пользователь внутрь input)
            if (elem.value === "") {
                //console.log(elem.nextElementSibling); // Выводим в консоль элементы с пустым вводом
                // Делаем подпись под пустыми элементами
                elem.nextElementSibling.textContent = "Данное поле не заполнено!";
                //isValid = false;
            }
        }

        if (isValid) {
            //console.log("Форма успешно отправлена");
            form.submit();
            // Сбрасываем значения всех полей
            form.reset();
        }

    })
});