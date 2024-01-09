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

    //const inputs = document.querySelector("form");


    //console.log(form); // Увидим HTML-код внутри тега form
    //console.dir(form); // Увидим все свойства нашего объекта HTMLFormElement

    // Минимум 4 символа, максимум 16, буквы анг алфавита, цифры 0-9
    //const regExpName = /^[a-zA-Z_-]{4,16}$/;

    /*
    Попробуйте вот это выражение: ^[a-zA-Z][a-zA-Z0-9-]+$
    Объяснение:

    Тут ^ обозначает начало строки
    $ - конец строки
    Далее в [] идет перечисление тех символов, что должны быть в начале, так как стоят после ^
    a-zA-Z - это всего символы английского алфавита во всех регистрах
    После этого идут остальные символы в [a-zA-Z0-9-].
    В конце ставится + так как необходимо иметь от одного символа, чтобы ник был как минимум 2 символа. Если это не надо, то замените + на *
*/

    const regExpName = /^[a-zA-Zа-яА-ЯёЁ0-9]{4,16}$/;
    const regExpEmail = /^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$/; //с ХАБРА
    //const dogCheck = /^\w+([.-]?\w+)@\w+([.-]?\w)$/;
    // Минимум 3 цифры, 3 буквы, 3 буквы в верхнем регистре, 1 спецсимвол
    //const regExpPass = /^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$/; // с видео
    // Строчные и прописные латинские буквы, цифры, спецсимволы. Минимум 8 символов:

    const bigLetterCheck = /([a-z][A-Z])|([A-Z][a-z])/;
    const numberCheck = /(?=.*[0-9])/;
    const regExpPass = /^(?=^.{8,32}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z]).*$/; 
    // Фильтруем содержимое внутри формы (зачем опять пробегаться по всему html)
    const pass = form.querySelector(".pas");
    const passConf = form.querySelector(".pasConf");

    let isValid = false;

    const inputs = form.querySelectorAll("input");
    const submitBtn = document.getElementById("but");

    const validateElem = (elem) => {
        if (elem.name === "username") {
            // Если имя пользователя не соответствует регулярке и поле пустое
            // Тк если поля еще не заполнены, то ошибка не должна выводиться
            // test - встроенный bool метод для соответствия ввода строке валидации
            if (!regExpName.test(elem.value) && elem.value !== "") {

                if (elem.value.length < 4 || elem.value.length > 16)
                    elem.nextElementSibling.textContent = "Длина имени от 4 до 16 символов";

                else {
                    elem.nextElementSibling.textContent = "Имя должно содержать только буквы и цифры";
                }

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

                if (elem.value.length < 8 || elem.value.length > 32)
                    elem.nextElementSibling.textContent = "Длина пароля от 8 до 32 символов";

                else if (!bigLetterCheck.test(elem.value))
                    elem.nextElementSibling.textContent = "Должна быть минимум 1 заглавная буква";

                else if (!numberCheck.test(elem.value))
                    elem.nextElementSibling.textContent = "Пароль должен содержать минимум 1 цифру";

                else
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

        if (elem.name === "ConfirmPassword") {
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

        if (elem.value === "")
            isValid = false;
        else
            isValid = true;

        if (!isValid) {
            //console.log("error");
            submitBtn.setAttribute('disabled', 'disabled');
        }
        else {
            //console.log("valid");
            submitBtn.removeAttribute('disabled');
        }
    }

    for (let elem of form.elements) {
        // Если у элемента class != 'form-check-input' или его тег != button
        // То есть мы отбрасываем input с галочкой и кнопку Submit,
        // таким образом остались только обычные input
        //if (!elem.classList.contains("form-check-input") // !!!!!!!!!!!!!!!!
        //    && elem.tagName !== "BUTTON"       //!!!!!!!!!!!!!!!!!!!!
        //) {
            // Вешаем обработчики событий на inputы
            // Blur срабатывает когда пользователь сделал ввод и ушел с этого поля
            // (кликнул мышкой на пустое место или перешел на след input) 
            elem.addEventListener("blur", () => {
                validateElem(elem); // Описание функции validateElem выше
            })
        //}
    }

    // Событие происходит при клике на кнопку с type="submit"
    form.addEventListener("submit", (even) => {
        // Указываем, что следует отменить типичное поведение браузера
        // Теперь при нажатии на кнопку submit страница не будет перезагружена

        even.preventDefault();

        // Пробегаемся по объектам нашего HTMLFormElement
        // где elem типа Element, в данном случае является HTML элементом (тегом с атрибутами)
        for (let elem of inputs) {

                //elem.value - это текст внутри элемента (текст, который вводит пользователь внутрь input)
                if (elem.value === "") {
                    //console.log(elem.nextElementSibling); // Выводим в консоль элементы с пустым вводом
                    // Делаем подпись под пустыми элементами
                    elem.nextElementSibling.textContent = "Данное поле не заполнено!";
                    isValid = false;
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