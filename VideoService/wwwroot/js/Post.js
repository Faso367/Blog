

document.addEventListener("DOMContentLoaded", () => {
    // Используем строгий синтаксис во избежание ошибок и уязвимостей
    "use sctrict";
    // Добавляю
    var wrapper = document.getElementsByClassName("undercomment-buttons");

    for (j = 0; j < wrapper.length; j++) {

        var likeIcon = wrapper[j].querySelector("#like");
        var dislikeIcon = wrapper[j].querySelector("#dislike");

        if (wrapper[j].querySelector("#likesCount").textContent == 1) {
            $(likeIcon).css('fontVariationSettings', "'FILL' 1");
        }
        else if (wrapper[j].querySelector("#dislikesCount").textContent == 1) {
            $(dislikeIcon).css('fontVariationSettings', "'FILL' 1");
        }
    }



    // БЫЛО
    ShowSendCommentSection(0);
    var acc = document.getElementsByClassName("show-answers-but");
    var i;

    for (i = 0; i < acc.length; i++) {
        acc[i].addEventListener("click", function () {

            this.classList.toggle("active");

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


function ReadMoreOrLess(id) {
    const moreText = document.getElementById('moreText' + '(' + id + ')');
    const but = document.getElementById('readMoreBut' + '(' + id + ')');
    const dots = document.getElementById('dots' + '(' + id + ')');

    if (moreText.style.display === 'none'
        || moreText.style.display === '') {
        moreText.style.display = 'inline';
        dots.style.display = "none";
        but.textContent = 'Свернуть';
    } else {
        moreText.style.display = 'none';
        dots.style.display = "inline";
        but.textContent = 'Читать далее';
    }
}



//function ShowTimeDifference(postId, mainCommentId) {
//    console.log(222);
//    //var currentdate = new Date();
//    //var datetime = currentdate.getDate() + "/"
//    //    + (currentdate.getMonth() + 1) + "/"
//    //    + currentdate.getFullYear() + " @ "
//    //    + currentdate.getHours() + ":"
//    //    + currentdate.getMinutes() + ":"
//    //    + currentdate.getSeconds();

//    //console.log(777);
    

//    //console.log(datetime);
//    this.innerHTML = $.get("/Post/ShowTimeDifference", { postId: postId, mainCommentId: mainCommentId });
//}

//var localLikesCount;
//var localDislikesCount;

function ChangeReactionsCount(postId, mainCommentId, like) {

    function ChangeReactionsCountOnServer(like, increment) { $.post("/Post/changeReactionsCount", { postId: postId, mainCommentId: mainCommentId, like: like, increment: increment }); };

    var wrapper = document.getElementById(`undercomment-buttons(${mainCommentId})`);

    var comment = document.getElementById(`comment(${mainCommentId})`);

    var likeIcon = wrapper.querySelector("#like");
    var dislikeIcon = wrapper.querySelector("#dislike");

    var likeElem = wrapper.querySelector("#likesCount");
    var dislikeElem = wrapper.querySelector("#dislikesCount");

    var localLikesCount = likeElem.getAttribute("data-likes-count");
    var localDislikesCount = dislikeElem.getAttribute("data-dislikes-count");

    //console.log(localLikesCount);
    //console.log(localDislikesCount);

    var likesCount = likeElem.textContent;
    var dislikesCount = dislikeElem.textContent;


    var isAuthenticated = document.getElementById("IsAuthenticated").innerText;

    // Если пользователь авторизован
    if (isAuthenticated == 'True') {

        // Если тыкаем на лайк
        if (like == "true") {

            if (localLikesCount == 0) {
                $(likeIcon).css('fontVariationSettings', "'FILL' 1");

                likesCount++;
                localLikesCount++;

                ChangeReactionsCountOnServer(true, true);

                // Если мы тыкаем на лайк и при этом уже дизлайкнули запись
                if (localDislikesCount == 1) {
                    $(dislikeIcon).css('fontVariationSettings', "'FILL' 0");
                    localDislikesCount--;
                    dislikesCount--;

                    ChangeReactionsCountOnServer(false, false);
                }
            }
            // Если уже лайкали
            else if (localLikesCount == 1) {
                $(likeIcon).css('fontVariationSettings', "'FILL' 0");
                likesCount--;
                localLikesCount--;

                ChangeReactionsCountOnServer(true, false);

            }
            likeElem.innerHTML = likesCount;
            dislikeElem.innerHTML = dislikesCount;
        }

        // Если тыкаем на дизлайк
        else if (like == "false") {
            if (localDislikesCount == 0) {

                $(dislikeIcon).css('fontVariationSettings', "'FILL' 1");

                // Если мы тыкаем на дизлайк и при этом уже лайкнули запись
                if (localLikesCount == 1) {

                    $(likeIcon).css('fontVariationSettings', "'FILL' 0");

                    localLikesCount--;
                    likesCount--;

                    ChangeReactionsCountOnServer(true, false);
                }
                dislikesCount++;
                localDislikesCount++;

                ChangeReactionsCountOnServer(false, true);

            }
            // Если уже дизлайкали запись
            else if (localDislikesCount == 1) {
                $(dislikeIcon).css('fontVariationSettings', "'FILL' 0");

                dislikesCount--;
                localDislikesCount--;

                //$.get("/Post/changeReactionsCount", { postId: postId, mainCommentId: mainCommentId, like: false, increment: false }, function (data) {
                //    if (data == "true") console.log("sendData");
                //});

                ChangeReactionsCountOnServer(false, false);

                //$.post("/Post/changeReactionsCount", { postId: postId, mainCommentId: mainCommentId, like: false, increment: false });
            }
            dislikeElem.innerHTML = dislikesCount;
            likeElem.innerHTML = likesCount;
        }

        likeElem.setAttribute("data-likes-count", localLikesCount);
        dislikeElem.setAttribute("data-dislikes-count", localDislikesCount);

    }
    // Если пользователь не авторизован
    else {
        console.log("User dont authenticated");

        //var popup = wrapper.querySelector("#myPopup");

        var popup = comment.querySelector("#myPopup");

        console.log(popup)

        popup.classList.toggle("show");
    }
}
//let isValid = false;
function ShowSendCommentSection(mainCommentId) {



    //var wrapper = document.getElementById(`undercomment-buttons(${mainCommentId})`);
    var sendCommentSection = document.getElementById(`sendCommentWrapper(${mainCommentId})`);


    //console.log(sendCommentSection);

   // if (mainCommentId != '0' && mainCommentId != 0) {
        sendCommentSection.style.display = "block";
    //}
    // -----------------------

    //var sendCommentInsideMainComment = sendCommentSection.querySelector(".send-comment");
    var form = sendCommentSection.querySelector(".send-comment .main-form");
    var Textarea = form.querySelector(".input-wrapper #comment-textarea");
    var But = form.querySelector("#send-comment-but");

    var popupWrapper = form.querySelector(".button-wrapper");
    var popupText = popupWrapper.querySelector(".popup .popuptext");
    console.log(Textarea);
    
    const changeColor1 = () => {
        But.style.backgroundColor = '#1aa95d';
    }

    const changeColor2 = () => {
        But.style.backgroundColor = '#1a1b1a';
    }

    let isValid = false;


    const validateElem = (elem) => {

        if (elem.value !== "") { // Это не значит непустое поле!? Видимо это не равно null

            But.removeAttribute('disabled');
            But.style.opacity = "1";
            But.style.cursor = "pointer";

            But.addEventListener('mouseenter', changeColor1, true);
            But.addEventListener('mouseleave', changeColor2, true);

            isValid = true;

        }


        else {
            But.setAttribute('disabled', 'disabled');
            But.style.opacity = "0.5";
            But.style.cursor = "default";
            if (isValid = true) { // ОН ВООБЕ КОГДА_НИБУДЬ БУДЕТ РАБОТАТЬ???

                But.removeEventListener('mouseenter', changeColor1, true);
                But.removeEventListener('mouseleave', changeColor2, true);

            }
            console.log("Поле пустое");
            popupText.style.visibility = "hidden";
            popupText.style.display = "none";
        }

    }

    validateElem(Textarea);

    //Textarea.addEventListener("blur", () => {
    //    validateElem(Textarea); // Описание функции validateElem выше
    //});

    Textarea.addEventListener("input", () => {
        validateElem(Textarea); // Описание функции validateElem выше
        console.log("inputEvent");
    });

    //function ShowPopup(popup) {
    //    var popup = document.getElementById("myPopup");
    //    popup.classList.toggle("show");
    //}


    // Событие происходит при клике на кнопку с type="submit"
    form.addEventListener("submit", (even) => {
        // Указываем, что следует отменить типичное поведение браузера
        // Теперь при нажатии на кнопку submit страница не будет перезагружена


        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        var isAuthenticated = document.getElementById("IsAuthenticated").innerText;

        console.log(isAuthenticated);

        //console.log("User is Authenticated?:" + isAuthenticated);

        even.preventDefault();

        //console.log("submitEvent");

        if (Textarea.value === "") {

            isValid = false;
            But.setAttribute('disabled', 'disabled');
            But.style.opacity = "0.5";
            But.style.cursor = "default";
            But.removeEventListener('mouseenter', changeColor1, true);
            But.removeEventListener('mouseleave', changeColor2, true);

        }

        else {
            isValid = true;
            But.removeAttribute('disabled');
            But.style.opacity = "1";
            But.style.cursor = "pointer";
            But.addEventListener('mouseenter', changeColor1, true);
            But.addEventListener('mouseleave', changeColor2, true);

            popupText.style.visibility = "visible";
            popupText.style.display = "block";

        }


        if (isValid) {


            if (isAuthenticated == 'True') {

                form.submit();
                // Сбрасываем значения всех полей
                form.reset();
            }

            else {

                //console.log("showPopup");

                //function myFunction() {
                // var popup = document.getElementById("myPopup");


                console.log(popupText);

                popupText.classList.toggle("show");

                console.log(popupText.classList)
                //}
                //myFunction();
            }
        }

    }) 
    
}

