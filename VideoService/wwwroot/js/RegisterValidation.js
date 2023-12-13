// Событие DOMContentLoaded возникает, когда HTML-документ полностью проанализирован,
// а все отложенные сценарии <script> выполнены
// Если scriptам нужен css код, то он тоже подгружается до этого события

document.addEventListener("DOMContentLoaded", () => {
    // Используем строгий синтаксис во избежание ошибок и уязвимостей
    "use sctrict";

    // Получаем все html-элементы внутри первого попавшегося тега <form>
    // и записываем их в переменную типа HTMLFormElement.
    // По факту содержит весь HTML-код внутри тега, причем затем мы можем
    // отдельно получить для атрибуты разных тегов и классов с помощью специальных свойств.
    const form = document.querySelector("form");
    //console.log(form); // Увидим HTML-код внутри тега form
    //console.dir(form); // Увидим все свойства нашего объекта HTMLFormElement

    // Минимум 3 символа, максимум 16, буквы анг алфавита, цифры 0-9
    const regExpName = /^[a-z0-9_-]{3,16}$/;
    //const regExpEmail = /^[A-Z0-9._%+-]+@[A-Z0-9-]+.+.[A-Z]{2,4}$/i; // с видео
    const regExpEmail = /^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$/; //с ХАБРА
    // Минимум 3 цифры, 3 буквы, 3 буквы в верхнем регистре, 1 спецсимвол
    //const regExpPass = /^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$/; // с видео
    // Строчные и прописные латинские буквы, цифры, спецсимволы. Минимум 8 символов:
    const regExpPass = /^(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/; // с ХАБРА

    // Фильтруем содержимое внутри формы (зачем опять пробегаться по всему html)
    const pass = form.querySelector(".pas");
    const passConf = form.querySelector(".pasConf");
    //let isValid = false;
    let isValid = false;
    // if (elem.value.length < 3) {
    //     elem.nextElementSibling.textContent = "От 3 до 16";
    // } 

    const submit = () => {
        alert("Данные отправлены");
    };

    const validateElem = (elem) => {
        if (elem.name === "username") {
            // Если имя пользователя не соответствует регулярке и поле пустое
            // Тк если поля еще не заполнены, то ошибка не должна выводиться
            // test - встроенный bool метод для соответствия ввода строке валидации
            if (!regExpName.test(elem.value) && elem.value !== "") {
                elem.nextElementSibling.textContent = "Введите корректное имя пользователя";
                isValid = false;
                // Тут можно поменять цвет border
            }
            else {
                elem.nextElementSibling.textContent = "";
                isValid = true;
            }
        }
        if (elem.name === "email") {
            if (!regExpEmail.test(elem.value) && elem.value !== "") {
                elem.nextElementSibling.textContent = "Введите корректный email";
                isValid = false;
                // Тут можно поменять цвет border
            }
            else {
                elem.nextElementSibling.textContent = "";
                isValid = true;
            }
        }
        if (elem.name === "password") {
            if (pass.value !== passConf.value && passConf.value !== "") {
                pass.nextElementSibling.textContent = "Пароли должны совпадать";
                passConf.nextElementSibling.textContent = "Пароли должны совпадать";
                isValid = false;
                // Тут можно поменять цвет border
            }
            else {
                pass.nextElementSibling.textContent = "";
                passConf.nextElementSibling.textContent = "";
                isValid = true;
            }

            if (!regExpPass.test(elem.value) && elem.value !== "") {
                elem.nextElementSibling.textContent = "Введите корректный пароль";
                isValid = false;
                // Тут можно поменять цвет border
            }
            else {
                pass.nextElementSibling.textContent = "";
                passConf.nextElementSibling.textContent = "";
                isValid = true;
            }
        }

        if (elem.name === "passwordConfirmation") {
            if (passConf.value !== pass.value && passConf.value !== "") {
                pass.nextElementSibling.textContent = "Пароли должны совпадать";
                passConf.nextElementSibling.textContent = "Пароли должны совпадать";
                isValid = false;
            }
            else {
                pass.nextElementSibling.textContent = "";
                passConf.nextElementSibling.textContent = "";
                isValid = true;
            }
        }
    }

    for (let elem of form.elements) {
        // Если у элемента class != 'form-check-input' или его тег != button
        // То есть мы отбрасываем input с галочкой и кнопку Submit,
        // таким образом остались только обычные input
        if (!elem.classList.contains("form-check-input") // !!!!!!!!!!!!!!!!
            && elem.tagName !== "BUTTON"       //!!!!!!!!!!!!!!!!!!!!
        ) {
            // Вешаем обработчики событий на inputы
            // Blur срабатывает когда пользователь сделал ввод и ушел с этого поля
            // (кликнул мышкой на пустое место или перешел на след input) 
            elem.addEventListener("blur", () => {
                validateElem(elem); // Описание функции validateElem выше
            })
        }
    }

    // Событие происходит при клике на кнопку с type="submit"
    form.addEventListener("submit", (even) => {
        // Указываем, что следует отменить типичное поведение браузера
        // Теперь при нажатии на кнопку submit страница не будет перезагружена
        even.preventDefault();
        //console.log("Сайт не был перезагружен");

        // Пробегаемся по объектам нашего HTMLFormElement
        // где elem типа Element, в данном случае является HTML элементом (тегом с атрибутами)
        for (let elem of form.elements) {
            // Если у элемента class != 'form-check-input' или его тег != button
            // То есть мы отбрасываем input с галочкой и кнопку Submit,
            // таким образом остались только обычные input
            if (!elem.classList.contains("form-check-input")   //!!!!!!!!!!!!!!
                && elem.tagName !== "BUTTON"   //!!!!!!!!!!!!!!!
            ) {
                //elem.value - это текст внутри элемента (текст, который вводит пользователь внутрь input)
                if (elem.value === "") {
                    //console.log(elem.nextElementSibling); // Выводим в консоль элементы с пустым вводом
                    // Делаем подпись под пустыми элементами
                    elem.nextElementSibling.textContent = "Данное поле не заполнено!";
                    isValid = false;
                }
                // Если ввод непустой
                else {
                    elem.nextElementSibling.textContent = "";
                    isValid = true;
                }
            }
        }
        console.log(isValid);




        if (isValid) {
           // if (form.querySelector(".form-check-input").ariaChecked) {
                //console.log("Форма успешно отправлена");
                submit();
                // Сбрасываем значения всех полей
                form.reset();
         //   }
         //   else {
         //       alert("Для отправки формы согласитесь с условиями");
         //   }
        }


    })
});

// Проверить значения всех inputов можно через querySelectorAll("input")
// или же просто перебрать все теги внутри формы и обработать их