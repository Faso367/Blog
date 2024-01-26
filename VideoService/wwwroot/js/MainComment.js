

function changeColor(element) {
    element.style.backgroundColor = "#1aa95d";
}

document.addEventListener("DOMContentLoaded", () => {
    // Используем строгий синтаксис во избежание ошибок и уязвимостей
    "use sctrict";

    // Получаем все html-элементы внутри первого попавшегося тега <form>
    // и записываем их в переменную типа HTMLFormElement.
    // По факту содержит весь HTML-код внутри тега, причем затем мы можем
    // отдельно получить для атрибуты разных тегов и классов с помощью специальных свойств.
    const form = document.querySelector("form");
    const Textarea = document.getElementById("comment-textarea");
    const But = document.getElementById("send-comment-but");

    const openBtn = document.getElementById("send-comment-but");
    const closeBtn = document.getElementById("closeModal");
    const modal = document.getElementById("modal");
    const isAuthenticated = document.getElementsByClassName("IsAuthenticated").textContent;

    const changeColor1 = () => {
        But.style.backgroundColor = '#1aa95d';
    }

    const changeColor2 = () => {
        But.style.backgroundColor = '#1a1b1a';
    }

    let isValid = false;


    const validateElem = (elem) => {

        if (elem.value !== "") {

            But.removeAttribute('disabled');
            But.style.opacity = "1";
            But.style.cursor = "pointer";

            But.addEventListener('mouseenter', changeColor1, true);
            But.addEventListener('mouseleave', changeColor2, true);
            /*console.log("add");*/
            isValid = true;

        }
            

        else {
            But.setAttribute('disabled', 'disabled');
            But.style.opacity = "0.5";
            But.style.cursor = "default";
            if (isValid = true) {
                /*console.log("remove");*/
                But.removeEventListener('mouseenter', changeColor1, true);
                But.removeEventListener('mouseleave', changeColor2, true);
            }

        }
            
    }

    validateElem(Textarea);

    //Textarea.addEventListener("blur", () => {
    //    validateElem(Textarea); // Описание функции validateElem выше
    //});

    Textarea.addEventListener("input", () => {
        validateElem(Textarea); // Описание функции validateElem выше
    });

    // Событие происходит при клике на кнопку с type="submit"
    form.addEventListener("submit", (even) => {
        // Указываем, что следует отменить типичное поведение браузера
        // Теперь при нажатии на кнопку submit страница не будет перезагружена

        even.preventDefault();

        console.log("submitEvent");

        if (Textarea.value === "") {

            isValid = false;
            But.setAttribute('disabled', 'disabled');
            But.style.opacity = "0.5";
            But.style.cursor = "default";
            But.removeEventListener('mouseenter', changeColor1, true);
            But.removeEventListener('mouseleave', changeColor2, true);
            /*console.log("remove");*/
        }

        else {
            isValid = true;
            But.removeAttribute('disabled');
            But.style.opacity = "1";
            But.style.cursor = "pointer";
            But.addEventListener('mouseenter', changeColor1, true);
            But.addEventListener('mouseleave', changeColor2, true);
            /*console.log("add");*/
        }
            
            

        if (isValid) {


            if (isAuthenticated) {

                closeBtn.addEventListener("click", () => {
                    modal.classList.remove("open");
                });

                //console.log("Форма успешно отправлена");
                form.submit();
                // Сбрасываем значения всех полей
                form.reset();
            }

            else {
                openBtn.addEventListener("click", () => {
                    modal.classList.add("open");
                });
            }
        }

    })
});



function auto_grow(element) {
        /*element.style.height = "5px";*/
    element.style.height = "0px";
    /*element.style.height = (element.scrollHeight) + "px";*/
    element.style.height = (element.scrollHeight - 5) + "px";
}