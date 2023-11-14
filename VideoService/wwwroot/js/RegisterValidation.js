document.addEventListener('DOMContentLoaded', () => {
    "use strict";

    const form = document.querySelector("form");
    const validateElem = (elem) =>
    {
        if (elem.name = "username") {

        }
        if (elem.name == "email") {

        }
        if (elem.name == "password") {

        }
    };

    for (let elem of form.elements) {
        if (!elem.classList.contains("form-check-input") && elem.tagName != "BUTTON") {
            elem.addEventListener("blur", () => {
                validateElem(elem);
            });
        }
    }

form.addEventListener("submit", (even) => {
    even.preventDefault();
    for (let elem of form.elements) {
        if (!elem.classList.contains("form-checkinput") && elem.tagName != "BUTTON")
        {
            if (elem.value == "")
            {
                elem.nextElementSibling.textContent = "Данное поле не заполнено!";
            }
            else
            {
                elem.nextElementSibling.textContent = "";
            }
        }
    }
});
});
    //const form = document.querySelector("form");

    //form.addEventListener("submit", (even) => {
    //    even.preventDefault();

    //    for (let elem of form.elements) {
    //        if (!elem.classList.contains("form-checkinput") &&
    //            elem.tagName != "BUTTON") {
    //            if (elem.value == "") {

    //            }
    //        }
    //    }
    //});
});



//window.onload = function () {
//    // A validator to check if the input value is within a specified range
//    // The global validator must be added before creating the pristine instance

//    Pristine.addValidator("my-range", function (value, param1, param2) {
//        return parseInt(param1) <= value && value <= parseInt(param2)
//    }, "The value (${0}) must be between ${1} and ${2}", 5, false);

//    var form = document.getElementById("form1");

//    var pristine = new Pristine(form);

//    form.addEventListener('submit', function (e) {
//        e.preventDefault();
//        var valid = pristine.validate();
//        //alert('Form is valid: ' + valid);

//    });

//}