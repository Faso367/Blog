//function ReadMoreOrLess() {
//    var dots = document.getElementById("dots");
//    var moreText = document.getElementById("more");
//    var btnText = document.getElementById("readMoreBut");

//    if (dots.style.display === "none") {
//        dots.style.display = "inline";
//        btnText.innerHTML = "Read more";
//        moreText.style.display = "none";
//    } else {
//        dots.style.display = "none";
//        btnText.innerHTML = "Read less";
//        moreText.style.display = "inline";
//    }
//}



//function ReadMoreOrLess(id) {
//    const moreText = document.getElementById('moreText' + '(' + id + ')');
//    const but = document.getElementById('readMoreBut' + '(' + id + ')');
//    const dots = document.getElementById('dots' + '(' + id + ')')

//    if (moreText.style.display === 'none'
//        || moreText.style.display === '') {
//        moreText.style.display = 'inline';
//        dots.style.display = "none";
//        but.textContent = 'Свернуть';
//    } else {
//        moreText.style.display = 'none';
//        but.textContent = 'Читать далее';
//        dots.style.display = "inline";
//    }
//} 


function ReadMoreOrLess(id) {
    const moreText = document.getElementById('moreText' + '(' + id + ')');
    const but = document.getElementById('readMoreBut' + '(' + id + ')');
    const dots = document.getElementById('dots' + '(' + id + ')');
    //const MyText = document.getElementById('MyText' + '(' + id + ')');

    if (moreText.style.display === 'none'
        || moreText.style.display === '') {
        //dots.textContent = MyText.textContent;
        moreText.style.display = 'inline';
        dots.style.display = "none";
        but.textContent = 'Свернуть';
    } else {
        moreText.style.display = 'none';
        dots.style.display = "inline";
        //dots.textContent = '...';
        but.textContent = 'Читать далее';
    }
} 


document.addEventListener("DOMContentLoaded", () => {
    // Используем строгий синтаксис во избежание ошибок и уязвимостей
    "use sctrict";

    var acc = document.getElementsByClassName("show-answers-but");
    var i;

    for (i = 0; i < acc.length; i++) {
        acc[i].addEventListener("click", function () {
            /* Toggle between adding and removing the "active" class,
            to highlight the button that controls the panel */
            this.classList.toggle("active");

            /* Toggle between hiding and showing the active panel */
            var panel = this.nextElementSibling;
            if (panel.style.maxHeight) {
                panel.style.maxHeight = null;
            } else {
                panel.style.maxHeight = panel.scrollHeight + "px";
            }

            if (panel.style.display === "block") {
                panel.style.display = "none";
            } else {
                panel.style.display = "block";
            }
        });
    }

});
//function ShowSubcomments(id) {
//    //const but = document.getElementById('showSubcommentsBut' + '(' + id + ')');
//    const subcommentText = document.getElementById('subcocommentText' + '(' + id + ')');

//    subcommentText.style.display = 'block';
//}